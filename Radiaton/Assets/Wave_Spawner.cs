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
    public float spawnInterval = 1f; // Now adjustable per wave
    public bool useTimer = false;
    public float waveDuration = 30f;
    public bool isCutsceneWave = false;
    public GameObject cutsceneTrigger;
    public bool IsBoss = false;
    }

public class Wave_Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Transform BossSpawnPoint;
    public Text waveNameText;
    public Text waveTimerText;

    private Wave currentWave;
    private int currentWaveNumber = 0;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private float waveEndTime;
    private float timeLeft;
    
    void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogError("No waves assigned in the Inspector!");
            return;
        }

        StartWave();
    }

    void Update()
    {
        if (currentWave == null) return;

        // Check if all enemies are dead
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool allEnemiesDead = totalEnemies.Length == 0 && !canSpawn;

        // Timer-based wave progression
        if (currentWave.useTimer)
        {
            timeLeft = Mathf.Max(0, waveEndTime - Time.time);
            waveTimerText.text = timeLeft > 0 ? $"Time Left: {Mathf.Ceil(timeLeft)}s" : "Next Wave Incoming!";

            // If time runs out OR all enemies are dead, advance the wave
            if (timeLeft <= 0 || allEnemiesDead)
            {
                AdvanceWave();
                return;
            }
        }
        else
        {
            waveTimerText.text = "KILL EVERYTHING!";

            // Kill-based wave progression
            if (allEnemiesDead && currentWaveNumber + 1 < waves.Length)
            {
                AdvanceWave();
                return;
            }
        }

        // Spawn enemies if it's not a cutscene wave
        if (!currentWave.isCutsceneWave)
        {
            SpawnWave();
        }
    }

    void StartWave()
    {
        currentWave = waves[currentWaveNumber];

        if (waveNameText != null)
            waveNameText.text = $"Wave: {currentWave.waveName}";
        else
            Debug.LogError("WaveNameText UI element is not assigned!");

        canSpawn = true;

        if (currentWave.useTimer)
        {
            waveEndTime = Time.time + currentWave.waveDuration;
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
            if (IsBoss = false)
               { 
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
               }
            

          Instantiate(randomEnemy, BossSpawnPoint.position, Quaternion.identity);
        }
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval; // Uses per-wave spawn interval

            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
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
        else
        {
            Debug.Log("All waves completed!");
        }
    }
}

