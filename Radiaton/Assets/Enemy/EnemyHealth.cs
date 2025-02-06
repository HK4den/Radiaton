using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public AudioClip deathSound; // Death sound effect
    public GameObject deathSoundPrefab; // Prefab that plays the sound

    void Start()
    {
        currentHealth = maxHealth;
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

        // Spawn invisible object that plays the sound
        if (deathSoundPrefab != null)
        {
            Instantiate(deathSoundPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Destroy the enemy
    }
}
