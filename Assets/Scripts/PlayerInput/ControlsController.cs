using System.Collections;
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

    public enum ControlsOptions { selectOne, selectTwo, backOne, backTwo, upOne, upTwo, rightOne, rightTwo, downOne, downTwo, leftOne, leftTwo, exit, reset };

    public TextMeshPro controlsTitle;
    public TextMeshPro select1;
    public TextMeshPro select2;
    public TextMeshPro back1;
    public TextMeshPro back2;
    public TextMeshPro up1;
    public TextMeshPro up2;
    public TextMeshPro right1;
    public TextMeshPro right2;
    public TextMeshPro down1;
    public TextMeshPro down2;
    public TextMeshPro left1;
    public TextMeshPro left2;
    public TextMeshPro exit;
    public TextMeshPro reset;

    void Start()
    {
        foreach (GameObject controlsObject in GameObject.FindGameObjectsWithTag("controls"))
        {
            if (controlsObject.GetComponent<TextMeshPro>() != null)
            {
                TextMeshPro TMPTextObject = controlsObject.GetComponent<TextMeshPro>();
                textControlObjects.Add(TMPTextObject);
                if (controlsObject.GetComponent<ControlsBindingText>() != null)
                {
                    controlsBindingTexts.Add(controlsObject.GetComponent<ControlsBindingText>());
                }
            }
        }
        HideControls();
    }

    public void ShowControls(bool keyboard)
    {
        keyboardControlsShown = keyboard;
        controlsEnabled = true;
        if (keyboardControlsShown)
        {
            controlsTitle.text = LocalizationManager.GetLocalizedValue("keyboard_controls"); ;
        }
        else
        {
            controlsTitle.text = LocalizationManager.GetLocalizedValue("gamepad_controls");
        }

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

    public void ReDisplayCorrectBindings()
    {
        foreach(ControlsBindingText textToReDisplay in controlsBindingTexts)
        {
            textToReDisplay.UpdateDisplayText();
        }
    }

    public void RemapSelectedControl(ControlsOptions curControlSelected)
    {
        if (curControlSelected == ControlsOptions.selectOne)
        {
            select1.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.selectTwo)
        {
            select2.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.backOne)
        {
            back1.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.backTwo)
        {
            back2.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.upOne)
        {
            up1.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.upTwo)
        {
            up2.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.rightOne)
        {
            right1.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.rightTwo)
        {
            right2.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.leftOne)
        {
            left1.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.leftTwo)
        {
            left2.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.downOne)
        {
            down1.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
        else if (curControlSelected == ControlsOptions.downTwo)
        {
            down2.gameObject.GetComponent<ControlsBindingText>().StartRebinding();
        }
    }
}
