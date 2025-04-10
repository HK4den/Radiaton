using UnityEngine;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject cutsceneBox;          // The overall container UI panel
    public TMP_Text dialogueText;    // The dialogue text element (TextMeshPro)
    public TMP_Text speakerNameText; // The speaker’s name text element (TextMeshPro)
    public Image portraitImage;             // The portrait image element

    [Header("Audio")]
    public AudioSource dialogueAudioSource; // Audio source for playing dialogue sound effects

    [Header("Background Music Settings")]
    public AudioSource backgroundMusicSource; // The background music AudioSource that will be lowered during cutscenes.
    public float backgroundMusicVolumeMultiplier = 0.5f; // Multiplier to reduce volume (e.g., 0.5 = 50% volume).
    private float originalBackgroundVolume;

    [Header("Cutscene Settings")]
    public float textSpeed = 0.05f;         // Speed at which text appears
    public bool pauseGameDuringCutscene = true;  // Should time be stopped during cutscenes?

    [Header("Player References")]
    public GameObject player;  // Assign your player GameObject here so controls can be toggled

    /// <summary>
    /// Begins the cutscene playback.
    /// </summary>
    /// <param name="cutscene">The Cutscene asset to play.</param>
    /// <param name="onComplete">Callback invoked when the cutscene is finished.</param>
    public void PlayCutscene(Cutscene cutscene, Action onComplete)
    {
        StartCoroutine(PlayDialogue(cutscene, onComplete));
    }

    private IEnumerator PlayDialogue(Cutscene cutscene, Action onComplete)
    {
        // Lower background music volume during cutscene if assigned.
        if (backgroundMusicSource != null)
        {
            originalBackgroundVolume = backgroundMusicSource.volume;
            backgroundMusicSource.volume = originalBackgroundVolume * backgroundMusicVolumeMultiplier;
        }

        // Pause game and disable player controls if configured.
        if (pauseGameDuringCutscene)
        {
            Time.timeScale = 0f;
        }
        DisablePlayerControls();

        cutsceneBox.SetActive(true);

        // Loop through each dialogue line.
        for (int i = 0; i < cutscene.dialogueLines.Length; i++)
        {
            // Clear previous text.
            dialogueText.text = "";
            string line = cutscene.dialogueLines[i];
            string speaker = i < cutscene.speakerNames.Length ? cutscene.speakerNames[i] : "";
            Sprite portrait = i < cutscene.portraits.Length ? cutscene.portraits[i] : null;
            AudioClip soundEffect = i < cutscene.dialogueSoundEffects.Length ? cutscene.dialogueSoundEffects[i] : null;

            // Set the UI elements.
            speakerNameText.text = speaker;
            if (portraitImage != null)
            {
                portraitImage.sprite = portrait;
            }

            // Play dialogue sound effect if available.
            if (dialogueAudioSource != null && soundEffect != null)
            {
                dialogueAudioSource.clip = soundEffect;
                dialogueAudioSource.Play();
            }

            // Reveal text one letter at a time.
            foreach (char letter in line.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSecondsRealtime(textSpeed);
            }

            // Wait until the player presses either Space (to proceed) or Tab (to skip dialogue).
            bool proceed = false;
            bool skipDialogue = false;
            while (!proceed)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    proceed = true;
                }
                else if (Input.GetKeyDown(KeyCode.Tab))
                {
                    proceed = true;
                    skipDialogue = true;
                }
                yield return null;
            }

            // Stop the dialogue sound (if it's still playing).
            if (dialogueAudioSource.isPlaying)
            {
                dialogueAudioSource.Stop();
            }

            // If Tab was pressed, skip the remaining dialogue.
            if (skipDialogue)
            {
                break;
            }
        }

        // End the cutscene.
        cutsceneBox.SetActive(false);

        // Re-enable player controls.
        EnablePlayerControls();
        if (pauseGameDuringCutscene)
        {
            Time.timeScale = 1f;
        }

        // Restore the background music volume.
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = originalBackgroundVolume;
        }
        onComplete?.Invoke();
    }

    /// <summary>
    /// Disables key player control scripts (movement, dash, and shooting).
    /// </summary>
    private void DisablePlayerControls()
    {
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = false;

            DashToMouse dtm = player.GetComponent<DashToMouse>();
            if (dtm != null) dtm.enabled = false;

            shooting shoot = player.GetComponent<shooting>();
            if (shoot != null) shoot.enabled = false;
        }
    }

    /// <summary>
    /// Re-enables the player control scripts.
    /// </summary>
    public void EnablePlayerControls()
    {
        if (player != null)
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null) pm.enabled = true;

            DashToMouse dtm = player.GetComponent<DashToMouse>();
            if (dtm != null) dtm.enabled = true;

            shooting shoot = player.GetComponent<shooting>();
            if (shoot != null) shoot.enabled = true;
        }
    }
}
