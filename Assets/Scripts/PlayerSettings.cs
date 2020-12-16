using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings Instance;
    public PlayerInput playerInput;
    private static string kSelect1, kSelect2, kBack1, kBack2, kUp1, kUp2, kDown1, kDown2, kRight1, kRight2, kLeft1, kLeft2;
    private static string gSelect1, gSelect2, gBacg1, gBacg2, gUp1, gUp2, gDown1, gDown2, gRight1, gRight2, gLeft1, gLeft2;
    private static string defaultBindings = "defaultBindings";
    private static string playerBindings = "playerBindings";

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
                PlayerPrefs.SetString(playerBindings, launchBindings); ;
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
}
