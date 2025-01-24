using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float currentCountdown;
    [SerializeField] private GameObject spawnPoint;
    public Wave[] waves;
    public int currentWaveIndex = 0;
    private bool readyToCountDown;
    
    public float countdownTime = 10f;

  IEnumerator SpawnWave()
{
    if (currentWaveIndex < waves.Length)
    {
    for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
{
   Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);
   enemy.transform.SetParent(spawnPoint.transform);
    yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
}
}
}
void Update()
   {
    currentCountdown -= Time.deltaTime;
    if (readyToCountDown == true)
   {
    currentCountdown -= Time.deltaTime;
   } 
    
    if (currentCountdown <= 0 )
    {
        readyToCountDown = false;
        ResetTimer();
       currentCountdown -= Time.deltaTime;
       currentCountdown = waves[currentWaveIndex].timeToNextWave;
        StartCoroutine(SpawnWave());
    }
    if (waves[currentWaveIndex].enemiesLeft == 0)
    {
        readyToCountDown = true;
        currentWaveIndex++;
    }
   }


[System.Serializable]
public class Wave 
{
    public Enemy[] enemies; 
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}
void start()
{
    readyToCountDown = true;
    currentCountdown = countdownTime;
    
    for (int i = 0; i< waves.Length; i++)
    {
        waves[i].enemiesLeft = waves[i].enemies.Length;
    }
}
 void update()
{
   if (currentWaveIndex >= waves.Length)
   {
    Debug.Log("You Survived every wave!");
    return;
   }
}
void ResetTimer()
{
    currentCountdown = countdownTime;
}


}
