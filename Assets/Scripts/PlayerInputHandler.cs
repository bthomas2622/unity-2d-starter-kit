using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    private SceneController currentSceneController;

    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        currentSceneController = GameObject.FindGameObjectWithTag("sceneController").GetComponent<SceneController>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        currentSceneController.Move(ctx);
    }

    public void Select(InputAction.CallbackContext ctx)
    {
        currentSceneController.Select(ctx);
    }

    public void Back(InputAction.CallbackContext ctx)
    {
        currentSceneController.Back(ctx);
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        currentSceneController.Click(ctx);
    }

    public void Point(InputAction.CallbackContext ctx)
    {
        currentSceneController.Point(ctx);
    }
}
