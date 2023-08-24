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

    public TowerPlatform[] Platforms;

    public int Coins, towerCount, platformCount;

    int _selectedTower;

    public GamePlayUI gameUI;

    bool isSelection;


    private void Start()
    {
        //Values to get saved status for tower cards ( 0 - False, 1 - True) using int since player prefabs doesn't have bool property
        int cannonStatus, mortarStatus, plasmaStatus;

        cannonStatus = PlayerPrefs.GetInt("Cannon", 0);
        mortarStatus = PlayerPrefs.GetInt("Mortar", 0);
        plasmaStatus = PlayerPrefs.GetInt("Plasma", 0);

        if(cannonStatus == 1)
        {
            toyTowers[1].isUnlocked = true;
        }
        else
        {
            toyTowers[1].isUnlocked = false;
        }

        if (mortarStatus == 1)
        {
            toyTowers[2].isUnlocked = true;
        }
        else
        {
            toyTowers[2].isUnlocked = false;
        }

        if (plasmaStatus == 1)
        {
            toyTowers[3].isUnlocked = true;
        }
        else
        {
            toyTowers[3].isUnlocked = false;
        }

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
    
            towerCount++;
            gameUI.UpdateDeckUI(_selectedTower);
            
            return true;
        }
        UnselectTowers();
        return false;
    }

    //Tower selection function, will be executed when tap on cards

    public void SelectTower(int index)
    {
        UnselectTowers();
        ToyTower tower = toyTowers[index];
        if (tower.isUnlocked)
        {
            if (!tower.isPurchased && Coins >= tower.towerCost)
            {
                tower.isPurchased = true;
                gameUI.uiAudio.PlayOneShot(gameUI.purchaseTower);
                Coins -= tower.towerCost;

                _selectedTower = index;
                tower.isSelected = true;
                isSelection = true;

                gameUI.CoinsUpdate(tower.towerCost,'-');
                CheckBuyCriteria();
            }
            else
            {
                _selectedTower = index;
                tower.isSelected = true;
                gameUI.uiAudio.PlayOneShot(gameUI.deckTap);
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
        for(int i=0 ; i < toyTowers.Length ; i++)
        {
            ToyTower tower = toyTowers[i];
            if(Coins >= tower.towerCost)
            {
                tower.canBuy = true; 
            }
            else
            {
                tower.canBuy = false;
            }
            gameUI.UpdateDeckUI(i);
        }
    }
}
