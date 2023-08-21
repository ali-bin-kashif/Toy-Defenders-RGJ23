using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ShopItem
{
    public Sprite itemImage;
    public string itemInfo;
    public int Cost;
    public bool isOwned;

}

public class Shop : MonoBehaviour
{

    int itemSelected;
    public ShopItem[] shopItems;

    public Image towerCard;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI towerCost, buyText;
    public Button buyButton;

    public Button leftButton, rightButton;

    public MainMenuUI mainMenu;

    public TextMeshProUGUI coinsHUD;

    public AudioSource Audio;


    // Start is called before the first frame update
    void Start()
    {
        itemSelected = 0;
        towerCard.sprite = shopItems[itemSelected].itemImage;
        infoText.text = shopItems[itemSelected].itemInfo;
        towerCost.text = "FREE";
        if (shopItems[itemSelected].isOwned)
        {
            buyButton.interactable = false;
            buyText.text = "OWNED";
        }
        else
        {
            buyButton.interactable = true;
            buyText.text = "BUY";
        }
        
    }

    void UpdateShopUI()
    {
        towerCard.sprite = shopItems[itemSelected].itemImage;
        infoText.text = shopItems[itemSelected].itemInfo;
        if(itemSelected == 0)
        {
            towerCost.text = "FREE";
        }
        else
        {
            towerCost.text = shopItems[itemSelected].Cost.ToString();
        }
        if (shopItems[itemSelected].isOwned)
        {
            buyButton.interactable = false;
            buyText.text = "OWNED";
        }
        else
        {
            if(mainMenu.Coins >= shopItems[itemSelected].Cost)
                buyButton.interactable = true;
            else
                buyButton.interactable = false;

            buyText.text = "BUY";
        }

        if (itemSelected == 0)
        {
            leftButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
        }

        if(itemSelected == shopItems.Length-1)
        {
            rightButton.interactable = false;
        }
        else
        {
            rightButton.interactable = true;
        }
    }

    public void LeftButton()
    {

        if(itemSelected > 0)
        {
            itemSelected--;
            UpdateShopUI();
            Audio.Play();
        }

    }

    public void RightButton()
    {
        if(itemSelected < shopItems.Length-1)
        {
            itemSelected++;
            UpdateShopUI();
            Audio.Play();
        }

    }

    public void BuyItem()
    {
        if(mainMenu.Coins >= shopItems[itemSelected].Cost)
        {

            shopItems[itemSelected].isOwned = true;
            UpdateShopUI();
            mainMenu.Coins -= shopItems[itemSelected].Cost;
            PlayerPrefs.SetInt("Coins", mainMenu.Coins);
            coinsHUD.text = mainMenu.Coins.ToString();
        }
    }
}
