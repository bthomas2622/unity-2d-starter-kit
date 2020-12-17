using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    private SceneController currentSceneController;
    private Vector2 cachedPointerLocation = new Vector2(0f, 0f);
    private Camera cachedMainCamera;

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
        cachedMainCamera = Camera.main;
        currentSceneController = GameObject.FindGameObjectWithTag("sceneController").GetComponent<SceneController>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        cachedMainCamera = Camera.main;
        currentSceneController = GameObject.FindGameObjectWithTag("sceneController").GetComponent<SceneController>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.started && ctx.action.name.Equals("Move"))
        {
            Vector2 inputValue = ctx.ReadValue<Vector2>();
            if (inputValue.x == 1)
            {
                currentSceneController.Move(Util.Direction.right);
            }
            else if (inputValue.x == -1)
            {
                currentSceneController.Move(Util.Direction.left);
            }
            else if (inputValue.y == 1)
            {
                currentSceneController.Move(Util.Direction.up);
            }
            else if (inputValue.y == -1)
            {
                currentSceneController.Move(Util.Direction.down);
            }
        }
    }

    public void Select(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) { return; }
        currentSceneController.Select();
    }

    public void Back(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) { return; }
        currentSceneController.Back();
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) { return; }
        currentSceneController.Click(cachedPointerLocation);
    }

    public void Point(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        cachedPointerLocation = cachedMainCamera.ScreenToWorldPoint(inputValue);
        currentSceneController.Point(cachedPointerLocation);
    }
}
