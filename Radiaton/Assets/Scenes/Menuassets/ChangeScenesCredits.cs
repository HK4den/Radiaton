using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenesCredits : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoToTips()
    {
        SceneManager.LoadScene("Tips");
    }
    public void GoToControls()
    {
        SceneManager.LoadScene("Controls");
    }
}
