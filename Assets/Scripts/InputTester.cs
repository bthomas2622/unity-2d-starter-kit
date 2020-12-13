using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTester : MonoBehaviour
{
    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.action.name.Equals("Move") && ctx.started)
        {
            Vector2 inputValue = ctx.ReadValue<Vector2>();
            if (inputValue.x == 1)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0);
            }
            else if (inputValue.x == -1)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0);
            }
            else if (inputValue.y == 1)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 0);
            }
            else if (inputValue.y == -1)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, 0);
            }
        }
    }

    public void Select(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) { return; }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, 0);
    }
}
