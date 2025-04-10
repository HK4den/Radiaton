using UnityEngine;
using System;

public class CutsceneTrigger : MonoBehaviour
{
    public Cutscene cutscene; // Assign a Cutscene asset in the Inspector

    /// <summary>
    /// Called by another script (for example, Wave_Spawner) to start the cutscene.
    /// </summary>
    public void TriggerCutscene(CutsceneManager manager, Action onComplete)
    {
        if (cutscene == null)
        {
            Debug.LogError("No Cutscene asset assigned to this trigger!");
            onComplete?.Invoke();
            return;
        }
        manager.PlayCutscene(cutscene, onComplete);
    }
}
