using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneWatcher : MonoBehaviour
{
    public void GoToCaf()
    {
        SceneManager.LoadScene("Cafeteria cutscenes");
    }
    public void GoToDes()
    {
        SceneManager.LoadScene("Desert cutscenes");
    }

}

