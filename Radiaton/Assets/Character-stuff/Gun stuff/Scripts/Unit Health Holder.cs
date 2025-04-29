using System.Collections;
using UnityEngine;

public class UnitHealthHolder : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0f)
            StartCoroutine(FlashWhite());
        else
            Die();
    }

    private IEnumerator FlashWhite()
    {
        var sr = GetComponent<SpriteRenderer>();
        var orig = sr.color;
        sr.color = Color.white;
        yield return null;
        sr.color = orig;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
