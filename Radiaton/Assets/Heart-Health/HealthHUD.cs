using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public Image heartImage;  // Heart UI icon
    public Image numberImage; // Number UI icon

    public Sprite normalHeart;
    public Sprite brokenHeart;
    public Sprite emptyHeart; // Stays broken at 0 HP

    public Sprite[] numberSprites; // Array for number images (0-10)

    private void Start()
    {
        UpdateHUD(GameManager.gameManager._playerHealth.Health);
    }

    public void UpdateHUD(int health)
    {
        Debug.Log("Updating HUD with health: " + health);

        // Update number sprite
        numberImage.sprite = numberSprites[Mathf.Clamp(health, 0, 10)];

        // Change heart sprite based on health
        if (health > 0)
        {
            heartImage.sprite = normalHeart;
        }
        else
        {
            heartImage.sprite = emptyHeart; // Set to empty heart at 0 HP
        }

        heartImage.SetAllDirty(); // Force UI to update
    }

    public IEnumerator FlashBrokenHeart()
    {
        heartImage.sprite = brokenHeart; // Show broken heart when damaged
        yield return new WaitForSeconds(0.3f);

        // If health is above 0, revert to normal heart
        if (GameManager.gameManager._playerHealth.Health > 0)
        {
            heartImage.sprite = normalHeart;
        }
        else
        {
            heartImage.sprite = emptyHeart; // Stay broken at 0 HP
        }
    }
}
