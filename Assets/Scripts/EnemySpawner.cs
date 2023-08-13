using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public string enemyName;
    public GameObject enemyPrefab; // Prefab of the enemy to be instantiated
    public int enemyCount;
}

[System.Serializable]
public class Wave
{
    public string waveName;
    public string waveDescription;
    public float spawnDelay;
    public Enemy[] Enemies;
}


public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;

    public Transform spawnPoint;

    public float waveCountDown;

    public int currentWave = 0;
    float searchCountDown = 1f;

    public enum SpawnState { IDLE, SPAWNING, WAITING, COUNTING };  // states for round

    public SpawnState state;
    public bool wavesCompleted;

    void Start()
    {
        state = SpawnState.COUNTING;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (AllEnemiesDead())
            {
                // Start new wave
                WaveCompleted();

            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0 && state != SpawnState.IDLE)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
            }
        }
        else if (state == SpawnState.COUNTING)
        {
            waveCountDown -= Time.deltaTime;
        }

    }


    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        foreach(Enemy enemy in _wave.Enemies)
        {
            for( int i=0 ; i < enemy.enemyCount ; i++)
            {
                Instantiate(enemy.enemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(_wave.spawnDelay);
            }
                 
        }

        state = SpawnState.WAITING;

        yield break;
    }

    bool AllEnemiesDead()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return true;
            }
        }
        return false;
    }

    void WaveCompleted()
    {
        //Debug.Log("Wave Completed !");
        if (currentWave >= waves.Length - 1)
        {
            // Level Completed, all waves done.
            //Debug.Log(currentWave);
            //Debug.Log("Level Completed !");
            wavesCompleted = true;
            state = SpawnState.IDLE;
            //currentWave = 0;
        }
        else
        {
            currentWave++;
            waveCountDown = 10;
            state = SpawnState.COUNTING;
        }

    }


}




