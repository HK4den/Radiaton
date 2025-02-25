using UnityEngine;
using UnityEngine.UI;

public class WaveUIManager : MonoBehaviour
{
    public Text waveNameText;  // UI Text for wave name
    public Text waveTimerText; // UI Text for countdown timer

    public void UpdateWaveName(int waveNumber)
    {
        waveNameText.text = $"Wave {waveNumber + 1}"; // Display "Wave 1", "Wave 2", etc.
    }

    public void UpdateWaveTimer(float timeLeft)
    {
        waveTimerText.text = timeLeft > 0 ? $"Time Left: {Mathf.Ceil(timeLeft)}s" : "Next Wave Incoming!";
    }
}
