using UnityEngine;
using UnityEngine.UI;

public class SwapImageButton : MonoBehaviour
{
    public Image buttonImage; // Assign in Inspector
    public Sprite image1;
    public Sprite image2;
    public AudioSource audioSource; // Assign in Inspector
    public AudioClip[] soundEffects; // Assign at least 3 sounds in Inspector

    private bool isImage1 = true;

    void Start()
    {
        // Ensure buttonImage and audioSource are assigned
        if (buttonImage == null) buttonImage = GetComponent<Image>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        // Check if assignments are successful
        if (buttonImage == null)
        {
            Debug.LogError("SwapImageButton: No Image component found! Assign one in the Inspector.");
            return;
        }
        if (audioSource == null)
        {
            Debug.LogError("SwapImageButton: No AudioSource found! Attach one to this GameObject.");
            return;
        }
        if (soundEffects.Length == 0)
        {
            Debug.LogError("SwapImageButton: No sound effects assigned! Add at least 3 AudioClips.");
            return;
        }

        buttonImage.sprite = image1; // Set initial sprite
    }

    public void SwapImage()
    {
        // Swap the image
        isImage1 = !isImage1;
        buttonImage.sprite = isImage1 ? image1 : image2;

        // Play a random sound effect
        if (soundEffects.Length > 0)
        {
            int randomIndex = Random.Range(0, soundEffects.Length);
            audioSource.PlayOneShot(soundEffects[randomIndex]);
        }
    }
}
