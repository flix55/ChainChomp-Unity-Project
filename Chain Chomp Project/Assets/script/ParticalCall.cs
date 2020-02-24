using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalCall : MonoBehaviour
{

    public ParticleSystem[] particalEffectLand;

    public void PLayParticall()
    {
        foreach (var item in particalEffectLand)
        {
            item.Play();
        }
    }

}
