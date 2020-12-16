using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance;
    private static string kSelect1, kSelect2, kBack1, kBack2, kUp1, kUp2, kDown1, kDown2, kRight1, kRight2, kLeft1, kLeft2;
    private static string gSelect1, gSelect2, gBacg1, gBacg2, gUp1, gUp2, gDown1, gDown2, gRight1, gRight2, gLeft1, gLeft2;

    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetControlDefaults(bool initializing, bool keyboard)
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetString("musicVolume", "kSelect1Default"); ;
            PlayerPrefs.Save();
        }
    }

    public void SaveUserRebinds(PlayerInput player)
    {
        // var rebinds = player.actions.SaveBindingOverridesAsJson();
        // PlayerPrefs.SetString("rebinds", rebinds);
    }

    public void LoadUserRebinds(PlayerInput player)
    {
        // var rebinds = PlayerPrefs.GetString("rebinds");
        // player.actions.LoadBindingOverridesFromJson(rebinds);
    }
}
