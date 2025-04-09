using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;


public class CutsceneManager : MonoBehaviour
{
    public GameObject cutsceneBox;
    public Text cutsceneText;
    public AudioSource voiceSource;
    public AudioClip[] voiceLines;
    public float textSpeed = 0.05f;

    private bool isPlaying = false;
    private Action onCutsceneComplete;

    public void PlayCutscene(Cutscene cutscene, Action onComplete)
    {
        if (isPlaying) return;

        cutsceneBox.SetActive(true);
        StartCoroutine(PlayDialogue(cutscene, onComplete));
    }

    private IEnumerator PlayDialogue(Cutscene cutscene, Action onComplete)
    {
        isPlaying = true;
        onCutsceneComplete = onComplete;

        for (int i = 0; i < cutscene.dialogueLines.Length; i++)
        {
            string line = cutscene.dialogueLines[i];
            AudioClip voice = i < voiceLines.Length ? voiceLines[i] : null;

            if (voiceSource && voice != null)
                voiceSource.PlayOneShot(voice);

            cutsceneText.text = "";
            foreach (char letter in line.ToCharArray())
            {
                cutsceneText.text += letter;
                yield return new WaitForSecondsRealtime(textSpeed);
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        cutsceneBox.SetActive(false);
        isPlaying = false;
        onCutsceneComplete?.Invoke();
    }
}
