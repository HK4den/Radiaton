using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
    public bool useTimer = false; // If true, wave ends when timer expires
    public float waveDuration = 30f; // Default wave time
    public bool isCutsceneWave = false; // If true, spawns cutscene trigger instead of enemies
    public GameObject cutsceneTrigger; // Object to spawn for cutscene waves
}

public class Wave_Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public WaveUIManager waveUI; // Reference to the UI Manager

    private Wave currentWave;
    private int currentWaveNumber = 0;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private float waveEndTime;
    private float timeLeft;

    void Start()
    {
        StartWave(); // Start the first wave
    }

    void Update()
    {
        if (currentWave.useTimer)
        {
            timeLeft = waveEndTime - Time.time; // Update countdown
            waveUI.UpdateWaveTimer(timeLeft); // Update UI

            if (timeLeft <= 0 && canSpawn)
            {
                AdvanceWave();
            }
        }

        if (!currentWave.useTimer)
        {
            GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 < waves.Length)
            {
                AdvanceWave();
            }
        }

        if (!currentWave.isCutsceneWave)
        {
            SpawnWave();
        }
    }

    void StartWave()
    {
        currentWave = waves[currentWaveNumber];

        waveUI.UpdateWaveName(currentWaveNumber); // Update wave number in UI

        if (currentWave.useTimer)
        {
            waveEndTime = Time.time + currentWave.waveDuration;
            timeLeft = currentWave.waveDuration; // Initialize timeLeft
        }

        if (currentWave.isCutsceneWave)
        {
            Instantiate(currentWave.cutsceneTrigger, transform.position, Quaternion.identity);
            canSpawn = false;
        }
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time && currentWave.noOfEnemies > 0)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }

    void AdvanceWave()
    {
        if (currentWaveNumber + 1 < waves.Length)
        {
            currentWaveNumber++;
            canSpawn = true;
            StartWave();
        }
    }
}
