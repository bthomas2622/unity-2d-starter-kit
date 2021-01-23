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
    private static string fullScreenMode = "fullscreenMode";
    private static int fullScreenModeDefault = 1;
    private static string musicVolume = "musicVolume";
    private static int musicVolumeDefault = 4;
    private static string effectsVolume = "effectsVolume";
    private static int effectsVolumeDefault = 5;
    private static string language = "language";
    private static int languageDefault = 1; // 1 - English, 2 - Spanish

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
            if (!PlayerPrefs.HasKey(fullScreenMode))
            {
                PlayerPrefs.SetInt(fullScreenMode, fullScreenModeDefault);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey(musicVolume))
            {
                PlayerPrefs.SetInt(musicVolume, musicVolumeDefault);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey(effectsVolume))
            {
                PlayerPrefs.SetInt(effectsVolume, effectsVolumeDefault);
                PlayerPrefs.Save();
            }
            if (!PlayerPrefs.HasKey(language))
            {
                PlayerPrefs.SetInt(language, languageDefault);
                PlayerPrefs.Save();
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        SetFullScreenSettings();
    }

    public void RestoreControlDefaults()
    {
        string controlDefaultBindings = PlayerPrefs.GetString(defaultBindings);
        PlayerPrefs.SetString(playerBindings, controlDefaultBindings); ;
        PlayerPrefs.Save();
        PlayerInputSingleton.Instance.gameObject.GetComponent<PlayerInput>().actions.LoadBindingOverridesFromJson(controlDefaultBindings);
    }

    public void SaveUserRebinds()
    {
        string rebinds = playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        PlayerPrefs.Save();
    }

    public void ChangeFullScreenMode(int newFullScreenMode)
    {
        if (newFullScreenMode == 1 || newFullScreenMode == 2)
        {
            PlayerPrefs.SetInt(fullScreenMode, newFullScreenMode);
            PlayerPrefs.Save();
        }
        SetFullScreenSettings();
    }

    public void ChangeMusicVolume(int newMusicVolume)
    {
        PlayerPrefs.SetInt(musicVolume, newMusicVolume);
        PlayerPrefs.Save();
    }

    public void ChangeEffectsVolume(int newEffectsVolume)
    {
        PlayerPrefs.SetInt(effectsVolume, newEffectsVolume);
        PlayerPrefs.Save();
    }

    public int GetFullScreenMode()
    {
        return PlayerPrefs.GetInt(fullScreenMode);
    }

    public int GetMusicVolume()
    {
        return PlayerPrefs.GetInt(musicVolume);
    }

    public int GetEffectsVolume()
    {
        return PlayerPrefs.GetInt(effectsVolume);
    }

    public int GetLanguage()
    {
        return PlayerPrefs.GetInt(language);
    }

    private void SetFullScreenSettings()
    {
        if (PlayerPrefs.GetInt(fullScreenMode) == 1)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (PlayerPrefs.GetInt(fullScreenMode) == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
