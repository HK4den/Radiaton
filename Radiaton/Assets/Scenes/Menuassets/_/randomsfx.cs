using UnityEngine;
using UnityEngine.UI;

public class RandomSoundEffectButton : MonoBehaviour
{
    // Reference to the AudioSource that will play the sound
    public AudioSource audioSource;

    // Array to store the sound effects (make sure to drag and drop your audio clips in the inspector)
    public AudioClip[] soundEffects;

    // Reference to the buttons
    public Button[] buttons;

    void Start()
    {
        // Ensure each button calls the PlayRandomSound function when clicked
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayRandomSound);
        }
    }

    // Function to play a random sound effect
    void PlayRandomSound()
    {
        // Check if we have sound effects in the array
        if (soundEffects.Length > 0)
        {
            // Select a random index from the soundEffects array
            int randomIndex = Random.Range(0, soundEffects.Length);

            // Play the selected sound
            audioSource.clip = soundEffects[randomIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No sound effects assigned!");
        }
    }
}
