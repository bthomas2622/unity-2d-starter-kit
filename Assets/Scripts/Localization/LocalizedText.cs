using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LocalizedText : MonoBehaviour
{
    public string text_id;
    private TextMeshPro textObject;

    void Awake()
    {
        textObject = gameObject.GetComponent<TextMeshPro>();
        if (!SceneManager.GetActiveScene().Equals("MainMenu")) {
            LocalizeTextObject();
        }
    }

    public void LocalizeTextObject()
    {
        textObject.text = LocalizationManager.GetLocalizedValue(text_id);
    }
}
