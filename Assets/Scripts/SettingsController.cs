using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsController : MonoBehaviour
{
    private bool settingsEnabled = false;

    public bool SettingsEnabled { get => settingsEnabled; }
    private List<TextMeshPro> textSettingsObjects = new List<TextMeshPro>();

    void Start()
    {
        foreach (GameObject controlsObject in GameObject.FindGameObjectsWithTag("controls"))
        {
            if (controlsObject.GetComponent<TextMeshPro>() != null)
            {
                TextMeshPro TMPTextObject = controlsObject.GetComponent<TextMeshPro>();
                textSettingsObjects.Add(TMPTextObject);
            }
        }
    }

    public void ShowSettings()
    {
        settingsEnabled = true;
        foreach (TextMeshPro settingsText in textSettingsObjects)
        {
            settingsText.enabled = true;
        }
    }

    public void HideSettings()
    {
        settingsEnabled = true;
        foreach (TextMeshPro settingsText in textSettingsObjects)
        {
            settingsText.enabled = false;
        }
    }
}
