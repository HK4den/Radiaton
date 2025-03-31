using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VictoryScreen : MonoBehaviour

{

public void RestartButton()
{

 SceneManager.LoadScene("Cade test area");
}

public void ExitButton()
{
SceneManager.LoadScene("MainMenu");
}


}
