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

    // Start is called before the first frame update
    void Start()
    {
        unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 0);
        Debug.Log("Start" + unlockedLevels);

        for(int i=0; i < levelButtons.Length; i++)
        {
            if(i <= unlockedLevels)
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
        levelButtons[unlockedLevels].GetComponent<Animator>().SetBool("isNextLevel", true);
    }


}
