using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ControlsBindingText : MonoBehaviour
{
    public string actionId;
    public string keyboardBindingId;
    public string gamepadBindingId;
    private bool keyboardDisplayStatus = false;
    private PlayerInput playerInput;
    private TextMeshPro objectText;
    private InputAction bindingAction;
    private static string empty = "";

    void Start()
    {
        playerInput = PlayerInputSingleton.Instance.GetComponent<PlayerInput>();
        objectText = gameObject.GetComponent<TextMeshPro>();
        bindingAction = playerInput.actions.FindAction(actionId);
        UpdateDisplayText();
    }

    private void UpdateDisplayText()
    {
        string displayText = empty;
        string bindingId = keyboardDisplayStatus ? keyboardBindingId : gamepadBindingId;
        foreach(InputBinding binding in bindingAction.bindings)
        {
            if (binding.id.ToString().Equals(bindingId))
            {
                displayText = binding.ToDisplayString();
                break;
            }
        }
        objectText.text = displayText;
    }

    public void SetKeyboardDisplayStatus(bool status)
    {
        keyboardDisplayStatus = status;
        UpdateDisplayText();
    }
}
