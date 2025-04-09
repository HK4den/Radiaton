using UnityEngine;

[CreateAssetMenu(fileName = "NewCutscene", menuName = "Cutscene")]
public class Cutscene : ScriptableObject
{
    [TextArea]
    public string[] dialogueLines;
}
