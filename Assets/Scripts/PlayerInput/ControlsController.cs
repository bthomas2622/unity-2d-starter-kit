﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControlsController : MonoBehaviour
{
    private bool controlsEnabled = false;
    private bool keyboardControlsShown = false;
    
    public bool ControlsEnabled { get => controlsEnabled; }
    private List<TextMeshPro> textControlObjects = new List<TextMeshPro>();
    private List<ControlsBindingText> controlsBindingTexts = new List<ControlsBindingText>();

    void Start()
    {
        foreach (GameObject controlsObject in GameObject.FindGameObjectsWithTag("controls"))
        {
            if (controlsObject.GetComponent<TextMeshPro>() != null)
            {
                TextMeshPro TMPTextObject = controlsObject.GetComponent<TextMeshPro>();
                TMPTextObject.color = Util.ToColor(Util.GetPaletteFont(PlayerSettings.Instance.GetColorPalette()));
                textControlObjects.Add(TMPTextObject);
                if (controlsObject.GetComponent<ControlsBindingText>() != null)
                {
                    controlsBindingTexts.Add(controlsObject.GetComponent<ControlsBindingText>());
                }
            }
        }
    }

    public void ShowControls(bool keyboard)
    {
        keyboardControlsShown = keyboard;
        controlsEnabled = true;
        foreach(ControlsBindingText controlsBindingText in controlsBindingTexts)
        {
            controlsBindingText.SetKeyboardDisplayStatus(keyboard);
        }
        foreach(TextMeshPro controlsText in textControlObjects)
        {
            controlsText.enabled = true;
        }
    }

    public void HideControls()
    {
        controlsEnabled = true;
        foreach (TextMeshPro controlsText in textControlObjects)
        {
            controlsText.enabled = false;
        }
    }
}