using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ToyTower
{
    public string towerName;
    public GameObject towerPrefab;
    public int towerCost;
    public bool isUnlocked;
    public bool canBuy;
    public bool isPurchased;
    public bool isSelected;
}


public class Inventory : MonoBehaviour
{

    public ToyTower[] toyTowers;

    public int Coins;

    int _selectedTower;

    public GamePlayUI gameUI;

    bool isSelection;

    private void Awake()
    {
        CheckBuyCriteria();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && isSelection)
        {
            UnselectTowers();
        }
    }

    //Function to spawn tower at platform when tapped
    public bool SummonTower(Transform spawnpoint)
    {
        ToyTower tower = toyTowers[_selectedTower];
        
        if (tower.isPurchased && tower.isUnlocked && tower.isSelected)
        {
            tower.isSelected = false;
            Instantiate(tower.towerPrefab, spawnpoint.position, tower.towerPrefab.transform.rotation);
            tower.isPurchased = false;
            UnselectTowers();
            gameUI.UpdateDeckUI(_selectedTower);
            return true;
        }
        UnselectTowers();
        return false;
    }

    //Tower selection functions, will be executed when tap on cards
    public void SelectTurret()
    {
        UnselectTowers();
        ToyTower tower = toyTowers[0];
        if (tower.isUnlocked) 
        {
            if (!tower.isPurchased && Coins >= tower.towerCost)
            {
                tower.isPurchased = true;
                Coins -= tower.towerCost;
                CheckBuyCriteria();
            }
            else
            {
                _selectedTower = 0;
                tower.isSelected = true;
                isSelection = true;
            }
                
        }  

    }

    public void SelectCannon()
    {
        UnselectTowers();
        ToyTower tower = toyTowers[1];
        if (tower.isUnlocked)
        {
            if (!tower.isPurchased && Coins >= tower.towerCost)
            {
                tower.isPurchased = true;
                Coins -= tower.towerCost;
                CheckBuyCriteria();
            }
            else
            {
                _selectedTower = 1;
                tower.isSelected = true;
                isSelection = true;
            }

        }
    }

    public void SelectMortar()
    {
        UnselectTowers();
        ToyTower tower = toyTowers[2];
        if (tower.isUnlocked)
        {
            if (!tower.isPurchased && Coins >= tower.towerCost)
            {
                tower.isPurchased = true;
                Coins -= tower.towerCost;
                CheckBuyCriteria();
            }
            else
            {
                _selectedTower = 2;
                tower.isSelected = true;
                isSelection = true;
            }

        }
    }

    public void UnselectTowers()
    {
        for(int i=0; i < toyTowers.Length; i++)
        {
            toyTowers[i].isSelected = false;
            gameUI.UpdateDeckUI(i);
            isSelection = false;
        }
    }

    public void CheckBuyCriteria()
    {
        foreach(ToyTower tower in toyTowers)
        {
            if(Coins >= tower.towerCost)
            {
                tower.canBuy = true;
            }
            else
            {
                tower.canBuy = false;
            }
        }
    }
}
