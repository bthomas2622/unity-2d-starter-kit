using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsController : MonoBehaviour
{
    private bool controlsEnabled = false;
    private bool keyboardControlsShown = false;
    
    public bool ControlsEnabled { get => controlsEnabled; }

    public void ShowControls(bool keyboard)
    {
        keyboardControlsShown = keyboard;
        controlsEnabled = true;
    }

    public void HideControls()
    {
        controlsEnabled = true;

    }
}
