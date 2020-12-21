using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface SceneController
{
    void Move(Util.Direction direction);
    void Select();
    void Back();
    void Click(Vector2 vector2);
    void Point(Vector2 vector2);
}
