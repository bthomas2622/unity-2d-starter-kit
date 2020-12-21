using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance;
    public PlayerInput playerInput;
    private static string defaultBindings = "defaultBindings";
    private static string playerBindings = "playerBindings";
    private static string colorPalette = "colorPalette";

    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            string launchBindings = playerInput.actions.SaveBindingOverridesAsJson();
            if (!PlayerPrefs.HasKey(defaultBindings))
            {
                PlayerPrefs.SetString(defaultBindings, launchBindings);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey(playerBindings))
            {
                PlayerPrefs.SetString(playerBindings, launchBindings);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey(colorPalette))
            {
                PlayerPrefs.SetString(colorPalette, 1.ToString());
                PlayerPrefs.Save();
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RestoreControlDefaults()
    {
        string controlDefaultBindings = PlayerPrefs.GetString(defaultBindings);
        PlayerPrefs.SetString(playerBindings, controlDefaultBindings); ;
        PlayerPrefs.Save();
    }

    public void SaveUserRebinds()
    {
        string rebinds = playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    public string GetColorPalette()
    {
        return PlayerPrefs.GetString(colorPalette);
    }

    public void SetColorPalette(string paletteNum)
    {
        if (paletteNum.Equals("1") || paletteNum.Equals("2") || paletteNum.Equals("3"))
        {
            PlayerPrefs.SetString(colorPalette, paletteNum);
            PlayerPrefs.Save();
        }
    }
}
