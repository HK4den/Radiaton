using UnityEngine;

public class DashCooldown : MonoBehaviour
{
    public float cooldownTime = 2f; // Time in seconds between dashes
    private float nextDashTime = 0f; // The next time the player can dash

    // Checks if the dash is ready
    public bool CanDash()
    {
        return Time.time >= nextDashTime;
    }

    // Starts the cooldown
    public void StartCooldown()
    {
        nextDashTime = Time.time + cooldownTime;
    }
}
