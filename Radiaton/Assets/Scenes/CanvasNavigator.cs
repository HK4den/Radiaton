using UnityEngine;
using System.Collections.Generic;

public class CanvasNavigator : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject aboutMenu;
    public GameObject creditsMenu;
    public GameObject tipsMenu;
    public GameObject controlsMenu;
    public GameObject enemiesMenu;
    public GameObject whatMenu;

    private Stack<GameObject> navigationStack = new Stack<GameObject>();
    private GameObject currentCanvas;

    void Start()
    {
        currentCanvas = mainMenu;
        currentCanvas.SetActive(true);
    }

    public void SwitchCanvas(GameObject newCanvas)
    {
        if (currentCanvas != null)
        {
            navigationStack.Push(currentCanvas);
            currentCanvas.SetActive(false);
        }

        currentCanvas = newCanvas;
        currentCanvas.SetActive(true);
    }

    public void GoBack()
    {
        if (navigationStack.Count > 0)
        {
            currentCanvas.SetActive(false);
            currentCanvas = navigationStack.Pop();
            currentCanvas.SetActive(true);
        }
    }
}

