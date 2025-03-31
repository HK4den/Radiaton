using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject player; // Assign player GameObject in Inspector

    private bool isPaused = false;
    private DashToMouse dashScript;
    private PlayerMovement moveScript;
    private shooting shootScript;

    void Start()
    {
        if (player != null)
        {
            dashScript = player.GetComponent<DashToMouse>();
            moveScript = player.GetComponent<PlayerMovement>();
            shootScript = player.GetComponent<shooting>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (player != null)
        {
            dashScript.enabled = false;
            moveScript.enabled = false;
            shootScript.enabled = false;
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (player != null)
        {
            dashScript.enabled = true;
            moveScript.enabled = true;
            shootScript.enabled = true;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
