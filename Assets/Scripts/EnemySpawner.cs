using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab; // Prefab of the enemy to be instantiated
    public int enemyCount;
    public float enemyDelay;
}

[System.Serializable]
public class Wave
{
    public string waveName;
    public string waveText;
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
    public bool isLevelWon;

    public float percentageComplete;

    public GamePlayUI gameUI;

    public TextMeshProUGUI waveText;

    void Start()
    {

        state = SpawnState.COUNTING;
        StartCoroutine(DisplayWaveText());

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
                if(wavesCompleted != true)
                {
                    Instantiate(enemy.enemyPrefab, spawnPoint.position, Quaternion.identity);
                    percentageComplete += 2;
                    yield return new WaitForSeconds(enemy.enemyDelay);

                }  
            }
            yield return new WaitForSeconds(_wave.spawnDelay);


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
            wavesCompleted = true;
            isLevelWon = true;
            percentageComplete = 100;
            state = SpawnState.IDLE;
        }
        else
        {
            waveText.text = waves[currentWave].waveText;
            StartCoroutine(DisplayWaveText());
            currentWave++;
            percentageComplete = ((float)currentWave / waves.Length) * 100;
            waveCountDown = 5;
            state = SpawnState.COUNTING;
        }

    }

    IEnumerator DisplayWaveText()
    {
        waveText.GetComponent<Animator>().SetTrigger("WaveText");
        yield return new WaitForSeconds(0.75f);
        gameUI.uiAudio.PlayOneShot(gameUI.panelSwipe);
    }


}




