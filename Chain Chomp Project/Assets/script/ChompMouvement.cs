using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChompMouvement : MonoBehaviour
{

    public AnimationCurve curveBasicBegening = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public AnimationCurve curveBasicMouvement = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public AnimationCurve curveAttack = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
    public Animator anim;
    [Space(20)]
    public GameObject chainChomp;
    public GameObject mesh;
    public GameObject Player;
    public float turnSpeed = 5f;
    public float turnSpeedIdle = 15f;
    public float turnAmount = 20f;
    public float howFar = 15;
    public float howlow = -5;
    [Space(20)]
    public float speedRedChargeAttack = 2f;
    public float speedRedAttatck = 5f;
    [Space(20)]
    public float timingCharge = 2.5f;
    public float timingAttack = 1.5f;
    public float timingReturnToTheMiddle = 1;
    public float timingturning = 1;
    public float idleTimingRotatingWaiting = 4;
    public GameObject sparks;
    public GameObject charging;


    bool lerping;
    bool turningTowardPlauer;
    bool turningTowardCenter;
    bool doingAnimation;
    bool returning;
    Vector3 direction;
    Vector3 chompStartPosition;
    Quaternion newRotation;

    Color colorStart;
    bool colorRed;
    float timer;

    bool timerBool;
    bool doingIdleRotation;
    int randomDirection;
    float timerRotation;
    Quaternion idleNewRotation;

    private void Start()
    {

        colorStart = mesh.gameObject.GetComponent<Renderer>().material.color;
        chompStartPosition = chainChomp.transform.position;
    }


    private void Update()
    {

        if(anim.GetInteger("animationState") == 1)
        {
            charging.SetActive(true);
        }
        else
        {
            charging.SetActive(false);
        }


        rotateTowardCenter();
        rotateTowardPlayer();
        ChangeColor();
        IdleRotationTimer(idleTimingRotatingWaiting);

    }

    void ChangeColor()
    {
        timer = Mathf.Clamp01(timer);
        if (colorRed)
        {
            timer += Time.deltaTime * speedRedChargeAttack;
            mesh.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, Color.red, timer);
        }
        else
        {
            timer -= Time.deltaTime * speedRedAttatck;
            mesh.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, Color.red, timer);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (doingAnimation == false)
        {
            if (other.gameObject == Player)
            {
                StartCoroutine(ChampSeePlayer());
            }
        }
    }

    IEnumerator ChampSeePlayer()
    {
        anim.SetInteger("animationState", 1);
        doingAnimation = true;
        turningTowardPlauer = true;
        yield return new WaitForSeconds(timingturning);
        turningTowardPlauer = false;
        ChargeAttack();
    }

    void ChargeAttack()
    {
        colorRed = true;
        Vector3 chargePosition = (chainChomp.transform.transform.right * howlow) + chainChomp.transform.position;
        StartCoroutine( SmoothLerp(chainChomp.transform, chainChomp.transform.localPosition, chargePosition, timingCharge));
    }

    void DirectAttack()
    {
        anim.SetInteger("animationState", 2);
        colorRed = false;
        Vector3 chargePosition = (chainChomp.transform.transform.right * howFar) + chainChomp.transform.position;
        StartCoroutine(SmoothLerp2(chainChomp.transform, chainChomp.transform.localPosition, chargePosition, timingAttack));
    }

    void ReturnToTheMiddle()
    {
        anim.SetInteger("animationState", 4);
        StartCoroutine(SmoothLerp3(chainChomp.transform, chainChomp.transform.position, chompStartPosition, timingReturnToTheMiddle));
    }

    void rotateTowardPlayer()
    {
        if (turningTowardPlauer == true)
        {
            SmothRotate(Player.transform.position, turnSpeed);
        }
    }

    void rotateTowardCenter()
    {
        if (turningTowardCenter == true)
        {
            SmothRotate(chompStartPosition, turnSpeed);
        }
    }

    void SmothRotate(Vector3 pos, float speed)
    {
        direction = pos - chainChomp.transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
           newRotation = Quaternion.LookRotation(direction);
           newRotation.eulerAngles += new Vector3(0, -90, 0);
           chainChomp.transform.rotation = Quaternion.Slerp(chainChomp.transform.rotation, newRotation, speed * Time.deltaTime);
        }
    }

    void IdleRotationTimer(float waiting)
    {
        if (!doingAnimation)
        {
            timerRotation += Time.deltaTime;

            if (timerRotation > waiting && timerBool == false)
            {
                StartCoroutine(RotationIdleAction());
                timerBool = true;
            }
        }
        if (doingAnimation)
        {
            timerRotation = 0;
        }

        if (doingIdleRotation == true)
        {
            RandomDirectionIdle();
        }

    }

    public IEnumerator RotationIdleAction()
    {
        randomDirection = Random.Range(0, 2);
        doingIdleRotation = true;
        anim.SetBool("jump", true);
        yield return new WaitForSeconds(0.15f);
        anim.SetBool("jump", false);
        doingIdleRotation = false;
        timerRotation = 0;
        timerBool = false;
    }

    void RandomDirectionIdle()
    {
        if(randomDirection == 0)
        {
            idleRotationValue(turnAmount);
        }
        if (randomDirection == 1)
        {
            idleRotationValue(-turnAmount);
        }
    }
    void idleRotationValue(float amount)
    {
        idleNewRotation.eulerAngles = new Vector3(chainChomp.transform.eulerAngles.x, chainChomp.transform.eulerAngles.y + amount, chainChomp.transform.eulerAngles.z);
        chainChomp.transform.rotation = Quaternion.Slerp(chainChomp.transform.rotation, idleNewRotation, turnSpeedIdle * Time.deltaTime);
    }

    //optimise that
    public IEnumerator SmoothLerp(Transform trans, Vector3 startPos, Vector3 endPos, float seconds)
    {
        lerping = true;
        float t = 0;

        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            trans.localPosition = Vector3.Lerp(startPos, endPos, curveBasicBegening.Evaluate(t));
            yield return null;
        }

        lerping = false;
        DirectAttack();
    }

    public IEnumerator SmoothLerp2(Transform trans, Vector3 startPos, Vector3 endPos, float seconds)
    {
        lerping = true;
        float t = 0;

        while (t <= 1.0)
        {
            if (t >= 0.25f)
            {
                anim.SetInteger("animationState", 3);
            }
            t += Time.deltaTime / seconds;
            trans.localPosition = Vector3.Lerp(startPos, endPos, curveAttack.Evaluate(t));
            yield return null;

        }
        lerping = false;
        turningTowardCenter = true;
        ReturnToTheMiddle();
    }

    public IEnumerator SmoothLerp3(Transform trans, Vector3 startPos, Vector3 endPos, float seconds)
    {
        lerping = true;
        float t = 0;

        while (t <= 1.0)
        {
            if(t == 0f)
            {
                anim.SetBool("jump", true);
            }

            if (t >= 0.5f)
            {
                anim.SetBool("jump", false);
            }

            t += Time.deltaTime / seconds;
            trans.position = Vector3.Lerp(startPos, endPos, curveBasicMouvement.Evaluate(t));
            yield return null;
        }
        turningTowardCenter = false;
        lerping = false;
        doingAnimation = false;
        returning = false;
        doingAnimation = false;
    }

}
