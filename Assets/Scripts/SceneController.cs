using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface SceneController
{
    void Move(InputAction.CallbackContext ctx);
    void Select(InputAction.CallbackContext ctx);
    void Back(InputAction.CallbackContext ctx);
    void Click(InputAction.CallbackContext ctx);
    void Point(InputAction.CallbackContext ctx);
}
