using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystem : MonoBehaviour
{
    [Header("ParticalCalls")]
    public Animator anim;
    [Space(10)]

    //public List <ParticalsAtribute> particalsAtribute;
    public GameObject particals;
    public int animationStateNumber;
    //public GameObject eatingPartical;
    //private int whichParticallIndex;

    private void Start()
    {
       /* for (int i = 0; i < particalsAtribute.Count; i++)
        {
            particalsAtribute[i].particals.Stop();
        }*/


    }

    void Update()
    {
       // SetActivePartical();

        /* for (int i = 0; i < particalsAtribute.Count; i++)
         {
             TriggerAnimationState(particalsAtribute[i].animationStateNumber);
             whichParticallIndex = i;
         }*/

        TriggerAnimationState(animationStateNumber);

    }

    void TriggerAnimationState(int animationState)
    {
        /* if (anim.GetInteger("animationState") == animationState)
         {
             particalsAtribute[whichParticallIndex].particals.Play();
         }
         else
         {
             Debug.Log("which index call : " + whichParticallIndex);
             particalsAtribute[whichParticallIndex].particals.Stop();
         }*/

        if (anim.GetInteger("animationState") == 1)
        {
            particals.SetActive(true);
        }
        else
        {
            particals.SetActive(false);
        }
        
    }

    /*void SetActivePartical()
    {
        if (anim.GetInteger("animationState") == 3)
        {
            eatingPartical.SetActive(true);
        }
        else
        {
            eatingPartical.SetActive(false);
        }
    }*/
}
