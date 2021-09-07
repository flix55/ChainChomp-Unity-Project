using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoBehaviour
{
    [Header("ParticalCalls")]
    public Animator anim;
    [Space(10)]
    public GameObject particals;
    public int animationStateNumber;

    void Update()
    {
        TriggerAnimationState(animationStateNumber);
    }

    void TriggerAnimationState(int animationState)
    {
        if (anim.GetInteger("animationState") == 1)
        {
            particals.SetActive(true);
        }
        else
        {
            particals.SetActive(false);
        }
    }
}
