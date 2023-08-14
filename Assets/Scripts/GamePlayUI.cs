using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    public Button[] towerCards;

    public Color disabledColor;

    Inventory _playerInventory;


    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindObjectOfType<Inventory>();

        for (int i = 0; i < _playerInventory.toyTowers.Length; i++)
        {
            if (_playerInventory.toyTowers[i].isUnlocked)
            {
                towerCards[i].interactable = true;
                if(!_playerInventory.toyTowers[i].isPurchased)
                {
                    towerCards[i].transform.GetChild(0).gameObject.SetActive(true); // Setting cost bar to true
                    towerCards[i].GetComponent<Image>().color = disabledColor; //Applying dull color to unpurchased item

                }
            }
            else
            {
                towerCards[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //To update coins value of coin HUD
        coinsText.text = _playerInventory.Coins.ToString();
        
    }

    public void UpdateDeckUI(int id)
    {
        ToyTower tower = _playerInventory.toyTowers[id];
        if (tower.isUnlocked)
        {
            if(tower.isPurchased)
            {
                towerCards[id].transform.GetChild(0).gameObject.SetActive(false);
                towerCards[id].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                towerCards[id].GetComponent<Animator>().SetBool("isPurchased", true);
            }
            else
            {
                towerCards[id].transform.GetChild(0).gameObject.SetActive(true);
                towerCards[id].GetComponent<Image>().color = disabledColor;
                towerCards[id].GetComponent<Animator>().SetBool("isPurchased", false);
            }

            if(tower.isSelected && tower.isPurchased)
            {
                towerCards[id].GetComponent<Animator>().SetBool("isSelected", true);
            }
            
            if(!tower.isSelected)
            {
                towerCards[id].GetComponent<Animator>().SetBool("isSelected", false);
            }
        }
    }


}
