using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    public int currentWaveIndex = 0;

    public bool isWaveOngoing = false;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (isWaveOngoing == false && currentWaveIndex <= waves.Length - 1)
        {
            isWaveOngoing = true;
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
        {
            Enemy newEnemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint);
            gameController.AddEnemy(newEnemy);
            gameController.waveHasStarted = true;
            if (gameController.gameHasStarted == false)
            {
                gameController.gameHasStarted = true;
            }

            yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
        }
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
}
