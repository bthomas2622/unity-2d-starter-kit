using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour, SceneController
{
    private int menuLayer = 1;
    private Vector2Int mainMenuPosition = new Vector2Int(1, 1);
    private Vector2Int settingsPosition = new Vector2Int(1, 1);
    private Vector2Int controlsPosition = new Vector2Int(1, 1);

    // xbounds (left to right), ybounds (bottom to top), layer, row, col
    private List<float> settingsButton = new List<float>() { -1.4f, 1.4f, -9.4f, -6.6f, 1f, 2f, 1f};

    private List<List<float>> sceneClickables = new List<List<float>>();
    private ControlsController controlsController;
    private SettingsController settingsController;
    private SpriteRenderer settingsBackdrop;

    private int menuLayerMax = 2;
    private int settingsPosXMax = 7;
    private int controlsPosXMax = 7;

    private SelectedIcon selectedIconLeft;
    private SelectedIcon selectedIconRight;

    void Awake()
    {
        menuLayer = 1;
        sceneClickables.Add(settingsButton);
        controlsController = GameObject.Find("ControlsController").GetComponent<ControlsController>();
        settingsController = GameObject.Find("SettingsController").GetComponent<SettingsController>();
        settingsBackdrop = GameObject.Find("MenuLayer2Backdrop").GetComponent<SpriteRenderer>();
        settingsBackdrop.enabled = false;
        selectedIconLeft = GameObject.Find("SelectedIconLeft").GetComponent<SelectedIcon>();
        selectedIconRight = GameObject.Find("SelectedIconRight").GetComponent<SelectedIcon>();
    }

    public void Move(Util.Direction direction)
    {
        if (direction == Util.Direction.up)
        {
            if (menuLayer == 1)
            {
                if (mainMenuPosition.x > 1)
                {
                    mainMenuPosition.x -= 1;
                }
            }
            else if (menuLayer == 2)
            {
                if (settingsPosition.x > 1)
                {
                    settingsPosition.x -= 1;
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.x > 1)
                {
                    controlsPosition.x -= 1;
                }
            }
        }
        else if (direction == Util.Direction.down)
        {
            if (menuLayer == 1)
            {
                if (mainMenuPosition.x < menuLayerMax)
                {
                    mainMenuPosition.x += 1;
                }
            }
            else if (menuLayer == 2)
            {
                if (settingsPosition.x < settingsPosXMax)
                {
                    settingsPosition.x += 1;
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.x < controlsPosXMax)
                {
                    controlsPosition.x += 1;
                }
            }
        }
        else if (direction == Util.Direction.left)
        {
            if (menuLayer == 2)
            {
                settingsController.Left(GetSettingOptionFromXPos());
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.y == 2)
                {
                    controlsPosition.y -= 1;
                }
            }
        }
        else if (direction == Util.Direction.right)
        {
            if (menuLayer == 2)
            {
                settingsController.Right(GetSettingOptionFromXPos());
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.y == 1)
                {
                    controlsPosition.y += 1;
                }
            }
        }
        UpdateSelected();
    }

    public void Select()
    {
        if (menuLayer == 1)
        {
            if (mainMenuPosition.x == 2)
            {
                menuLayer += 1;
                ResetLayerDefaultPositions(true, true);
                settingsBackdrop.enabled = true;
                settingsController.ShowSettings();
            }
        }
        else if (menuLayer == 2)
        {
            if (settingsPosition.x == 4)
            {
                menuLayer += 1;
                settingsController.HideSettings();
                ResetLayerDefaultPositions(false, true);
                controlsController.ShowControls(true);
            }
            else if (settingsPosition.x == 5)
            {
                menuLayer += 1;
                settingsController.HideSettings();
                ResetLayerDefaultPositions(false, true);
                controlsController.ShowControls(false);
            }
            else if (settingsPosition.x == 7)
            {
                menuLayer = 1;
                settingsBackdrop.enabled = false;
                settingsController.HideSettings();
            }
        }
        else if (menuLayer == 3)
        {
            if (controlsPosition.x == 7)
            {
                if (controlsPosition.y == 1)
                {
                    menuLayer = 2;
                    controlsController.HideControls();
                    settingsController.ShowSettings();
                }
                else
                {
                    PlayerSettings.Instance.RestoreControlDefaults();
                    controlsController.ReDisplayCorrectBindings();
                }
            }
            else
            {
                controlsController.RemapSelectedControl(GetControlsOptionFromXYPos());
            }
        }
        UpdateSelected();
    }

    private void UpdateSelected()
    {
        int xPositionUpdate = menuLayer == 1 ? mainMenuPosition.x : menuLayer == 2 ? settingsPosition.x : controlsPosition.x;
        int yPositionUpdate = menuLayer == 1 ? mainMenuPosition.y : menuLayer == 2 ? settingsPosition.y : controlsPosition.y;
        selectedIconLeft.UpdateSelectedIconPosition(menuLayer, xPositionUpdate, yPositionUpdate);
        selectedIconRight.UpdateSelectedIconPosition(menuLayer, xPositionUpdate, yPositionUpdate);
        if (menuLayer == 2)
        {
            settingsController.ChangeSettingSelected(GetSettingOptionFromXPos());
        }
    }

    private void ResetLayerDefaultPositions(bool settings, bool controls)
    {
        if (settings)
        {
            settingsPosition.x = 1;
            settingsPosition.y = 1;
        }
        else if (controls)
        {
            controlsPosition.x = 1;
            controlsPosition.y = 1;
        }
    }

    public void Back()
    {
        if (menuLayer == 2)
        {
            menuLayer = 1;
            settingsBackdrop.enabled = false;
            settingsController.HideSettings();
        }
        else if (menuLayer == 3)
        {
            menuLayer = 2;
            controlsController.HideControls();
            settingsController.ShowSettings();
        }
        UpdateSelected();
    }

    public void Click(Vector2 clickLocation)
    {
        Vector2Int clickAnalysis = Util.ReturnPositionFromMouse(clickLocation, menuLayer, sceneClickables);
        if (clickLocation.x != 0)
        {
            if (menuLayer == 1)
            {
                mainMenuPosition = clickAnalysis;
            }
            else if (menuLayer == 2)
            {
                settingsPosition = clickAnalysis;
            }
            else if (menuLayer == 3)
            {
                controlsPosition = clickAnalysis;
            }
            Select();
        }
    }

    public void Point(Vector2 pointerLocation)
    {
        Vector2Int clickAnalysis = Util.ReturnPositionFromMouse(pointerLocation, menuLayer, sceneClickables);
        if (pointerLocation.x != 0)
        {
            if (menuLayer == 1)
            {
                mainMenuPosition = clickAnalysis;
            }
            else if (menuLayer == 2)
            {
                settingsPosition = clickAnalysis;
            }
            else if (menuLayer == 3)
            {
                controlsPosition = clickAnalysis;
            }
        }
    }

    private ControlsController.ControlsOptions GetControlsOptionFromXYPos()
    {
        if (controlsPosition.x == 1)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.selectOne;
            }
            else
            {
                return ControlsController.ControlsOptions.selectTwo;
            }
        }
        else if (controlsPosition.x == 2)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.backOne;
            }
            else
            {
                return ControlsController.ControlsOptions.backTwo;
            }
        }
        else if (controlsPosition.x == 3)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.upOne;
            }
            else
            {
                return ControlsController.ControlsOptions.upTwo;
            }
        }
        else if (controlsPosition.x == 4)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.downOne;
            }
            else
            {
                return ControlsController.ControlsOptions.downTwo;
            }
        }
        else if (controlsPosition.x == 5)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.leftOne;
            }
            else
            {
                return ControlsController.ControlsOptions.leftTwo;
            }
        }
        else if (controlsPosition.x == 6)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.rightOne;
            }
            else
            {
                return ControlsController.ControlsOptions.rightTwo;
            }
        }
        else
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.exit;
            }
            else
            {
                return ControlsController.ControlsOptions.reset;
            }
        }
    }

    private SettingsController.SettingOptions GetSettingOptionFromXPos()
    {
        if (settingsPosition.x == 1)
        {
            return SettingsController.SettingOptions.fullscreen;
        }
        else if (settingsPosition.x == 2)
        {
            return SettingsController.SettingOptions.musicVolume;
        }
        else if (settingsPosition.x == 3)
        {
            return SettingsController.SettingOptions.effectsVolume;
        }
        else if (settingsPosition.x == 4)
        {
            return SettingsController.SettingOptions.keyboardControls;
        }
        else if (settingsPosition.x == 5)
        {
            return SettingsController.SettingOptions.gamepadControls;
        }
        else if (settingsPosition.x == 6)
        {
            return SettingsController.SettingOptions.language;
        }
        else
        {
            return SettingsController.SettingOptions.na;
        }
    }
}
