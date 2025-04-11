using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes1 : MonoBehaviour
{
    public void GoToCaf()
    {
        SceneManager.LoadScene("cadetestarea");
    }
    public void GoToDes()
    {
        SceneManager.LoadScene("Desert");
    }

}
