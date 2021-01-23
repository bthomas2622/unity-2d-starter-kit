using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ControlsBindingText : MonoBehaviour
{
    public string actionId;
    public string keyboardBindingId;
    private int keyboardBindingIndex;
    public string gamepadBindingId;
    private int gamepadBindingIndex;
    private bool keyboardDisplayStatus = false;
    private PlayerInput playerInput;
    private TextMeshPro objectText;
    private InputAction bindingAction;
    private static string empty = "";
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Start()
    {
        playerInput = PlayerInputSingleton.Instance.GetComponent<PlayerInput>();
        objectText = gameObject.GetComponent<TextMeshPro>();
        bindingAction = playerInput.actions.FindAction(actionId);
        int i = 0;
        foreach(InputBinding testing in bindingAction.bindings)
        {
            if (testing.id.ToString().Equals(keyboardBindingId))
            {
                keyboardBindingIndex = i;
            }
            if (testing.id.ToString().Equals(gamepadBindingId))
            {
                gamepadBindingIndex = i;
            }
            i += 1;
        }
        UpdateDisplayText();
    }

    public void UpdateDisplayText()
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

    private void RebindComplete()
    {
        UpdateDisplayText();
        rebindingOperation.Dispose();
        bindingAction.Enable();
    }
}
