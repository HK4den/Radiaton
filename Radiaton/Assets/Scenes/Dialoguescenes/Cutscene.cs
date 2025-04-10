using UnityEngine;

[CreateAssetMenu(fileName = "NewCutscene", menuName = "Cutscene")]
public class Cutscene : ScriptableObject
{
    [TextArea]
    public string[] dialogueLines;
    public string[] speakerNames;
    public Sprite[] portraits;
    // New: Each dialogue line can have its own sound effect.
    public AudioClip[] dialogueSoundEffects;
}
