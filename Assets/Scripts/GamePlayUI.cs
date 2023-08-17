using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText, bonusCoins;

    public Button[] towerCards;

    //Buttons and HUD Game Objects
    public GameObject coinHUD,pauseBtn;

    //Screens and menus Game Objects
    public GameObject pauseMenu, winMenu, lossMenu;

    public Slider progressBar;

    public Color disabledColor;

    public Animator Deck;

    public EnemySpawner _waveSystem;

    Inventory _playerInventory;

    bool runOnce;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _playerInventory = GameObject.FindObjectOfType<Inventory>();

        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        lossMenu.SetActive(false);

        //Setting the UI of deck according to the inventory and available towers
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

        progressBar.value = _waveSystem.percentageComplete;


        //Enabling Win and loss menu after level end
        if (_waveSystem.wavesCompleted && _waveSystem.isLevelWon && !runOnce) //Game win condition
        {
            StartCoroutine(GameWin());
            runOnce = true;
        }
        else if (_waveSystem.wavesCompleted && !_waveSystem.isLevelWon && !runOnce) //Game loss condition
        {
            StartCoroutine(GameLost());
            runOnce = true;
        }
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

    public void PauseButton()
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        coinHUD.SetActive(false);
        progressBar.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
        coinHUD.SetActive(true);
        progressBar.gameObject.SetActive(true);
        
    }

    public void RetryButton()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    IEnumerator GameLost()
    {
        pauseBtn.SetActive(false);
        coinHUD.SetActive(false);
        progressBar.gameObject.SetActive(false);
        Deck.SetTrigger("DeckEnd");
        yield return new WaitForSeconds(1f);
        Deck.gameObject.SetActive(false);
        lossMenu.SetActive(true);
    }

    IEnumerator GameWin()
    {
        pauseBtn.SetActive(false);
        coinHUD.SetActive(false);
        progressBar.GetComponent<Animator>().SetTrigger("Win");
        Deck.SetTrigger("DeckEnd");
        yield return new WaitForSeconds(3f);
        bonusCoins.text = _playerInventory.Coins.ToString();
        progressBar.gameObject.SetActive(false);
        Deck.gameObject.SetActive(false);
        winMenu.SetActive(true);
    }




}
