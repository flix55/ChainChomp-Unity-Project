using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalCallFromAnimation : MonoBehaviour
{

    public ParticleSystem[] particalEffectLand;
    public GameObject eatingPartical;

    public void PLayParticall()
    {
        foreach (var item in particalEffectLand)
        {
            item.Play();
        }
    }
    public void PlayEatingPartical()
    {
        StartCoroutine(WaitAndPrint(0.5f));
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        eatingPartical.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        eatingPartical.SetActive(false);
    }

}
