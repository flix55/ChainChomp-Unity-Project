using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public Animator anim;
    public GameObject[] chainObjects;
    [Space(10)]
    public float springEffect2 = 0.1f;
    public float amountspringTension = 5;
    public float rotationSpeed = 10;
    public float floorCollisionYPosition = 0.5f;
    public float snapTreshold = 6.8f;

    Vector3 localDistance;
    //clean code the book would be pround of me
    Quaternion[] newRotation = new[] { new Quaternion(0f, 0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f) , new Quaternion(0f, 0f, 0f, 0f) , new Quaternion(0f, 0f, 0f, 0f) , new Quaternion(0f, 0f, 0f, 0f) , new Quaternion(0f, 0f, 0f, 0f) , new Quaternion(0f, 0f, 0f, 0f) };
    Vector3[] direction = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f) };

    Vector3[] poss = new [] { new Vector3(0f,0f,0f), new Vector3(1f,1f,1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f) };
    Vector3[] dir = new[] { new Vector3(0f, 0f, 0f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f), new Vector3(1f, 1f, 1f) };

    float springEffect2InitialValue;
    float amountspringTensionInitialValue;
    float rotationSpeedInitialValue;

    private void Start()
    {
        springEffect2InitialValue = springEffect2;
        amountspringTensionInitialValue = amountspringTension;
        rotationSpeedInitialValue = rotationSpeed;
    }

    void Update()
    {
        ChainGoCrazy();
        InBetwenCalculation();
    }

    void InBetwenCalculation()
    {
        float dist = Vector3.Distance(chainObjects[0].transform.position, chainObjects[chainObjects.Length - 1].transform.position);
        int i;

        float cordDistance = Vector3.Distance(chainObjects[0].transform.position, chainObjects[chainObjects.Length - 1].transform.position);

        for (i = 0; i < chainObjects.Length; i++)
        {
            // position
            if(i != 0 && i != chainObjects.Length - 1)
            {
                localDistance = (chainObjects[i - 1].transform.position + chainObjects[i + 1].transform.position) / 2;

                Vector3 localDistanceCopie = localDistance;
                if(cordDistance < snapTreshold && i != 1 && i != 0)
                {
                    float distanceFromTheGround = (i * -0.2f) + 1.5f;
                    float YCurve = localDistance.y;
                    YCurve = distanceFromTheGround + (localDistance.y * 0.2f);

                    /*float pointBetwenCordDistance(){
                        return null;
                    }*/
                }

                
                poss[i] = Vector3.Lerp(poss[i], (localDistance - dir[i]) * (i / amountspringTension), springEffect2);
                dir[i] += poss[i];
                chainObjects[i].transform.position = new Vector3(dir[i].x, dir[i].y = Mathf.Clamp(dir[i].y,floorCollisionYPosition,10000f), dir[i].z);
            }

            //rotation
            if(i != 0 && i != chainObjects.Length - 1)
            {

                direction[i] = chainObjects[i + 1].transform.position - chainObjects[i].transform.position;
                if (direction[i] != Vector3.zero)
                {

                    newRotation[i] = Quaternion.LookRotation(direction[i]);
                    newRotation[i].eulerAngles += new Vector3(90, 0, 90);
                    /*Vector3 onlyXRot = new Vector3(chainObjects[i].transform.eulerAngles.x, 0, chainObjects[i].transform.eulerAngles.z);
                    chainObjects[i].transform.rotation = Quaternion.Euler(onlyXRot);*/
                    chainObjects[i].transform.rotation = Quaternion.Slerp(chainObjects[i].transform.rotation, newRotation[i], rotationSpeed * Time.deltaTime);
                }
            }
        }

    }

    void ChainGoCrazy()
    {
        if (anim.GetInteger("animationState") == 2 || anim.GetInteger("animationState") == 3)
        {
            springEffect2 = 0.03f;
            rotationSpeed = 5;
            amountspringTension = 3;
        }
        else
        {
            springEffect2 = springEffect2InitialValue;
            amountspringTension = amountspringTensionInitialValue;
            rotationSpeed = rotationSpeedInitialValue;
        }
    }

    // add delay... somehow
    //rotation on the x axis as well // and spring effects on rotation // 180 degres x rotation when crossing the middle 

    //chain physcis with the more tension, the less weight thay have starting from the attachChain (aka fucking physic)
    // ---- alright so two main position, one for the in betwen(aka localdistance), one for the ground(make one), find the middle ground of those two variable if the magnitude is lacking

}
