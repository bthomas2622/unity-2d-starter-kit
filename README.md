# Unity 2020 2D Starter Kit

This project is designed to help people get up and running on a 2D Unity project with less busy work. It is simply a main menu ready to go with input handling, player settings, audio, localization, 2D camera, etc.

*Disclaimer*: Even though I did my best to provide context about the contents of the starter kit in this README, this is not a step-by-step tutorial. It is a Unity Project that can be explored to learn/leverage the fundamentals contained. 

Feel free to reach out with questions I am [@freebrunch](https://twitter.com/freebrunch) on Twitter.

## Features
1. **New Unity Input System** configured for mouse + keyboard + gamepad input and remappable controls
2. **Player Settings** capable of saving preferences for fullscreen/windowed, music volume, effects volume, keyboard mappings, gamepad mappings, and language.
3. **Audio Manager** for playing music and sounds   
4. **Localization Manager** for seemless switching between languages

# Input System

### Helpful Links
1. [Dapper Dino - Key rebinding tutorial for new Unity Input System](https://youtu.be/dUCcZrPhwSo)
2. [Official new Unity Input System Docs](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/QuickStartGuide.html)

I used the [new Unity Input System](https://blogs.unity3d.com/2019/10/14/introducing-the-new-input-system/) for handling player input. It abstracts from specific hardware making it easy to define behavior for any mouse, keyboard, or gamepad regardless of hardware differences. 

The new Unity Input System holds mappings in a dedicated **Input Action** asset found in the *Assets/Actions* folder in either C# or Unity file form. You can double click the Unity file where you can use the GUI to define **Actions** then map forms of input to them. 

In my scene I have a **PlayerInput** (Singleton) game object with a *Player Input* component where this Input Action asset is assigned and configured to *"Invoke Unity Events"*. This project has keyboard/gamepad actions defined for:

1. Move (Up, Down, Left Right)
2. Select 
3. Back

And mouse actions defined for point and click. 

Then I have a **PlayerInputHandler** (Singleton) game object that listens to the player input events and passes them along to the **"Scene Controller"** to situationally do what is needed with the input. A "Scene Controller" is a pattern I use to respond to the input differently in each Unity scene. 

### Remapping Controls

I have a script called **ControlsBindingText.cs** that is placed on text game objects in the controls view. Each object corresponds to a different input mapping (select, up, etc.). When one of these mappings is "selected" the ControlsBindingText.cs - *StartRebinding()* function is called. 

```
    public void StartRebinding()
    {
        objectText.text = empty;
        bindingAction.Disable();
        if (keyboardDisplayStatus)
        {
            rebindingOperation = bindingAction.PerformInteractiveRebinding(keyboardBindingIndex)
                    .WithControlsExcluding("Mouse")
                    .WithControlsExcluding("Gamepad")
                    .OnMatchWaitForAnother(0.1f)
                    .OnComplete(operation => RebindComplete())
                    .Start();
        }
        else
        {
            rebindingOperation = bindingAction.PerformInteractiveRebinding(gamepadBindingIndex)
                .WithControlsExcluding("Mouse")
                .WithControlsExcluding("Keyboard")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete())
                .Start();
        }
    }
```

The most confusing part of this script are the *bindingId*'s and *bindingIndex*'s. Essentially for the new Unity Input System every binding for an "Action" has a unique ID associated with it. I entered these ID's from the UI into the public string field in their corresponding *ControlsBindingText.cs* objects so they remapped the correct Action binding in the Unity Input Action asset. The only way I can get all the bindings on an Action is as an Array so I also track which "Index" in that Actions "Bindings" array each of those Id's matches. 

Hopefully the script makes more sense than that paragraph.

### Saving the Controls

The **PlayerSettings.cs** (Singleton) script has a public reference to the **PlayerInput** asset that holds the new Unity Input System config. 

It leverages the PlayerInput asset to save bindings as Json in string form.

```
string rebinds = playerInput.actions.SaveBindingOverridesAsJson();
PlayerPrefs.SetString(playerBindings, rebinds);
PlayerPrefs.Save();
```

It then can also override the current PlayerInput asset with a saved config. 

`PlayerInputSingleton.Instance.gameObject.GetComponent<PlayerInput>().actions.LoadBindingOverridesFromJson(controlDefaultBindings);`

# Player Settings

The **PlayerSettings.cs** (Singleton) script is pretty self explanatory. It utilizeds Unity `PlayerPrefs` to Save/Load player settings. And contains a bunch of helper methods to retrieve/set those saved settings.

### Settings managed
1. Music Volume
2. Effects Volume
3. Language Chosen
4. Player Control Mapping

# Audio 

The Audio system is also pretty self explanatory. The **AudioManager** game object (Singleton) contains 9 *Audio Source* components. The first Audio Source is dedicated to music. And the subsequent 8 are for sound effects. The **AudioManager.cs** script on the *AudioManager* game object is accessed from anywhere to play clips, and it cycles through the array of Audio Sources configuring and playing them. It sets their volume based on Player Settings. It also contains some cached sounds used often.  

# Localization

### Helpful Links
1. [Game Dev Guide - Youtube tutorial for script that imports localization csv](https://youtu.be/c-dzg4M20wY)
2. [Zolran - Creating TextMeshPro font asset with fallback fonts for different language characters](https://youtu.be/pLW2B98W5AU)

There are a lot of routes to get dynamic localization for you Unity project. This is a very simple one. This solution takes in a localization spreadsheet. Each column in the spreadsheet corresponds to a different language. And each row is a translation of some text, identifiable by a key in the first column. Make sure that each cell of translation is bookended by quotation marks.

### Languages used in this starter kit project
1. English
2. Spanish (obtained through google translate)
3. Simplified Chinese (obtained through google translate)

To use the method in this tool you export your localization spreadsheet as a *.csv* file and save in the **Resources/** folder. The **CSVLoader.cs** script takes in the spreadsheet and saves all the text in a dictionary with a key for each language. 

The **LocalizationManager** gameobject / script then grabs each key on the CSVLoader and saves the contents in a language specific dictionary.

```
    localizedEN = csvLoader.GetDictionaryValues("en");
    localizedES = csvLoader.GetDictionaryValues("es");
    localizedSC = csvLoader.GetDictionaryValues("sc");
```

Now by using the key for the text field you want to get the localized text for, you can query the LocalizationManager to get the string you seek.

```
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
```

Every text object in the project has a **LocalizedText.cs** script assigned that calls the **LocalizationManager** to get it's text localized to the current selected language. 

### Handling different language characters

When I first implemented this localization system all of the chinese characters for Simplified Chinese language chosen were displayed as squares. This was because the font asset each TextMeshPro game object was using did not support chinese characters. 

I decided to import the Google created "Noto Sans" font via *.otf* files downloaded from Google Fonts. The "Noto" series of fonts is an initiative by google to create a font family that supports almost all known languages. Find all the Noto fonts in the **Assets/Fonts** folder. I then created TextMeshPro font assets from each of these fonts. Designated the **BASE-Noto Sans-Regular SDF** TextMeshPro font asset as the font asset for every text object. Then edited the **BASE-Noto Sans-Regular SDF** asset to designate it's *Fallback Font Assets* to include all the other Noto family font assets (japanese, korean, etc.) so that if it ever didn't have the language character supported it would search its fallbacks. 

# In Conclusion

I hope this additional context about the project is helpful! I didn't want to dive deep into every possible aspect but rather give a surface level overview to the tools presented by the starter kit.

Hope this project helps you in some way!

Cheers,
Ben