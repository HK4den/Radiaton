using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public Image heartImage;
    public Image numberImage;
    public GameObject GameOverScreen;
    public Sprite normalHeart;
    public Sprite brokenHeart;
    public Sprite emptyHeart;
    public Sprite greenHeart;
    public Sprite[] numberSprites;
    public float fadeDuration = 0.15f;

    private Coroutine flashGreenRoutine;

    void Start()
    {
        GameOverScreen.SetActive(false);
        UpdateHUD(GameManager.gameManager._playerHealth.Health);
    }

    public void UpdateHUD(int health)
    {
        numberImage.sprite = numberSprites[Mathf.Clamp(health, 0, numberSprites.Length - 1)];
        heartImage.sprite = health > 0 ? normalHeart : emptyHeart;
        if (health <= 0) GameOverScreen.SetActive(true);
        heartImage.SetAllDirty();
    }

    public IEnumerator FlashBrokenHeart()
    {
        heartImage.sprite = brokenHeart;
        yield return new WaitForSeconds(0.3f);
        heartImage.sprite = GameManager.gameManager._playerHealth.Health > 0
            ? normalHeart : emptyHeart;
    }

    public IEnumerator FlashGreenHeart()
    {
        if (flashGreenRoutine != null) StopCoroutine(flashGreenRoutine);
        flashGreenRoutine = StartCoroutine(FlashGreenRoutine());
        yield return flashGreenRoutine;
        flashGreenRoutine = null;
    }

    private IEnumerator FlashGreenRoutine()
    {
        heartImage.sprite = greenHeart;
        Color orig = heartImage.color;
        float t = 0f;
        while (t < fadeDuration)
        {
            heartImage.color = Color.Lerp(Color.clear, Color.white, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        heartImage.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while (t < fadeDuration)
        {
            heartImage.color = Color.Lerp(Color.white, Color.clear, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        heartImage.sprite = normalHeart;
        heartImage.color = Color.white;
    }

    public IEnumerator ResetHeartColor()
    {
        yield return null; // wait one frame
        heartImage.color = Color.white;
    }
}
