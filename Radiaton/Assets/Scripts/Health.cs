using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 5f;
    public string[] damageTags;  // Array to store tags that cause damage

    // Call this method when an object with a tag collides
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death (e.g., play animation, disable object, etc.)
        Destroy(gameObject); // Example: Destroy the object
    }

    // Call this method to check if the object should take damage
    public void CheckDamageTags(Collider other)
    {
        // Loop through all the tags that cause damage
        foreach (var tag in damageTags)
        {
            if (other.CompareTag(tag))
            {
                TakeDamage(1f); // Apply 10 damage by default
                return; // Exit once we find the first matching tag (optional)
            }
        }
    }
}
