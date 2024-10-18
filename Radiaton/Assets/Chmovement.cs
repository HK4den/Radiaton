using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chmovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody 2D rb;
    Vector2 movement;

    //update is called once per frame
    private void Update()
    {
        //Input
        movement.x =
    Input.GetAxisRaw("Horizontal");
        movement.y =
    Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}