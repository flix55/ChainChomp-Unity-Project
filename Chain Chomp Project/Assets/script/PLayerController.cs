using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{
    Vector3 playerMovement;
    Rigidbody playerRB;
    public float moveSpeed = 10;
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        /*float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        playerMovement.Set(h, 0f, v);*/
        playerMovement = playerMovement * moveSpeed * Time.deltaTime;
        playerRB.MovePosition(transform.position + playerMovement);
        if(Input.GetButton("joystick button 12"))
        {
            Debug.Log("up");
        }
        if (Input.GetButtonDown("joystick button 13"))
        {
            Debug.Log("down");
        }
        if (Input.GetButtonDown("joystick button 14"))
        {
            Debug.Log("right");
        }
        if (Input.GetButtonDown("joystick button 15"))
        {
            Debug.Log("left");
        }
        if (Input.GetButton("joystick button 1"))
        {
            Debug.Log("1");
        }
        if (Input.GetButtonDown("joystick button 2"))
        {
            Debug.Log("2");
        }
        if (Input.GetButton("joystick button 3"))
        {
            Debug.Log("3");
        }
        if (Input.GetButtonDown("joystick button 4"))
        {
            Debug.Log("4");
        }
        if (Input.GetButtonDown("joystick button 5"))
        {
            Debug.Log("5");
        }
        if (Input.GetButtonDown("joystick button 6"))
        {
            Debug.Log("6");
        }
        if (Input.GetButtonDown("joystick button 7"))
        {
            Debug.Log("7");
        }



        if (Input.GetButton("joystick button 8"))
        {
            Debug.Log("up");
            playerMovement.Set(0, 0f, 1);
            playerMovement = playerMovement * moveSpeed * Time.deltaTime;
        }
        if (Input.GetButton("joystick button 9"))
        {
            Debug.Log("down");
            playerMovement.Set(0, 0f, -1);
            playerMovement = playerMovement * moveSpeed * Time.deltaTime;
        }
        if (Input.GetButton("joystick button 10"))
        {
            Debug.Log("right");
            playerMovement.Set(-1, 0f, 0);
            playerMovement = playerMovement * moveSpeed * Time.deltaTime;
        }
        if (Input.GetButton("joystick button 11"))
        {
            Debug.Log("left");
            playerMovement.Set(1, 0f, 0);
            playerMovement = playerMovement * moveSpeed * Time.deltaTime;
        }
    }
}
