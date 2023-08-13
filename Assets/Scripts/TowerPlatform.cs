using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlatform : MonoBehaviour
{

    Transform _spawnPoint;

    Inventory toyInventory;

    void Start()
    {
        _spawnPoint = transform.GetChild(0); //Get the spawn point transform which is child

        toyInventory = GameObject.FindObjectOfType<Inventory>();
    }

    private void OnMouseDown()
    {

        toyInventory.SummonTower(_spawnPoint);
    }


}
