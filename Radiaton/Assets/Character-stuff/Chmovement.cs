using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float maxSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input direction
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;
        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        // Calculate target velocity based on movement input
        Vector2 targetVelocity = movementInput * moveSpeed;

        // Determine whether to accelerate or decelerate
        if (movementInput.magnitude > 0)
        {
            // Accelerate towards target velocity
            rb.velocity = Vector2.MoveTowards(rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate to a stop if no input
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        // Clamp velocity to max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate is zero since we're in 2D

        // Calculate the direction from the player to the mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the player towards the mouse
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
