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
    public AudioClip shopButton, purchaseSound;

    //Values to save status for tower cards ( 0 - False, 1 - True) using int since player prefabs doesn't have bool property
    int cannonStatus, mortarStatus, plasmaStatus;


    // Start is called before the first frame update
    void Start()
    {
        itemSelected = 0;
        cannonStatus = PlayerPrefs.GetInt("Cannon", 0);
        mortarStatus = PlayerPrefs.GetInt("Mortar", 0);
        plasmaStatus = PlayerPrefs.GetInt("Plasma", 0);

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

        if(cannonStatus == 1) //Here means true
        {
            shopItems[1].isOwned = true;
        }

        if (mortarStatus == 1) //Here means true
        {
            shopItems[2].isOwned = true;
        }

        if (plasmaStatus == 1) //Here means true
        {
            shopItems[3].isOwned = true;
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
            Audio.PlayOneShot(shopButton);
        }

    }

    public void RightButton()
    {
        if(itemSelected < shopItems.Length-1)
        {
            itemSelected++;
            UpdateShopUI();
            Audio.PlayOneShot(shopButton);
        }

    }

    public void BuyItem()
    {
        if(mainMenu.Coins >= shopItems[itemSelected].Cost)
        {
            Audio.PlayOneShot(purchaseSound,1f);
            shopItems[itemSelected].isOwned = true;

            if(itemSelected == 1)
            {
                PlayerPrefs.SetInt("Cannon", 1);
            }

            if (itemSelected == 2)
            {
                PlayerPrefs.SetInt("Mortar", 1);
            }

            if (itemSelected == 3)
            {
                PlayerPrefs.SetInt("Plasma", 1);
            }
            UpdateShopUI();
            mainMenu.Coins -= shopItems[itemSelected].Cost;
            PlayerPrefs.SetInt("Coins", mainMenu.Coins);
            coinsHUD.text = mainMenu.Coins.ToString();
        }
    }
}
