using UnityEngine;

public class DashToMouse : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;

    private bool isDashing = false;
    private Vector3 dashDirection;
    private float dashTime;

    private DashCooldown dashCooldown; // Reference to DashCooldown

    void Start()
    {
        // Get the DashCooldown component on the same GameObject
        dashCooldown = GetComponent<DashCooldown>();
        if (dashCooldown == null)
        {
            Debug.LogError("DashCooldown script not found on this GameObject!");
        }
    }

    void Update()
    {
        // Check cooldown and initiate dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldown != null && dashCooldown.CanDash())
        {
            StartDash();
            dashCooldown.StartCooldown();
        }

        if (isDashing)
        {
            Dash();
        }
    }

    void StartDash()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        dashDirection = (mousePosition - transform.position).normalized;

        isDashing = true;
        dashTime = dashDuration;
    }

    void Dash()
    {
        transform.position += dashDirection * dashSpeed * Time.deltaTime;
        dashTime -= Time.deltaTime;

        if (dashTime <= 0)
        {
            isDashing = false;
        }
    }
}
