using System.Collections;
using System.Collections.Generic;
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
    private Coroutine flashGreenCoroutine;
    private void Start()
    {
        GameOverScreen.SetActive(false);
        UpdateHUD(GameManager.gameManager._playerHealth.Health);
    }
    public void UpdateHUD(int health)
    {
        numberImage.sprite = numberSprites[Mathf.Clamp(health, 0, 10)];
        if (health > 0)
            heartImage.sprite = normalHeart;
        else
            heartImage.sprite = emptyHeart;
        if (health <= 0)
            GameOverScreen.SetActive(true);
        heartImage.SetAllDirty();
    }
    public IEnumerator FlashBrokenHeart()
    {
        heartImage.sprite = brokenHeart;
        yield return new WaitForSeconds(0.3f);
        if (GameManager.gameManager._playerHealth.Health > 0)
            heartImage.sprite = normalHeart;
        else
            heartImage.sprite = emptyHeart;
    }
    public IEnumerator FlashGreenHeart()
    {
        if (flashGreenCoroutine != null)
        {
            StopCoroutine(flashGreenCoroutine);
            flashGreenCoroutine = null;
            heartImage.color = Color.white;
        }
        flashGreenCoroutine = StartCoroutine(FlashGreenHeartRoutine());
        yield return flashGreenCoroutine;
        flashGreenCoroutine = null;
    }
    private IEnumerator FlashGreenHeartRoutine()
    {
        heartImage.sprite = greenHeart;
        Color originalColor = heartImage.color;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            heartImage.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, t);
            timer += Time.deltaTime;
            yield return null;
        }
        heartImage.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        timer = 0f;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            heartImage.color = Color.Lerp(Color.white, Color.white, t);
            timer += Time.deltaTime;
            yield return null;
        }
        heartImage.sprite = normalHeart;
        heartImage.color = Color.white;
    }
}
