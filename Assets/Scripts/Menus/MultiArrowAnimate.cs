using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiArrowAnimate : MonoBehaviour
{
    private Animator arrowAnimator;

    void Start()
    {
        arrowAnimator = gameObject.GetComponent<Animator>();        
    }

    public void FlickerArrow(bool left)
    {
        if (left)
        {
            arrowAnimator.Play("MultiLeftArrowFlicker", -1, 0f);
        }
        else
        {
            arrowAnimator.Play("MultiRightArrowFlicker", -1, 0f);
        }
    }
}
