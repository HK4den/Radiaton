using UnityEngine;

[CreateAssetMenu(fileName = "NewCutscene", menuName = "Cutscene")]
public class Cutscene : ScriptableObject
{
    [TextArea]
    public string[] dialogueLines;
    public string[] speakerNames;
    public Sprite[] portraits;
    public AudioClip[] voiceLines;
}