using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour, SceneController
{
    private int menuLayer = 1;
    private Vector2Int layer1Position = new Vector2Int(1, 1);

    // layer, row, col, xbounds (left to right), ybounds (bottom to top)
    private List<float> settingsButton = new List<float>() { -1.4f, 1.4f, -9.4f, -6.6f, 1f, 3f, 2f};

    private List<List<float>> sceneClickables = new List<List<float>>();
    private ControlsController controlsController;

    void Awake()
    {
        layer1Position.x = 3;
        layer1Position.y = 2;
        sceneClickables.Add(settingsButton);
        controlsController = GameObject.Find("ControlsController").GetComponent<ControlsController>();
    }

    public void Move(Util.Direction direction)
    {
    }

    public void Select()
    {
        if (menuLayer == 1)
        {
            if (layer1Position.x == 3 && layer1Position.y == 2)
            {
                menuLayer += 1;
                controlsController.ShowControls(true);
            }
        }
    }

    public void Back()
    {
        if (menuLayer == 2)
        {
            menuLayer -= 1;
            controlsController.HideControls();
        }
    }

    public void Click(Vector2 clickLocation)
    {
        Vector2Int clickAnalysis = Util.ReturnPositionFromMouse(clickLocation, menuLayer, sceneClickables);
        if (clickLocation.x != 0)
        {
            if (menuLayer == 1)
            {
                layer1Position = clickAnalysis;
            }
            Select();
        }
    }

    public void Point(Vector2 pointerLocation)
    {

    }
}
