using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveManager : MonoBehaviour
{

    public Text waveCounterText; // Reference to the UI Text component [1, 5, 8]

    private int currentWave = 0;



    public void StartNewWave()

    {

        currentWave++; 

        waveCounterText.text = "Wave: " + currentWave; // Update the UI text [1, 5, 8]

    }

}
