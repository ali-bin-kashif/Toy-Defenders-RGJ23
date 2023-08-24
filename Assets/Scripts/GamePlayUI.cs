using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText, bonusCoins, updateCoins;

    public Button[] towerCards;

    //Buttons and HUD Game Objects
    public GameObject coinHUD,pauseBtn;

    //Screens and menus Game Objects
    public GameObject pauseMenu, winMenu, lossMenu;

    public Slider progressBar;

    public Color disabledColor;

    public Animator Deck, coinsHUDAnim;

    public EnemySpawner _waveSystem;

    Inventory _playerInventory;

    bool runOnce;

    public AudioSource uiAudio;
    public AudioClip buttonTap, purchaseTower, deckTap, gameWin, gameLoss, panelSwipe, doorOpen;

    public float doorSoundDelay;


    void DoorOpenSound()
    {
        uiAudio.PlayOneShot(doorOpen, 0.7f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        _playerInventory = GameObject.FindObjectOfType<Inventory>();
        uiAudio = GetComponent<AudioSource>();

        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        lossMenu.SetActive(false);

        coinsText.text = _playerInventory.Coins.ToString();


        //Setting the UI of deck according to the inventory and available towers
        for (int i = 0; i < _playerInventory.toyTowers.Length; i++)
        {
            ToyTower tower = _playerInventory.toyTowers[i];
            if (tower.isUnlocked)
            {
                towerCards[i].interactable = true;
                if(!tower.isPurchased)
                {
                    if(tower.canBuy)
                    {
                        towerCards[i].GetComponent<Animator>().SetBool("canBuy", true);
                        towerCards[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        towerCards[i].GetComponent<Animator>().SetBool("canBuy", false);
                        towerCards[i].transform.GetChild(1).gameObject.SetActive(true);
                    }

                }
            }
            else
            {
                towerCards[i].interactable = false;
            }
        }

        //Playing door sound with a delay
        Invoke("DoorOpenSound", doorSoundDelay);
    }

    // Update is called once per frame
    void Update()
    {

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

        if(_playerInventory.towerCount == _playerInventory.platformCount)
        {
            foreach(Button card in towerCards)
            {
                card.GetComponent<Animator>().SetBool("canBuy", false);
                card.transform.GetChild(1).gameObject.SetActive(false);
                card.transform.GetChild(0).gameObject.SetActive(false);
            }
            return;
        }
        ToyTower tower = _playerInventory.toyTowers[id];
        
        if (tower.isUnlocked)
        {
            if(tower.isPurchased)
            {
                towerCards[id].transform.GetChild(0).gameObject.SetActive(false);
                //towerCards[id].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                towerCards[id].GetComponent<Animator>().SetBool("isPurchased", true);
                uiAudio.Play();
            }
            else
            {
                if (tower.canBuy)
                {
                    towerCards[id].GetComponent<Animator>().SetBool("canBuy", true);
                    towerCards[id].transform.GetChild(0).gameObject.SetActive(true);
                    towerCards[id].transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    towerCards[id].GetComponent<Animator>().SetBool("canBuy", false);
                    towerCards[id].transform.GetChild(1).gameObject.SetActive(true);
                    towerCards[id].transform.GetChild(0).gameObject.SetActive(false);
                }
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
        uiAudio.PlayOneShot(buttonTap);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
        coinHUD.SetActive(true);
        progressBar.gameObject.SetActive(true);
        uiAudio.PlayOneShot(buttonTap);

    }

    public void RetryButton()
    {
       // audio.Play();
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
        
    }

    public void NextLevelButton()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index+1);
    }

    public void GoToScene(int index)
    {
       // audio.Play();
        SceneManager.LoadScene(index);
        
    }

    IEnumerator GameLost()
    {
        uiAudio.PlayOneShot(gameLoss);
        pauseBtn.SetActive(false);
        coinHUD.SetActive(false);
        progressBar.gameObject.SetActive(false);
        Deck.SetTrigger("DeckEnd");
        yield return new WaitForSeconds(1f);
        Deck.gameObject.SetActive(false);
        uiAudio.PlayOneShot(panelSwipe);
        lossMenu.SetActive(true);
    }

    IEnumerator GameWin()
    {
        pauseBtn.SetActive(false);
        coinHUD.SetActive(false);
        progressBar.GetComponent<Animator>().SetTrigger("Win");
        uiAudio.PlayOneShot(gameWin);
        Deck.SetTrigger("DeckEnd");

        yield return new WaitForSeconds(2f);
        bonusCoins.text = _playerInventory.Coins.ToString();
        progressBar.gameObject.SetActive(false);
        Deck.gameObject.SetActive(false);
        uiAudio.PlayOneShot(panelSwipe);
        winMenu.SetActive(true);

        //Save earned coins
        int coins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coins + _playerInventory.Coins);

        //Unlock next level
        int levels = PlayerPrefs.GetInt("UnlockedLevels");
        int index = SceneManager.GetActiveScene().buildIndex;
        if ( levels < ( index + 1 ) )
        { 
            PlayerPrefs.SetInt("UnlockedLevels", index + 1);
        }
        
    }

    public void CoinsUpdate(int coins, char sign)
    {
        updateCoins.text = sign + coins.ToString();
        coinsHUDAnim.SetTrigger("CoinsUpdate");
        //To update coins value of coin HUD
        coinsText.text = _playerInventory.Coins.ToString();
    }



}
