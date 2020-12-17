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
}
