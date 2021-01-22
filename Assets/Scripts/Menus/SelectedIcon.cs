using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedIcon : MonoBehaviour
{
    public enum SelectedDirection { left, right };
    public SelectedDirection iconDirection;
    public Sprite underline;
    public Sprite icon;
    private SpriteRenderer selectedSpriteRenderer;

    void Awake()
    {
        selectedSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void UpdateSelectedIconPosition(int layer, int x, int y)
    {
        selectedSpriteRenderer.enabled = true;
        if (iconDirection == SelectedDirection.left)
        {
            if (layer == 1)
            {
                selectedSpriteRenderer.sprite = icon;
                if (x == 1)
                {
                    gameObject.transform.position = new Vector3(-5f, -1.75f, 0f);
                }
                else if (x == 2)
                {
                    gameObject.transform.position = new Vector3(-2.75f, -8f, 0f);
                }
            }
            else if (layer == 2)
            {
                if (x < 7)
                {
                    selectedSpriteRenderer.sprite = underline;
                    if (x == 1)
                    {
                        gameObject.transform.position = new Vector3(-8f, 4.75f, 0f);
                    }
                    else if (x == 2)
                    {
                        gameObject.transform.position = new Vector3(-8f, 2.5f, 0f);
                    }
                    else if (x == 3)
                    {
                        gameObject.transform.position = new Vector3(-8f, 0.25f, 0f);
                    }
                    else if (x == 4)
                    {
                        gameObject.transform.position = new Vector3(-8f, -2f, 0f);
                    }
                    else if (x == 5)
                    {
                        gameObject.transform.position = new Vector3(-8f, -4.25f, 0f);
                    }
                    else if (x == 6)
                    {
                        gameObject.transform.position = new Vector3(-8f, -6.5f, 0f);
                    }
                }
                else if (x == 7)
                {
                    selectedSpriteRenderer.sprite = icon;
                    gameObject.transform.position = new Vector3(-2.9f, -8.5f, 0f);
                }
            }
        }
        else
        {
            // RIGHT
            if (layer == 1)
            {
                selectedSpriteRenderer.sprite = icon;
                if (x == 1)
                {
                    gameObject.transform.position = new Vector3(5f, -1.75f, 0f);
                }
                else if (x == 2)
                {
                    gameObject.transform.position = new Vector3(2.75f, -8f, 0f);
                }
            }
            else if (layer == 2)
            {
                if (x < 7)
                {
                    selectedSpriteRenderer.enabled = false;
                }
                else if (x == 7)
                {
                    selectedSpriteRenderer.sprite = icon;
                    gameObject.transform.position = new Vector3(2.9f, -8.48f, 0f);
                }
            }
        }
    }
}
