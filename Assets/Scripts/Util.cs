using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public enum Direction { up, down, left, right};

    public static Vector2Int ReturnPositionFromMouse(Vector2 mousePosition, int currentSceneLayer, List<List<float>> sceneClickables)
    {
        foreach(List<float> sceneClickable in sceneClickables)
        {
            if (Mathf.RoundToInt(sceneClickable[4]) == currentSceneLayer)
            {
                if (mousePosition.x >= sceneClickable[0] && mousePosition.x <= sceneClickable[1] && mousePosition.y >= sceneClickable[2] && mousePosition.y <= sceneClickable[3])
                {
                    return new Vector2Int(Mathf.RoundToInt(sceneClickable[5]), Mathf.RoundToInt(sceneClickable[6]));
                }
            }
        }
        return new Vector2Int(0, 0);
    }

    public static string GetPaletteFont(string paletteNum)
    {
        if (paletteNum.Equals("1"))
        {
            return "9dab86";
        }
        else if (paletteNum.Equals("2"))
        {
            return "f3eded";
        }
        else
        {
            return "682c0e";
        }
    }

    public static string GetPaletteAccentOne(string paletteNum)
    {
        if (paletteNum.Equals("1"))
        {
            return "e08f62";
        }
        else if (paletteNum.Equals("2"))
        {
            return "df8931";
        }
        else
        {
            return "fc8621";
        }
    }

    public static string GetPaletteAccentTwo(string paletteNum)
    {
        if (paletteNum.Equals("1"))
        {
            return "d7c79e";
        }
        else if (paletteNum.Equals("2"))
        {
            return "f5c16c";
        }
        else
        {
            return "c24914";
        }
    }

    public static string GetPaletteBackground(string paletteNum)
    {
        if (paletteNum.Equals("1"))
        {
            return "a35638";
        }
        else if (paletteNum.Equals("2"))
        {
            return "aa530e";
        }
        else
        {
            return "f9e0ae";
        }
    }

    public static Color ToColor(this string color)
    {
        if (color.StartsWith("#", StringComparison.InvariantCulture))
        {
            color = color.Substring(1); // strip #
        }

        if (color.Length == 6)
        {
            color += "FF"; // add alpha if missing
        }

        var hex = Convert.ToUInt32(color, 16);
        var r = ((hex & 0xff000000) >> 0x18) / 255f;
        var g = ((hex & 0xff0000) >> 0x10) / 255f;
        var b = ((hex & 0xff00) >> 8) / 255f;
        var a = ((hex & 0xff)) / 255f;

        return new Color(r, g, b, a);
    }
}
