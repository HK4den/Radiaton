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

    // Determines if the wave should pause at the start (stopping time and disabling controls)
    public bool pauseAtStart = true;
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

    // Delay between spawning each boss in a boss wave.
    public float bossSpawnDelay = 0.5f;

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
        // Do not update enemy spawning if there is no current wave or if a cutscene is in progress.
        if (currentWave == null || cutsceneInProgress) return;

        // Gather all enemies present in the scene.
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
            // If it's a cutscene wave, display "Dialogue...", otherwise prompt "KILL EVERYTHING!".
            if (waveTimerText != null)
                waveTimerText.text = currentWave.isCutsceneWave ? "Dialogue..." : "KILL EVERYTHING!";

            if (allEnemiesDead)
            {
                AdvanceWave();
                return;
            }
        }

        // For waves that are not purely cutscene waves, spawn enemies.
        if (!currentWave.isCutsceneWave)
        {
            SpawnWave();
        }
    }

    void StartWave()
    {
        currentWave = waves[currentWaveNumber];
        bossesSpawned = false;
        canSpawn = true;

        if (waveNameText != null)
            waveNameText.text = $"Wave: {currentWave.waveName}";

        if (currentWave.useTimer)
        {
            waveEndTime = Time.time + currentWave.waveDuration;
        }

        // If the wave is configured to pause at the start (even if not a full cutscene wave)
        if (currentWave.pauseAtStart && cutsceneManager != null)
        {
            cutsceneManager.pauseGameDuringCutscene = true;
            Time.timeScale = 0f;
            // Wait for input to resume this wave.
            StartCoroutine(WaitForResume());
        }
        // If the wave is marked as a cutscene wave, trigger the cutscene.
        else if (currentWave.isCutsceneWave)
        {
            if (currentWave.cutsceneTrigger != null)
            {
                GameObject triggerObj = Instantiate(currentWave.cutsceneTrigger, transform.position, Quaternion.identity);
                CutsceneTrigger trigger = triggerObj.GetComponent<CutsceneTrigger>();

                if (trigger != null && cutsceneManager != null)
                {
                    cutsceneInProgress = true;
                    // Trigger the cutscene; once finished, automatically advance to the next wave.
                    trigger.TriggerCutscene(cutsceneManager, () =>
                    {
                        cutsceneInProgress = false;
                        AdvanceWave();
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
                Debug.LogWarning("Wave marked as cutscene but no cutsceneTrigger prefab assigned.");
                cutsceneInProgress = false;
            }
            // Prevent enemy spawning for this wave.
            canSpawn = false;
        }
    }

    IEnumerator WaitForResume()
    {
        // Wait for the player to press the Space key to resume the wave.
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        // Resume time.
        Time.timeScale = 1f;
        if (cutsceneManager != null)
        {
            cutsceneInProgress = false;
            cutsceneManager.pauseGameDuringCutscene = false;
            // Re-enable player controls.
            cutsceneManager.EnablePlayerControls();
        }
    }

    void SpawnWave()
    {
        if (canSpawn && Time.time >= nextSpawnTime && currentWave.noOfEnemies > 0)
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

        // If this wave is a boss wave and bosses haven't been spawned yet, spawn bosses sequentially.
        if (currentWave.isBossWave && !bossesSpawned)
        {
            StartCoroutine(SpawnBosses());
            bossesSpawned = true; // Prevent re-triggering the boss spawn coroutine.
        }
    }

    IEnumerator SpawnBosses()
    {
        foreach (GameObject boss in currentWave.bossEnemies)
        {
            Instantiate(boss, BossSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(bossSpawnDelay);
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

            if (dash != null)
                dash.enabled = false;
            if (move != null)
                move.enabled = false;
            if (shoot != null)
                shoot.enabled = false;
        }
    }
}
