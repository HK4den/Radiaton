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
        // Update number sprite
        numberImage.sprite = numberSprites[Mathf.Clamp(health, 0, 10)];

        // Change heart sprite based on health
        if (health > 0)
        {
            heartImage.sprite = normalHeart;
        }
        else
        {
            heartImage.sprite = emptyHeart; // Stays broken at 0 HP
        }
    }

    public IEnumerator FlashBrokenHeart()
    {
        heartImage.sprite = brokenHeart; // Show broken heart when damaged
        yield return new WaitForSeconds(0.3f);
        heartImage.sprite = normalHeart; // Revert back to normal heart
    }
}
