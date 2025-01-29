using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private bool isInvincible = false;
    public float invincibilityDuration = 1.5f; // Time in seconds
    private SpriteRenderer spriteRenderer; // Reference to the player's sprite

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    // Collision with enemy and bullet
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))
        {
            PlayerTakeDmg(1);
            Debug.Log("Player hit by: " + collision.gameObject.tag);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") || collider.CompareTag("Bullet"))
        {
            PlayerTakeDmg(1);
            Debug.Log("Player hit by: " + collider.tag);
        }
    }

    void Update()
    {
        // Testing player health system
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerTakeDmg(1);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerHeal(1);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        if (isInvincible) return; // Ignore damage if invincible

        GameManager.gameManager._playerHealth.DmgUnit(dmg);

        // Update the health HUD
        FindObjectOfType<HealthHUD>().UpdateHUD(GameManager.gameManager._playerHealth.Health);

        // Flash the broken heart sprite temporarily
        StartCoroutine(FindObjectOfType<HealthHUD>().FlashBrokenHeart());

        StartCoroutine(InvincibilityFrames()); // Start invincibility
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float blinkInterval = 0.06f; // Time between blinks
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle sprite visibility
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true; // Ensure sprite is visible after invincibility
        isInvincible = false;
    }
    //for the HUD to update based on health
    



}
