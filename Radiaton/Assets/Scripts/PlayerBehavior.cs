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
    public AudioClip healSound;

    private HealthHUD healthHUD;
    private Color originalColor;
    public float healFadeDuration = 0.15f;

    public float hitShakeIntensity = 0.2f;
    public float hitShakeDuration = 0.15f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        healthHUD = FindObjectOfType<HealthHUD>();
    }

    void Update()
    {
        // Testing input
        if (Input.GetKeyDown(KeyCode.P))
            PlayerTakeDmg(1);
        if (Input.GetKeyDown(KeyCode.Q))
            PlayerHeal(1);
    }

    private void PlayerTakeDmg(int dmg)
    {
        if (isInvincible || isDead) return;

        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        if (hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

        healthHUD.UpdateHUD(GameManager.gameManager._playerHealth.Health);
        StartCoroutine(healthHUD.FlashBrokenHeart());

        // Screen shake on hit
        if (Camera.main.TryGetComponent<CameraFollow>(out var cam))
            cam.Shake(hitShakeIntensity, hitShakeDuration);

        if (GameManager.gameManager._playerHealth.Health <= 0)
            HandleDeath();
        else
            StartCoroutine(InvincibilityFrames());
    }

    private void PlayerHeal(int healing)
    {
        if (isDead) return;

        GameManager.gameManager._playerHealth.HealUnit(healing);
        if (healSound != null)
            audioSource.PlayOneShot(healSound);

        healthHUD.UpdateHUD(GameManager.gameManager._playerHealth.Health);
        StartCoroutine(healthHUD.FlashGreenHeart());
        StartCoroutine(FlashGreenColor());
    }

    private IEnumerator FlashGreenColor()
    {
        Color healColor = new Color(0.454f, 1f, 0.318f);
        float timer = 0f;

        // Fade in to heal color
        while (timer < healFadeDuration)
        {
            float t = timer / healFadeDuration;
            spriteRenderer.color = Color.Lerp(originalColor, healColor, t);
            timer += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = healColor;

        yield return new WaitForSeconds(0.3f);

        // Fade out back to original
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

        var pm = GetComponent<PlayerMovement>();
        if (pm != null) pm.enabled = false;

        var dtm = GetComponent<DashToMouse>();
        if (dtm != null) dtm.enabled = false;

        var shoot = GetComponent<shooting>();
        if (shoot != null) shoot.enabled = false;

        var cd = GetComponent<DashCooldown>();
        if (cd != null) cd.enabled = false;

        Debug.Log("Player is dead!");
    }

    private IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        float blinkInterval = 0.06f;
        float elapsed = 0f;

        while (elapsed < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.CompareTag("Bullet"))
        {
            PlayerTakeDmg(1);
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            PlayerHeal(1);
        }
        else if (collision.gameObject.CompareTag("heal2"))
        {
            PlayerHeal(3);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead) return;

        if (collider.CompareTag("Enemy") ||
            collider.CompareTag("Bullet"))
        {
            PlayerTakeDmg(1);
        }
    }
}
