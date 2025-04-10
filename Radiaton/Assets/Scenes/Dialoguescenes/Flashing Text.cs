using System.Collections;
using UnityEngine;
using TMPro;

public class FlashingTMPText : MonoBehaviour
{
    // Interval for flashing on/off in seconds (real-time).
    public float flashInterval = 0.5f;

    // Reference to the TMP_Text component.
    private TMP_Text tmpText;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
        if (tmpText == null)
        {
            Debug.LogError("No TMP_Text component found on " + gameObject.name);
        }
    }

    void OnEnable()
    {
        StartCoroutine(Flash());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Flash()
    {
        // Loop forever while the GameObject is enabled.
        while (true)
        {
            tmpText.enabled = !tmpText.enabled;
            // Wait in real time so it works even when Time.timeScale is 0
            yield return new WaitForSecondsRealtime(flashInterval);
        }
    }
}
