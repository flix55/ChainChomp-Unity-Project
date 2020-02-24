using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDelay : MonoBehaviour
{
    ChompMouvement chompMouvement;
    public float followSpeed = 5;
    public GameObject[] transparentChainChomp;
    public GameObject[] meshsForTransparentFade;
    public GameObject finalPosition;
    public Material matTransparent0;
    public Material matTransparent1;
    float countUp;
    float countUp2;

    void Start()
    {
        chompMouvement = FindObjectOfType<ChompMouvement>();
    }

    void Update()
    {
        FollowAll();
        SeeChainChomp();
    }

    void FollowAll()
    {
        for (int i = 0; i < transparentChainChomp.Length; i++)
        {
            transparentChainChomp[i].transform.position = Vector3.Lerp(transparentChainChomp[i].transform.position, finalPosition.transform.position, Time.deltaTime * (followSpeed - (i + 2)));
            transparentChainChomp[i].transform.eulerAngles = finalPosition.transform.eulerAngles;
        }
    }

    void SeeChainChomp()
    {
        if (chompMouvement.anim.GetInteger("animationState") == 2)
        {
            countUp = Mathf.Lerp(countUp, 1, Time.deltaTime * 5);
        }
        else
        {
            countUp = Mathf.Lerp(countUp, 0, Time.deltaTime * 20);
        }

        foreach (GameObject transChain in meshsForTransparentFade)
        {
            transChain.GetComponent<Renderer>().material.Lerp(matTransparent0, matTransparent1, Mathf.Clamp01(countUp));
            //Debug.Log(Mathf.Clamp01(countUp));
        }

    }
}
