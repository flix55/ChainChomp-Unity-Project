using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDelay : MonoBehaviour
{
    ChompMouvement chompMouvement;
    public float howManyGhost = 10;
    public GameObject[] transparentChainChomp;
    public GameObject[] meshsForTransparentFade;
    public GameObject finalPosition;
    public Material matTransparent0;
    public Material matTransparent1;
    float[] countUp = new float[3];

    float timer;
    int queu;

    void Start()
    {
        chompMouvement = FindObjectOfType<ChompMouvement>();
    }

    void Update()
    {
        SeeChainChomp();
        Rotation();
    }

    void Rotation()
    {
        for (int i = 0; i < transparentChainChomp.Length; i++)
        {
            transparentChainChomp[i].transform.eulerAngles = finalPosition.transform.eulerAngles;          
        }
    }

    void SeeChainChomp()
    {
        if (chompMouvement.anim.GetInteger("animationState") == 2)
        {
            countUp[queu] = Mathf.PingPong(Time.time * 10, 1);
            SpawnGhost();
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                countUp[i] = 0;
                meshsForTransparentFade[i].GetComponent<Renderer>().material.Lerp(matTransparent0, matTransparent1, Mathf.Clamp01(countUp[queu]));
            }
            timer = 0;
        }
    }

    void SpawnGhost()
    {
        timer += Time.deltaTime;
        if(timer >= chompMouvement.timingAttack / (howManyGhost * 4.5))
        {
            transparentChainChomp[queu].transform.position = finalPosition.transform.position;
            meshsForTransparentFade[queu].GetComponent<Renderer>().material.Lerp(matTransparent0, matTransparent1, Mathf.Clamp01(countUp[queu]));
            if (queu < transparentChainChomp.Length -1)
            {
                queu++;
            }
            else
            {
                queu = 0;
            }
            timer = 0;
        }
    }
}
