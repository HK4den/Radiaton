using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenesCredits : MonoBehaviour
{
    public void GoToScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
