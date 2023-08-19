using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;
    public GameObject SettingPanel;
    public GameObject InfoPanel;
    public GameObject ShopPanel;

    public GameObject fadePanel;

    public TextMeshProUGUI coinsHUD;

    public int Coins;

    // Start is called before the first frame update
    void Start()
    {
        Coins = PlayerPrefs.GetInt("Coins", 1000);
        coinsHUD.text = Coins.ToString();

        Time.timeScale = 1;
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        SettingPanel.SetActive(false);
        InfoPanel.SetActive(false);
        ShopPanel.SetActive(false);
    }



    public void PlayButton()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }

    public void ShopButton()
    {
        ShopPanel.SetActive(true);
        InfoPanel.SetActive(false);
        SettingPanel.SetActive(false);
    }

    public void InfoButton()
    {
        InfoPanel.SetActive(true);
        ShopPanel.SetActive(false);
        SettingPanel.SetActive(false);
    }

    public void SettingButton()
    {
        SettingPanel.SetActive(true);
        ShopPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void CloseButton()
    {
        SettingPanel.SetActive(false);
        ShopPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void BackButton()
    {
        LevelSelectPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public IEnumerator SelectLevel(int index)
    {
        fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }

    public void LevelButton(int index)
    {
        StartCoroutine(SelectLevel(index));
    }
}
