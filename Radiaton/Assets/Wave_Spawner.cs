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
    public bool isFinalWave = false;
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
    private bool cutsceneInProgress = false;

    private CutsceneManager cutsceneManager;

    void Start()
    {
        if (waves.Length == 0)
        {
            Debug.LogError("No waves assigned in the Inspector!");
            return;
        }

        cutsceneManager = FindObjectOfType<CutsceneManager>();
        if (cutsceneManager == null)
        {
            Debug.LogError("CutsceneManager not found in the scene!");
        }

        if (winScreen != null)
            winScreen.SetActive(false);

        StartWave();
    }

    void Update()
    {
        if (currentWave == null || cutsceneInProgress) return;

        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool allEnemiesDead = totalEnemies.Length == 0 && !canSpawn;

        if (currentWave.useTimer)
        {
            timeLeft = Mathf.Max(0, waveEndTime - Time.time);
            if (waveTimerText != null)
                waveTimerText.text = timeLeft > 0 ? $"Time Left: {Mathf.Ceil(timeLeft)}s" : "Next Wave Incoming!";

            if (timeLeft <= 0 || allEnemiesDead)
            {
                AdvanceWave();
                return;
            }
        }
        else
        {
            if (waveTimerText != null)
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

        canSpawn = true;

        if (currentWave.useTimer)
        {
            waveEndTime = Time.time + currentWave.waveDuration;
        }

        if (currentWave.isCutsceneWave)
        {
            if (currentWave.cutsceneTrigger != null)
            {
                GameObject triggerObj = Instantiate(currentWave.cutsceneTrigger, transform.position, Quaternion.identity);
                CutsceneTrigger trigger = triggerObj.GetComponent<CutsceneTrigger>();

                if (trigger != null && cutsceneManager != null)
                {
                    cutsceneInProgress = true;
                    trigger.StartCutscene(cutsceneManager, () =>
                    {
                        cutsceneInProgress = false;
                        canSpawn = true;
                    });
                }
                else
                {
                    Debug.LogWarning("CutsceneTrigger or CutsceneManager not set correctly.");
                    cutsceneInProgress = false;
                }
            }
            else
            {
                Debug.LogWarning("Cutscene marked true, but no cutsceneTrigger prefab assigned.");
                cutsceneInProgress = false;
            }

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
