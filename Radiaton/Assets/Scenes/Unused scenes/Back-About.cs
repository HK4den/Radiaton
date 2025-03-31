using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Backabout : MonoBehaviour
{
    public void GoToAbout()
    {
        SceneManager.LoadScene("About");
    }

}
