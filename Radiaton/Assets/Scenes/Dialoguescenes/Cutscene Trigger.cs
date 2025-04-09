using UnityEngine;
using System;

public class CutsceneTrigger : MonoBehaviour
{
    public Cutscene cutscene; // Assign this in the prefab or via inspector

    public void StartCutscene(CutsceneManager manager, Action onComplete)
    {
        if (cutscene == null)
        {
            Debug.LogError("Cutscene not assigned to CutsceneTrigger!");
            onComplete?.Invoke();
            return;
        }

        manager.PlayCutscene(cutscene, onComplete);
    }
}
