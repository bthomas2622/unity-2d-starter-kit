using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackdrop : MonoBehaviour
{
    public Sprite P1;
    public Sprite P2;
    public Sprite P3;
    private SpriteRenderer menuBackdropSpriteRenderer;

    void Start()
    {
        menuBackdropSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        menuBackdropSpriteRenderer.sprite = (PlayerSettings.Instance.GetColorPalette()) switch
        {
            "1" => P1,
            "2" => P2,
            _ => P3,
        };
    }

    public void UpdatePalette()
    {
        menuBackdropSpriteRenderer.sprite = (PlayerSettings.Instance.GetColorPalette()) switch
        {
            "1" => P1,
            "2" => P2,
            _ => P3,
        };
    }
}
