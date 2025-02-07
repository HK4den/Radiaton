using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes2 : MonoBehaviour
{
    public void GoToScene()
    {
        SceneManager.LoadScene("Credits");
    }

}