using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ToyTower
{
    public string towerName;
    public GameObject towerPrefab;
    public int towerCount;
    public bool isUnlocked;
}


public class Inventory : MonoBehaviour
{

    public ToyTower[] toyTowers;

    public int selectedTower;
    
    

    public void SummonTower(Transform spawnpoint)
    {
        ToyTower tower = toyTowers[selectedTower];
        if (tower.towerCount > 0 && tower.isUnlocked)
        {
            Instantiate(tower.towerPrefab, spawnpoint.position, tower.towerPrefab.transform.rotation);
            tower.towerCount--;
        }
    }
}
