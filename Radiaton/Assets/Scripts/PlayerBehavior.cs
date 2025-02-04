using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private bool isInvincible = false;
    private bool isDead = false;
    public float invincibilityDuration = 1.5f;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
    public AudioClip hurtSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    void Update()
    {
        // Test health system
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
        if (isInvincible || isDead) return; // Prevents damage when dead or invincible

        GameManager.gameManager._playerHealth.DmgUnit(dmg);

        // Play hurt sound
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // Update HUD
        FindObjectOfType<HealthHUD>().UpdateHUD(GameManager.gameManager._playerHealth.Health);
        StartCoroutine(FindObjectOfType<HealthHUD>().FlashBrokenHeart());

        // Check if player is dead
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            HandleDeath();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private void HandleDeath()
    {
        if (isDead) return; // Prevents re-triggering death

        isDead = true;
        GetComponent<PlayerMovement>().enabled = false; // Disable movement
        GetComponent<DashToMouse>().enabled = false; //Disables dashing
        GetComponent<shooting>().enabled = false; //Disables shooting
        GetComponent<DashCooldown>().enabled = false; //Disables the dash cooldown
        Debug.Log("Player is dead!");
    }

    private void PlayerHeal(int healing)
    {
        if (isDead) return; // Can't heal if dead
        GameManager.gameManager._playerHealth.HealUnit(healing);
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float blinkInterval = 0.06f;
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    // Collision detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet")))
        {
            PlayerTakeDmg(1);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isDead && (collider.CompareTag("Enemy") || collider.CompareTag("Bullet")))
        {
            PlayerTakeDmg(1);
        }
    }
}
