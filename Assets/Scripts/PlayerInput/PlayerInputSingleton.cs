using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSingleton : MonoBehaviour
{
    public static PlayerInputSingleton Instance;

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
}
