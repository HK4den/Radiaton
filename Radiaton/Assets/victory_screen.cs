using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class victorySCreen : MonoBehaviour

{

public void RestartButton()
{

 SceneManager.LoadScene("Cade test area");
}

public void MainMenuButton()
{
SceneManager.LoadScene("MainMenu");
}


}
