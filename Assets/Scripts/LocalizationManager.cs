using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language { English, Spanish, SimplifiedChinese };
    private static Language curLanguage = Language.English;
    public static Dictionary<string, string> localizedEN;
    public static Dictionary<string, string> localizedES;
    public static Dictionary<string, string> localizedSC;
    public static bool isInit;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Init();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetLanguage(PlayerSettings.Instance.GetLanguage());
        UpdateAllTexts();
    }

    public void UpdateAllTexts()
    {
        LocalizedText[] texts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText textObject in texts)
        {
            textObject.LocalizeTextObject();
        }
    }

    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localizedEN = csvLoader.GetDictionaryValues("en");
        localizedES = csvLoader.GetDictionaryValues("es");
        localizedSC = csvLoader.GetDictionaryValues("sc");

        isInit = true;
    }

    public static string GetLocalizedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;
        switch (curLanguage)
        {
            case Language.English:
                localizedEN.TryGetValue(key, out value);
                break;
            case Language.Spanish:
                localizedES.TryGetValue(key, out value);
                break;
            case Language.SimplifiedChinese:
                localizedSC.TryGetValue(key, out value);
                break;
            default:
                localizedEN.TryGetValue(key, out value);
                break;
        }
        return value;
    }

    public static void SetLanguage(int languageNum)
    {
        if (!isInit) { Init(); }

        if (languageNum == 1)
        {
            curLanguage = Language.English;
        }
        else if (languageNum == 2)
        {
            curLanguage = Language.Spanish;
        }
        else if (languageNum == 3)
        {
            curLanguage = Language.SimplifiedChinese;
        }
        else
        {
            curLanguage = Language.English;
        }
    }
}
