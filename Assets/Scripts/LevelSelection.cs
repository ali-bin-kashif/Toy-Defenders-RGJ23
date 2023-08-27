using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
{

    public Button[] levelButtons;
    [SerializeField]
    int unlockedLevels;

    private void Awake()
    {
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log("Start" + unlockedLevels);

        for(int i=0; i < levelButtons.Length; i++)
        {
            //levelButtons[i].transform.GetChild(0).gameObject.SetActive(false);
            if (i <= unlockedLevels - 1)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
            
        }

        

        

    }

    private void OnEnable()
    {
        levelButtons[unlockedLevels - 1].GetComponent<Animator>().SetBool("isNextLevel", true);

        for (int i = 0; i < unlockedLevels-1; i++)
        {
            levelButtons[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }


}
