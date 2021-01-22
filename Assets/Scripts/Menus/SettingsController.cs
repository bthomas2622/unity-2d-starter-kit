using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsController : MonoBehaviour
{
    private bool settingsEnabled = false;

    public bool SettingsEnabled { get => settingsEnabled; }
    private List<TextMeshPro> textSettingsObjects = new List<TextMeshPro>();
    private MultiArrows multiArrows;
    public enum SettingOptions { fullscreen, musicVolume, effectsVolume, keyboardControls, gamepadControls, language, na };

    public TextMeshPro fullscreenChosenText;
    public TextMeshPro musicVolumeChosenText;
    public TextMeshPro effectsVolumeChosenText;
    public TextMeshPro keyboardRemapText;
    public TextMeshPro gamepadRemapText;
    public TextMeshPro languageChosenText;

    private int fullscreenChosen;
    private int musicChosen;
    private int effectsChosen;
    private int languageChosen;
    
    void Start()
    {
        foreach (GameObject settingsObject in GameObject.FindGameObjectsWithTag("settings"))
        {
            if (settingsObject.GetComponent<TextMeshPro>() != null)
            {
                TextMeshPro TMPTextObject = settingsObject.GetComponent<TextMeshPro>();
                textSettingsObjects.Add(TMPTextObject);
            }
        }
        multiArrows = GameObject.FindGameObjectWithTag("multiArrows").GetComponent<MultiArrows>();
        fullscreenChosen = PlayerSettings.Instance.GetFullScreenMode();
        musicChosen = PlayerSettings.Instance.GetMusicVolume();
        effectsChosen = PlayerSettings.Instance.GetEffectsVolume();
        languageChosen = PlayerSettings.Instance.GetLanguage();
        UpdateSettingsPerPreferences();
        HideSettings();
    }

    public void ShowSettings()
    {
        settingsEnabled = true;
        foreach (TextMeshPro settingsText in textSettingsObjects)
        {
            settingsText.enabled = true;
        }
        multiArrows.SetLeftArrow(true);
    }

    public void HideSettings()
    {
        settingsEnabled = true;
        foreach (TextMeshPro settingsText in textSettingsObjects)
        {
            settingsText.enabled = false;
        }
    }

    public void ChangeSettingSelected(SettingOptions settingOptionsSelected)
    {
        DisplayCorrectArrows(settingOptionsSelected);
        if (settingOptionsSelected == SettingOptions.fullscreen)
        {
            multiArrows.SetPosition(fullscreenChosenText.gameObject.transform.position);
        }
        else if (settingOptionsSelected == SettingOptions.musicVolume)
        {
            multiArrows.SetPosition(musicVolumeChosenText.gameObject.transform.position);
        }
        else if (settingOptionsSelected == SettingOptions.effectsVolume)
        {
            multiArrows.SetPosition(effectsVolumeChosenText.gameObject.transform.position);
        }
        else if (settingOptionsSelected == SettingOptions.keyboardControls)
        {
            multiArrows.SetPosition(keyboardRemapText.gameObject.transform.position);
        }
        else if (settingOptionsSelected == SettingOptions.gamepadControls)
        {
            multiArrows.SetPosition(gamepadRemapText.gameObject.transform.position);
        }
        else if (settingOptionsSelected == SettingOptions.language)
        {
            multiArrows.SetPosition(languageChosenText.gameObject.transform.position);
        }
    }

    private void DisplayCorrectArrows(SettingOptions settingOptionsSelected)
    {
        multiArrows.FlipArrows(false);
        if (settingOptionsSelected == SettingOptions.fullscreen)
        {
            if (fullscreenChosen == 1)
            {
                multiArrows.SetLeftArrow(false);
                multiArrows.SetRightArrow(true);
            }
            else
            {
                multiArrows.SetLeftArrow(true);
                multiArrows.SetRightArrow(false);
            }
        }
        else if (settingOptionsSelected == SettingOptions.musicVolume)
        {
            if (musicChosen > 0)
            {
                multiArrows.SetLeftArrow(true);
            }
            else
            {
                multiArrows.SetLeftArrow(false);
            }
            if (musicChosen < 10)
            {
                multiArrows.SetRightArrow(true);
            }
            else
            {
                multiArrows.SetRightArrow(false);
            }
        }
        else if (settingOptionsSelected == SettingOptions.effectsVolume)
        {
            if (effectsChosen > 0)
            {
                multiArrows.SetLeftArrow(true);
            }
            else
            {
                multiArrows.SetLeftArrow(false);
            }
            if (effectsChosen < 10)
            {
                multiArrows.SetRightArrow(true);
            }
            else
            {
                multiArrows.SetRightArrow(false);
            }
        }
        else if (settingOptionsSelected == SettingOptions.keyboardControls)
        {
            multiArrows.FlipArrows(true);
            multiArrows.SetLeftArrow(true);
            multiArrows.SetRightArrow(true);
        }
        else if (settingOptionsSelected == SettingOptions.gamepadControls)
        {
            multiArrows.FlipArrows(true);
            multiArrows.SetLeftArrow(true);
            multiArrows.SetRightArrow(true);
        }
        else if (settingOptionsSelected == SettingOptions.language)
        {
            multiArrows.HideArrows();
        }
        else
        {
            multiArrows.HideArrows();
        }
    }

    public void Left(SettingOptions settingOptionSelected) 
    {
        if (settingOptionSelected == SettingOptions.fullscreen)
        {
            if (fullscreenChosen == 2)
            {
                fullscreenChosen = 1;
                PlayerSettings.Instance.ChangeFullScreenMode(fullscreenChosen);
                fullscreenChosenText.text = ConvertPlayerSettingIntToString(fullscreenChosen, SettingOptions.fullscreen);
                DisplayCorrectArrows(settingOptionSelected);
            }
        }
        else if (settingOptionSelected == SettingOptions.musicVolume)
        {
            if (musicChosen > 0)
            {
                musicChosen -= 1;
                PlayerSettings.Instance.ChangeMusicVolume(musicChosen);
                musicVolumeChosenText.text = ConvertPlayerSettingIntToString(musicChosen, SettingOptions.musicVolume);
                DisplayCorrectArrows(settingOptionSelected);
                if (musicChosen > 0)
                {
                    multiArrows.FlickerArrow(true);
                }
            }
        }
        else if (settingOptionSelected == SettingOptions.effectsVolume)
        {
            if (effectsChosen > 0)
            {
                effectsChosen -= 1;
                PlayerSettings.Instance.ChangeMusicVolume(effectsChosen);
                effectsVolumeChosenText.text = ConvertPlayerSettingIntToString(effectsChosen, SettingOptions.effectsVolume);
                DisplayCorrectArrows(settingOptionSelected);
                if (effectsChosen > 0)
                {
                    multiArrows.FlickerArrow(true);
                }
            }
        }
    }

    public void Right(SettingOptions settingOptionSelected)
    {
        if (settingOptionSelected == SettingOptions.fullscreen)
        {
            if (fullscreenChosen == 1)
            {
                fullscreenChosen = 2;
                PlayerSettings.Instance.ChangeFullScreenMode(fullscreenChosen);
                fullscreenChosenText.text = ConvertPlayerSettingIntToString(fullscreenChosen, SettingOptions.fullscreen);
                DisplayCorrectArrows(settingOptionSelected);
            }
        }
        else if (settingOptionSelected == SettingOptions.musicVolume)
        {
            if (musicChosen < 10)
            {
                musicChosen += 1;
                PlayerSettings.Instance.ChangeMusicVolume(musicChosen);
                musicVolumeChosenText.text = ConvertPlayerSettingIntToString(musicChosen, SettingOptions.musicVolume);
                DisplayCorrectArrows(settingOptionSelected);
                if (musicChosen < 10)
                {
                    multiArrows.FlickerArrow(false);
                }
            }
        }
        else if (settingOptionSelected == SettingOptions.effectsVolume)
        {
            if (effectsChosen < 10)
            {
                effectsChosen += 1;
                PlayerSettings.Instance.ChangeMusicVolume(effectsChosen);
                effectsVolumeChosenText.text = ConvertPlayerSettingIntToString(effectsChosen, SettingOptions.effectsVolume);
                DisplayCorrectArrows(settingOptionSelected);
                if (effectsChosen < 10)
                {
                    multiArrows.FlickerArrow(false);
                }
            }
        }
    }

    private void UpdateSettingsPerPreferences()
    {
        fullscreenChosenText.text = ConvertPlayerSettingIntToString(fullscreenChosen, SettingOptions.fullscreen);
        musicVolumeChosenText.text = ConvertPlayerSettingIntToString(musicChosen, SettingOptions.musicVolume);
        effectsVolumeChosenText.text = ConvertPlayerSettingIntToString(effectsChosen, SettingOptions.effectsVolume);
        languageChosenText.text = ConvertPlayerSettingIntToString(languageChosen, SettingOptions.language);
    }

    private string ConvertPlayerSettingIntToString(int num, SettingOptions settingOption)
    {
        if (settingOption == SettingOptions.fullscreen)
        {
            if (num == 1)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        else if (settingOption == SettingOptions.musicVolume || settingOption == SettingOptions.effectsVolume)
        {
            return num.ToString();
        }
        else if (settingOption == SettingOptions.language)
        {
            if (num == 1)
            {
                return "English";
            }
            else
            {
                return "Espanol";
            }
        }
        else
        {
            return "";
        }
    }
}