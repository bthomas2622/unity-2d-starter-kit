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
    // layer 1
    private List<float> playButton = new List<float>() { -3.5f, 3.5f, -1.5f, 1.5f, 1f, 1f, 1f };
    private List<float> settingsButton = new List<float>() { -1.4f, 1.4f, -5.5f, -2.5f, 1f, 2f, 1f};
    // layer 2
    private List<float> fullscreen = new List<float>() { 4.6f, 14.6f, 4.65f, 6.65f, 2f, 1f, 1f };
    private List<float> fullscreenRight = new List<float>() { 14.61f, 16.6f, 4.65f, 6.65f, 2f, 1f, 2f };
    private List<float> fullscreenLeft = new List<float>() { 2.59f, 4.59f, 4.65f, 6.65f, 2f, 1f, -1f };
    private List<float> music = new List<float>() { 4.6f, 14.6f, 2.4f, 4.4f, 2f, 2f, 1f };
    private List<float> musicRight = new List<float>() { 14.61f, 16.6f, 2.4f, 4.4f, 2f, 2f, 2f };
    private List<float> musicLeft = new List<float>() { 2.59f, 4.59f, 2.4f, 4.4f, 2f, 2f, -1f };
    private List<float> effects = new List<float>() { 4.6f, 14.6f, 0.15f, 2.15f, 2f, 3f, 1f };
    private List<float> effectsRight = new List<float>() { 14.61f, 16.6f, 0.15f, 2.15f, 2f, 3f, 2f };
    private List<float> effectsLeft = new List<float>() { 2.59f, 4.59f, 0.15f, 2.15f, 2f, 3f, -1f };
    private List<float> keyboardSettings = new List<float>() { 4.6f, 14.6f, -2.1f, -0.1f, 2f, 4f, 1f };
    private List<float> gamepadSettings = new List<float>() { 4.6f, 14.6f, -4.35f, -2.35f, 2f, 5f, 1f };
    private List<float> language = new List<float>() { 4.6f, 14.6f, -6.6f, -4.6f, 2f, 6f, 1f };
    private List<float> languageRight = new List<float>() { 14.61f, 16.6f, -6.6f, -4.6f, 2f, 6f, 2f };
    private List<float> languageLeft = new List<float>() { 2.59f, 4.59f, -6.6f, -4.6f, 2f, 6f, -1f };
    private List<float> settingsExit = new List<float>() { -13.5f, 3.5f, -9.5f, -7.5f, 2f, 7f, 1f };
    // layer 3
    private List<float> select1 = new List<float>() { 1.25f, 8.75f, 4.65f, 6.65f, 3f, 1f, 1f };
    private List<float> select2 = new List<float>() { 9.25f, 16.75f, 4.65f, 6.65f, 3f, 1f, 2f };
    private List<float> back1 = new List<float>() { 1.25f, 8.75f, 2.4f, 4.4f, 3f, 2f, 1f };
    private List<float> back2 = new List<float>() { 9.25f, 16.75f, 2.4f, 4.4f, 3f, 2f, 2f };
    private List<float> up1 = new List<float>() { 1.25f, 8.75f, 0.15f, 2.15f, 3f, 3f, 1f };
    private List<float> up2 = new List<float>() { 9.25f, 16.75f, 0.15f, 2.15f, 3f, 3f, 2f };
    private List<float> down1 = new List<float>() { 1.25f, 8.75f, -2.1f, -0.1f, 3f, 4f, 1f };
    private List<float> down2 = new List<float>() { 9.25f, 16.75f, -2.1f, -0.1f, 3f, 4f, 2f };
    private List<float> right1 = new List<float>() { 1.25f, 8.75f, -6.6f, -4.6f, 3f, 6f, 1f };
    private List<float> right2 = new List<float>() { 9.25f, 16.75f, -6.6f, -4.6f, 3f, 6f, 2f };
    private List<float> left1 = new List<float>() { 1.25f, 8.75f, -4.35f, -2.35f, 3f, 5f, 1f };
    private List<float> left2 = new List<float>() { 9.25f, 16.75f, -4.35f, -2.35f, 3f, 5f, 2f };
    private List<float> controlsExit = new List<float>() { -13.5f, 3.5f, -9.5f, -7.5f, 3f, 7f, 1f };
    private List<float> controlsReset = new List<float>() { 5f, 15f, -9.5f, -7.5f, 3f, 7f, 2f };


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
        sceneClickables.Add(playButton);
        sceneClickables.Add(settingsButton);
        sceneClickables.Add(fullscreen);
        sceneClickables.Add(music);
        sceneClickables.Add(effects);
        sceneClickables.Add(keyboardSettings);
        sceneClickables.Add(gamepadSettings);
        sceneClickables.Add(language);
        sceneClickables.Add(settingsExit);
        sceneClickables.Add(fullscreenLeft);
        sceneClickables.Add(fullscreenRight);
        sceneClickables.Add(musicLeft);
        sceneClickables.Add(musicRight);
        sceneClickables.Add(effectsLeft);
        sceneClickables.Add(effectsRight);
        sceneClickables.Add(languageLeft);
        sceneClickables.Add(languageRight);
        sceneClickables.Add(select1);
        sceneClickables.Add(select2);
        sceneClickables.Add(back1);
        sceneClickables.Add(back2);
        sceneClickables.Add(up1);
        sceneClickables.Add(up2);
        sceneClickables.Add(down1);
        sceneClickables.Add(down2);
        sceneClickables.Add(right1);
        sceneClickables.Add(right2);
        sceneClickables.Add(left1);
        sceneClickables.Add(left2);
        sceneClickables.Add(controlsExit);
        sceneClickables.Add(controlsReset);

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
        if (clickAnalysis.x != 0)
        {
            if (menuLayer == 1)
            {
                mainMenuPosition = clickAnalysis;
            }
            else if (menuLayer == 2)
            {
                if (clickAnalysis.y == -1)
                {
                    Move(Util.Direction.left);
                }
                else if (clickAnalysis.y == 2)
                {
                    Move(Util.Direction.right);
                }
                else
                {
                    settingsPosition = clickAnalysis;
                }
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
        Vector2Int pointAnalysis = Util.ReturnPositionFromMouse(pointerLocation, menuLayer, sceneClickables);
        if (pointAnalysis.x != 0)
        {
            if (menuLayer == 1)
            {
                mainMenuPosition = pointAnalysis;
            }
            else if (menuLayer == 2)
            {
                settingsPosition = pointAnalysis;
            }
            else if (menuLayer == 3)
            {
                controlsPosition = pointAnalysis;
            }
            UpdateSelected();
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
