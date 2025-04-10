using UnityEngine;
using System.Collections;

public class FadeAndDestroy : MonoBehaviour
{
    // Time (in seconds) for which the object will remain fully visible.
    public float displayTime = 3f;

    // Time (in seconds) over which the object will fade to transparent.
    public float fadeTime = 2f;

    // Reference to the SpriteRenderer component.
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("FadeAndDestroy requires a SpriteRenderer component on the same GameObject.");
        }
    }

    void Start()
    {
        // Start the fade coroutine.
        StartCoroutine(FadeOutAndDestroy());
    }

    IEnumerator FadeOutAndDestroy()
    {
        // Wait for the display time before starting the fade.
        yield return new WaitForSeconds(displayTime);

        // Get the current color of the sprite.
        Color originalColor = spriteRenderer.color;
        float elapsed = 0f;

        // Gradually fade the alpha value to 0 over fadeTime seconds.
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(originalColor.a, 0f, elapsed / fadeTime);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }

        // Ensure alpha is fully 0, then destroy the GameObject.
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }
}
