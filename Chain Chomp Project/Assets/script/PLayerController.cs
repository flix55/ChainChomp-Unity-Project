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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        playerMovement.Set(h, 0f, v);
        playerMovement = playerMovement * moveSpeed * Time.deltaTime;
        playerRB.MovePosition(transform.position + playerMovement);
    }
}
