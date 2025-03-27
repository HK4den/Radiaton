using System.Collections;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private bool isInvincible = false;
    private bool isDead = false;
    public float invincibilityDuration = 1.5f;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip healSound; // Healing sound

    private HealthHUD healthHUD;
    private Color originalColor;
    public float healFadeDuration = 0.15f; // How fast it fades in/out

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        healthHUD = FindObjectOfType<HealthHUD>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerTakeDmg(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerHeal(1);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        if (isInvincible || isDead) return;

        GameManager.gameManager._playerHealth.DmgUnit(dmg);

        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        healthHUD.UpdateHUD(GameManager.gameManager._playerHealth.Health);
        StartCoroutine(healthHUD.FlashBrokenHeart());

        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            HandleDeath();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private void PlayerHeal(int healing)
    {
        if (isDead) return;

        GameManager.gameManager._playerHealth.HealUnit(healing);

        if (audioSource != null && healSound != null)
        {
            audioSource.PlayOneShot(healSound);
        }

        healthHUD.UpdateHUD(GameManager.gameManager._playerHealth.Health);
        StartCoroutine(healthHUD.FlashGreenHeart());
        StartCoroutine(FlashGreenColor());
    }

    private IEnumerator FlashGreenColor()
    {
        Color healColor = new Color(0.454f, 1f, 0.318f); // Hex #74FF51
        float timer = 0f;

        // Fade In
        while (timer < healFadeDuration)
        {
            float t = timer / healFadeDuration;
            spriteRenderer.color = Color.Lerp(originalColor, healColor, t);
            timer += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = healColor;

        yield return new WaitForSeconds(0.3f); // Match heart flash duration

        // Fade Out
        timer = 0f;
        while (timer < healFadeDuration)
        {
            float t = timer / healFadeDuration;
            spriteRenderer.color = Color.Lerp(healColor, originalColor, t);
            timer += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = originalColor;
    }

    private void HandleDeath()
    {
        if (isDead) return;

        isDead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<DashToMouse>().enabled = false;
        GetComponent<shooting>().enabled = false;
        GetComponent<DashCooldown>().enabled = false;
        Debug.Log("Player is dead!");
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
