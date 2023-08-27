using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlatform : MonoBehaviour
{
    

    Transform _spawnPoint;

    Inventory _playerInventory;

    bool hasTower = false;

    Animator towerHUD;

    public Tutorial gameTutorial;

    int tutorialDone;

    void Start()
    {
        _spawnPoint = transform.GetChild(0); //Get the spawn point transform which is child

        _playerInventory = GameObject.FindObjectOfType<Inventory>();
        towerHUD = GetComponent<Animator>();
        
    }

    private void OnMouseDown()
    {
        if(!hasTower)
        {
            hasTower = _playerInventory.SummonTower(_spawnPoint);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tower"))
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            towerHUD.SetBool("isPlaced", true);
            
            if(gameTutorial != null)
            {
                tutorialDone = PlayerPrefs.GetInt("TutorialComplete", 0);
                if(tutorialDone == 0)
                {
                    gameTutorial.SecondStepComplete();
                }
                
            }
        }
    }


}
