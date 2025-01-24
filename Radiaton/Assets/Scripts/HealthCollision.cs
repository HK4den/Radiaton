using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        // Get the health component attached to the player or enemy
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for damage when the player or enemy collides with something
        health.CheckDamageTags(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for damage when the player or enemy enters a trigger zone
        health.CheckDamageTags(other);
    }
}
