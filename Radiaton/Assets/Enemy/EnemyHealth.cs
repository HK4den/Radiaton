using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public AudioClip deathSound; // Optional death sound
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = Camera.main.GetComponent<AudioSource>(); // Use camera's AudioSource
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Playerbullet") || collision.gameObject.CompareTag("Player"))
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

        Destroy(gameObject); // Remove the enemy from the scene
    }
}
