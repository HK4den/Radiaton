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
    public float spawnInterval = 1f;
    public bool useTimer = false;
    public float waveDuration = 30f;
    public bool isCutsceneWave = false;
    public GameObject cutsceneTrigger;
    public bool isBossWave = false;
    public GameObject[] bossEnemies;
    public bool isFinalWave = false; // New checkbox for final wave
}

public class Wave_Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Transform BossSpawnPoint;
    public Text waveNameText;
    public Text waveTimerText;
    public GameObject winScreen;
    public GameObject player;

    private Wave currentWave;
    private int currentWaveNumber = 0;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private float waveEndTime;
    private float timeLeft;
    private bool bossesSpawned = false;

    void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogError("No waves assigned in the Inspector!");
            return;
        }
        if (winScreen != null)
            winScreen.SetActive(false);

        StartWave();
    }

    void Update()
    {
        if (currentWave == null) return;

        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool allEnemiesDead = totalEnemies.Length == 0 && !canSpawn;

        if (currentWave.useTimer)
        {
            timeLeft = Mathf.Max(0, waveEndTime - Time.time);
            waveTimerText.text = timeLeft > 0 ? $"Time Left: {Mathf.Ceil(timeLeft)}s" : "Next Wave Incoming!";

            if (timeLeft <= 0 || allEnemiesDead)
            {
                AdvanceWave();
                return;
            }
        }
        else
        {
            waveTimerText.text = "KILL EVERYTHING!";
            if (allEnemiesDead)
            {
                AdvanceWave();
                return;
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
        bossesSpawned = false;

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
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);

            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }
        }

        if (currentWave.isBossWave && !bossesSpawned)
        {
            foreach (GameObject boss in currentWave.bossEnemies)
            {
                Instantiate(boss, BossSpawnPoint.position, Quaternion.identity);
            }
            bossesSpawned = true;
        }
    }

    void AdvanceWave()
    {
        if (currentWave.isFinalWave)
        {
            WinGame();
            return;
        }

        if (currentWaveNumber + 1 < waves.Length)
        {
            currentWaveNumber++;
            canSpawn = true;
            StartWave();
        }
        else
        {
            Debug.Log("All waves completed but no final wave marked. Consider setting isFinalWave = true.");
        }
    }

    void WinGame()
    {
        Debug.Log("Victory! All waves completed.");
        if (winScreen != null)
            winScreen.SetActive(true);

        if (player != null)
        {
            var dash = player.GetComponent<DashToMouse>();
            var move = player.GetComponent<PlayerMovement>();
            var shoot = player.GetComponent<shooting>();

            if (dash != null) dash.enabled = false;
            if (move != null) move.enabled = false;
            if (shoot != null) shoot.enabled = false;
        }
    }
}
