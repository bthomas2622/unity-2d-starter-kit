using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiArrows : MonoBehaviour
{
    public GameObject rightArrowPrefab;
    public GameObject leftArrowPrefab;
    private GameObject rightArrow;
    private GameObject leftArrow;
    private SpriteRenderer rightArrowRenderer;
    private SpriteRenderer leftArrowRenderer;
    private MultiArrowAnimate leftArrowScript;
    private MultiArrowAnimate rightArrowScript;
    private float arrowOffset = 6f;

    void Awake()
    {
        rightArrow = Instantiate(rightArrowPrefab, transform.position, Quaternion.identity);
        leftArrow = Instantiate(leftArrowPrefab, transform.position, Quaternion.identity);
        rightArrowRenderer = rightArrow.GetComponent<SpriteRenderer>();
        leftArrowRenderer = leftArrow.GetComponent<SpriteRenderer>();
        rightArrowScript = rightArrow.GetComponent<MultiArrowAnimate>();
        leftArrowScript = leftArrow.GetComponent<MultiArrowAnimate>();
        SetArrowPositions();
        HideArrows();
    }

    public void SetPosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
        SetArrowPositions();
    }

    public void HideArrows()
    {
        leftArrowRenderer.enabled = false;
        rightArrowRenderer.enabled = false;
    }

    public void FlipArrows(bool flip)
    {
        if (flip)
        {
            leftArrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
            rightArrow.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            leftArrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
            rightArrow.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void SetRightArrow(bool shown)
    {
        rightArrowRenderer.enabled = shown;
    }

    public void SetLeftArrow(bool shown)
    {
        leftArrowRenderer.enabled = shown;
    }

    public void FlickerArrow(bool left)
    {
        if (left)
        {
            leftArrowScript.FlickerArrow(left);
        }
        else
        {
            rightArrowScript.FlickerArrow(left);
        }
    }

    private void SetArrowPositions()
    {
        rightArrow.transform.position = new Vector3(gameObject.transform.position.x + arrowOffset, gameObject.transform.position.y, 0);
        leftArrow.transform.position = new Vector3(gameObject.transform.position.x - arrowOffset, gameObject.transform.position.y, 0);
    }
}
