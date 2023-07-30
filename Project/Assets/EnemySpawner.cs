using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public string name;
    public int health;
    public int damage;
    public GameObject prefab; // Prefab of the enemy to be instantiated
}

public class EnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public List<Enemy> enemiesList = new List<Enemy>(); // List of enemies

    public int maxEnemiesAllowed = 5;
    private int spawnedEnemiesCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemiesCount < maxEnemiesAllowed)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f)); // Adjust the spawn interval as needed

            if (spawnedEnemiesCount >= maxEnemiesAllowed)
                yield break;

            SpawnRandomEnemy();
        }
    }

       void SpawnRandomEnemy()
    {
        if (enemiesList.Count == 0)
        {
            Debug.LogWarning("No enemies in the list.");
            return;
        }

        int randomIndex = Random.Range(0, enemiesList.Count);
        Enemy enemyToSpawn = enemiesList[randomIndex];

        if (enemyToSpawn.prefab != null)
        {
            GameObject spawnedEnemy = Instantiate(enemyToSpawn.prefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemiesCount++;
            Debug.Log("A " + enemyToSpawn.name + " has spawned!");
        }
        else
        {
            Debug.LogWarning("Prefab for " + enemyToSpawn.name + " is not assigned.");
        }
    }
}
