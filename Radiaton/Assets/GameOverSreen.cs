using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour

{
public void RestartButtonC()
    {  
        SceneManager.LoadScene("CadeTestArea");
    }

    public void RestartButtonD()
    {
        SceneManager.LoadScene("Desert");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
