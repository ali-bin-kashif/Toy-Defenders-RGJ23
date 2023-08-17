using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject LevelSelectPanel;
    public GameObject SettingPanel;
    public GameObject InfoPanel;
    //public GameObject ShopPanel;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuPanel.SetActive(true);
        LevelSelectPanel.SetActive(false);
        SettingPanel.SetActive(false);
        InfoPanel.SetActive(false);
        //ShopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }

   /* public void ShopButton()
    {
        ShopPanel.SetActive(true);
    }*/

    public void InfoButton()
    {
        InfoPanel.SetActive(true);
    }

    public void SettingButton()
    {
        SettingPanel.SetActive(true);
    }

    public void CloseButton()
    {
        SettingPanel.SetActive(false);
        //ShopPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void BackButton()
    {
        LevelSelectPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void SelectLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
