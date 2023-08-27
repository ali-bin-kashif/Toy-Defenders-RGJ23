using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public AudioSource Audio;
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;
    public GameObject SettingPanel;
    public GameObject InfoPanel;
    public GameObject ShopPanel;

    public GameObject fadePanel;

    public TextMeshProUGUI coinsHUD;

    public Button musicToggle;
    public Sprite musicOn, musicOff;
    public AudioSource gameMusic;
    public AudioClip toggleSound;

    public Shop Shop;
    public GameObject shopBtn;
    public int Coins;
    int musicEnable;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;

        Audio = GetComponent<AudioSource>();

        Coins = PlayerPrefs.GetInt("Coins", 0);
        musicEnable = PlayerPrefs.GetInt("Music", 1);
        if(musicEnable == 1) //Here 1 means true(On)
        {
            musicToggle.GetComponent<Image>().sprite = musicOn;
            gameMusic.Play();
        }
        else
        {
            musicToggle.GetComponent<Image>().sprite = musicOff;
        }
        

        coinsHUD.text = Coins.ToString();

        Time.timeScale = 1;
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        SettingPanel.SetActive(false);
        InfoPanel.SetActive(false);
        ShopPanel.SetActive(false);

        foreach (ShopItem item in Shop.shopItems)
        {
            if (Coins >= item.Cost && !item.isOwned)
            {
                shopBtn.GetComponent<Animator>().SetBool("Alert", true);
                shopBtn.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }



    public void PlayButton()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
        Audio.Play();
    }

    public void ShopButton()
    {
        ShopPanel.SetActive(true);
        InfoPanel.SetActive(false);
        SettingPanel.SetActive(false);
        Audio.Play();
    }

    public void InfoButton()
    {
        InfoPanel.SetActive(true);
        ShopPanel.SetActive(false);
        SettingPanel.SetActive(false);
        Audio.Play();
    }

    public void SettingButton()
    {
        SettingPanel.SetActive(true);
        ShopPanel.SetActive(false);
        InfoPanel.SetActive(false);
        Audio.Play();
    }

    public void CloseButton()
    {
        SettingPanel.SetActive(false);
        ShopPanel.SetActive(false);
        InfoPanel.SetActive(false);
        Audio.Play();
    }

    public void BackButton()
    {
        LevelSelectPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        Audio.Play();
    }

    public IEnumerator SelectLevel(int index)
    {
        Audio.Play();
        fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }

    public void LevelButton(int index)
    {
        StartCoroutine(SelectLevel(index));
    }

    public void MusicToggle()
    {
        Audio.PlayOneShot(toggleSound,0.5f);
        if (musicEnable == 1)
        {
            musicEnable = 0;
            PlayerPrefs.SetInt("Music", musicEnable);
            musicToggle.GetComponent<Image>().sprite = musicOff;
            gameMusic.Stop();
        }
        else
        {
            musicEnable = 1;
            PlayerPrefs.SetInt("Music", musicEnable);
            musicToggle.GetComponent<Image>().sprite = musicOn;
            gameMusic.Play();
        }
        
    }
}
