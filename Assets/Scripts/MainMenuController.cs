using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour, SceneController
{
    public void Move(InputAction.CallbackContext ctx)
    {
        Debug.Log("Move");
    }

    public void Select(InputAction.CallbackContext ctx)
    {
        Debug.Log("Select");
    }

    public void Back(InputAction.CallbackContext ctx)
    {
        Debug.Log("Back");
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        Debug.Log("Click");
    }

    public void Point(InputAction.CallbackContext ctx)
    {
        Debug.Log("Point");
    }
}
