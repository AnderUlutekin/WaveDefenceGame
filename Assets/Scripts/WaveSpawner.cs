using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private int currentWaveIndex = 0;

    [SerializeField]
    private bool isWaveOngoing = false;

    void Start()
    {
        
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
            Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint);

            yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
        }
        currentWaveIndex += 1;
    }
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
}
