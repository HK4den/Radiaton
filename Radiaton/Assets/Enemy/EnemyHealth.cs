using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public AudioClip deathSound; // Death sound effect
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;

        // Ensure the enemy has its own AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add if missing
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playerbullet") || collision.gameObject.CompareTag("Placeholder"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // Destroy the bullet upon hitting the enemy
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took damage! Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");

        // Play death sound if available
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Destroy(gameObject); // Destroy immediately after playing the sound
    }
}
