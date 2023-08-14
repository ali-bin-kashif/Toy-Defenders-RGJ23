using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ToyTower
{
    public string towerName;
    public GameObject towerPrefab;
    public bool isUnlocked;
    public bool isPurchased;
    public bool isSelected;
}


public class Inventory : MonoBehaviour
{

    public ToyTower[] toyTowers;

    public int Coins;

    int _selectedTower;

    public GamePlayUI gameUI;


    //Function to spawn tower at platform when tapped
    public void SummonTower(Transform spawnpoint)
    {
        ToyTower tower = toyTowers[_selectedTower];
        
        if (tower.isPurchased && tower.isUnlocked && tower.isSelected)
        {
            tower.isSelected = false;
            Instantiate(tower.towerPrefab, spawnpoint.position, tower.towerPrefab.transform.rotation);
            tower.isPurchased = false;
            gameUI.UpdateDeckUI(_selectedTower);
        }
        UnselectTowers();
    }

    //Tower selection functions, will be executed when tap on cards
    public void SelectTurret()
    {
        UnselectTowers();
        ToyTower tower = toyTowers[0];
        if (tower.isUnlocked) 
        {
            if (!tower.isPurchased && Coins >= 200)
            {
                tower.isPurchased = true;
                Coins -= 200;
            }
            else
            {
                _selectedTower = 0;
                tower.isSelected = true;
            }
                
        }
        

    }

    public void SelectCannon()
    {
        UnselectTowers();
        ToyTower tower = toyTowers[1];
        if (tower.isUnlocked)
        {
            if (!tower.isPurchased && Coins >= 300)
            {
                tower.isPurchased = true;
                Coins -= 300;
            }
            else
            {
                _selectedTower = 1;
                tower.isSelected = true;
            }

        }
    }

    public void UnselectTowers()
    {
        for(int i=0; i < toyTowers.Length; i++)
        {
            toyTowers[i].isSelected = false;
            gameUI.UpdateDeckUI(i);
        }
    }
}
