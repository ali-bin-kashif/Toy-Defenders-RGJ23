using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlatform : MonoBehaviour
{

    Transform _spawnPoint;

    Inventory _playerInventory;

    bool hasTower = false;

    void Start()
    {
        _spawnPoint = transform.GetChild(0); //Get the spawn point transform which is child

        _playerInventory = GameObject.FindObjectOfType<Inventory>();
    }

    private void OnMouseDown()
    {
        if(!hasTower)
        {
            hasTower = _playerInventory.SummonTower(_spawnPoint);
        } 
    }


}
