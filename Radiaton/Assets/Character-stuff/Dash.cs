using UnityEngine;

public class DashToMouse : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;

    private bool isDashing = false;
    private Vector3 dashDirection;
    private float dashTime;

    private DashCooldown dashCooldown; // Reference to DashCooldown

    // Public variables for setting layers
    public string originalLayerName;
    public string dashLayerName;

    private int originalLayer;  // Store original layer ID

    // Reference to the Chmovement script
    private PlayerMovement movementScript;  // Don't need to be public anymore

    void Start()
    {
        // Get the DashCooldown component on the same GameObject
        dashCooldown = GetComponent<DashCooldown>();
        if (dashCooldown == null)
        {
            Debug.LogError("DashCooldown script not found on this GameObject!");
        }

        // Store the original layer when the game starts
        originalLayer = gameObject.layer;

        // Make sure the layer names are set correctly in the Inspector
        if (string.IsNullOrEmpty(originalLayerName) || string.IsNullOrEmpty(dashLayerName))
        {
            Debug.LogError("Layer names must be set in the Inspector!");
        }

        // Get the Chmovement component attached to the same GameObject
        movementScript = GetComponent<PlayerMovement>();
        if (movementScript == null)
        {
            Debug.LogError("Chmovement script not found on this GameObject!");
        }
    }

    void Update()
    {
        // If dashing, don't process any input or movement
        if (isDashing)
        {
            // Still handle dash movement, so we don’t return here
            Dash();
            return;
        }

        // Check cooldown and initiate dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldown != null && dashCooldown.CanDash())
        {
            StartDash();
            dashCooldown.StartCooldown();
        }

        // Other normal player movement goes here, but won't execute while dashing.
    }

    void StartDash()
    {
        // Change to dash layer at the start of the dash
        gameObject.layer = LayerMask.NameToLayer(dashLayerName);

        // Calculate dash direction based on mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        dashDirection = (mousePosition - transform.position).normalized;

        // Disable the Chmovement script to stop movement during the dash
        if (movementScript != null)
        {
            movementScript.enabled = false;  // Disable movement input during dash
        }

        isDashing = true;
        dashTime = dashDuration;
    }

    void Dash()
    {
        // Move the character in the dash direction
        transform.position += dashDirection * dashSpeed * Time.deltaTime;
        dashTime -= Time.deltaTime;

        if (dashTime <= 0)
        {
            // Revert back to the original layer after the dash is done
            gameObject.layer = originalLayer;

            // Re-enable the Chmovement script after the dash
            if (movementScript != null)
            {
                movementScript.enabled = true;  // Re-enable movement input after dash
            }

            isDashing = false;
        }
    }
}
