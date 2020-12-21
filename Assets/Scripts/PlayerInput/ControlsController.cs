using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControlsController : MonoBehaviour
{
    private bool controlsEnabled = false;
    private bool keyboardControlsShown = false;
    
    public bool ControlsEnabled { get => controlsEnabled; }
    private List<TextMeshPro> textControlObjects = new List<TextMeshPro>();
    private PlayerInput playerInput;

    private static string moveActionId = "3e88efde-e611-4634-9a08-d21834f90a72";
    private static string moveKeyboardId = "00ca640b-d935-4593-8157-c05846ea39b3";
    private static string keyboardUpOneId = "e2062cb9-1b15-46a2-838c-2f8d72a0bdd9";
    private static string keyboardUpTwoId = "8180e8bd-4097-4f4e-ab88-4523101a6ce9";
    private static string keyboardDownOneId = "320bffee-a40b-4347-ac70-c210eb8bc73a";
    private static string keyboardDownTwoId = "1c5327b5-f71c-4f60-99c7-4e737386f1d1";
    private static string keyboardRightOneId = "fcfe95b8-67b9-4526-84b5-5d0bc98d6400";
    private static string keyboardRightTwoId = "77bff152-3580-4b21-b6de-dcd0c7e41164";
    private static string keyboardLeftOneId = "d2581a9b-1d11-4566-b27d-b92aff5fabbc";
    private static string keyboardLeftTwoId = "2e46982e-44cc-431b-9f0b-c11910bf467a";
    private static string moveGamepadId = "89393826-7516-4be7-933d-3e8a9e96f7e6";
    private static string gamepadUpOneId = "c46af795-6f26-4bcf-ab2b-bb793358ab6a";
    private static string gamepadUpTwoId = "d62cc885-8117-44b4-99fe-33c732c93872";
    private static string gamepadDownOneId = "101c3c09-e09a-4d2b-9e39-fc8e59fbf7ac";
    private static string gamepadDownTwoId = "e230a9a0-51eb-4597-8138-a19805f8f4ca";
    private static string gamepadRightOneId = "9beba9a3-f4b7-4be3-8e23-8e8e8d276e2b";
    private static string gamepadRightTwoId = "ca691eb3-f1e5-4ba4-a4bf-75274b5408bd";
    private static string gamepadLeftOneId = "2c860c85-3685-4b47-9fd5-5c11d697b1a3";
    private static string gamepadLeftTwoId = "311f1858-07fd-4725-8919-c55dfaad9010";
    private static string selectActionId = "88bc1e0d-de60-4bcf-9cf8-cdc49aad8864";
    private static string keyboardSelectOneId = "5adc3629-ce0c-4502-95c0-d377501c279c";
    private static string keyboardSelectTwoId = "747514c8-aa94-4965-8bb3-3f9a184f98e4";
    private static string gamepadSelectOneId = "f0d7bcbf-2cb4-4af8-8c3c-7bd7a6c64ac3";
    private static string gamepadSelectTwoId = "6913de49-9602-4a3b-8fd3-c6b49f12cffc";
    private static string backActionId = "42ad320b-3dfc-4de1-9ec5-bcadd757a34c";
    private static string keyboardBackOneId = "85ae8bb7-4c5d-4e7d-a578-66079a6ffc46";
    private static string keyboardBackTwoId = "522c1fdd-5fe5-4a48-9439-85e7d65a3d7f";
    private static string gamepadBackOneId = "34120be3-4d57-426d-8d20-ca1a317fd624";
    private static string gamepadBackTwoId = "e12aa318-dc1b-49ca-ac39-314f1b425921";

    void Start()
    {
        playerInput = PlayerInputSingleton.Instance.GetComponent<PlayerInput>();
        foreach (GameObject controlsObject in GameObject.FindGameObjectsWithTag("controls"))
        {
            if (controlsObject.GetComponent<TextMeshPro>() != null)
            {
                textControlObjects.Add(controlsObject.GetComponent<TextMeshPro>());
            }
        }
        //foreach (InputAction action in playerInput.currentActionMap.actions)
        //{
        //    //Debug.Log(action.id);
        //    //Debug.Log(action.name);
        //    foreach (InputBinding binding in action.bindings)
        //    {
        //        //Debug.Log(binding.id);
        //        //Debug.Log(binding.name);
        //        //Debug.Log(action.GetBindingDisplayString(binding));
        //    }
        //}
        //var bindingIndex = action.GetBindingIndex(InputBinding.MaskByGroup("Gamepad"));
        //Debug.Log(action.GetBindingDisplayString(bindingIndex););
    }

    public void ShowControls(bool keyboard)
    {
        keyboardControlsShown = keyboard;
        controlsEnabled = true;
        foreach(TextMeshPro controlsText in textControlObjects)
        {
            controlsText.enabled = true;
        }
    }

    public void HideControls()
    {
        controlsEnabled = true;
        foreach (TextMeshPro controlsText in textControlObjects)
        {
            controlsText.enabled = false;
        }
    }
}
