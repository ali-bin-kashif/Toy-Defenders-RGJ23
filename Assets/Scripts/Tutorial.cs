using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialHand, tutorialHand2;
    public GameObject coinHUD, pauseBtn, progressBar;
    public EnemySpawner waveSpawner;
    public GameObject firstStep, secondStep;

    public Transform Platform;

    //int as bool for player prefs to save wether tutorial has done or not (1 -True , 0 - False)
    public int tutorialDone;

    public Camera mainCam; 

    private void Awake()
    {
        
        tutorialDone = PlayerPrefs.GetInt("TutorialComplete", 0);

        if(tutorialDone == 1)
        {
            tutorialHand.SetActive(false);
            firstStep.SetActive(false);
            secondStep.SetActive(false);
            waveSpawner.enabled = true;
            coinHUD.SetActive(true);
            pauseBtn.SetActive(true);
            progressBar.SetActive(true);
        }
        else
        {
            tutorialHand.SetActive(false);
            firstStep.SetActive(false);
            secondStep.SetActive(false);
            waveSpawner.enabled = false;
            coinHUD.SetActive(false);
            pauseBtn.SetActive(false);
            progressBar.SetActive(false);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(tutorialDone == 0)
        {
            FirstStep();
            
        }
        else
        {
            gameObject.SetActive(false);
            waveSpawner.enabled = true;
        }
        
    }


    void FirstStep()
    {
        tutorialHand.SetActive(true);
        firstStep.SetActive(true);
        //yield return new WaitForSeconds(2.5f);
        
    }

    public void FirstStepComplete()
    {
        tutorialHand.SetActive(false);
        firstStep.SetActive(false);
        secondStep.SetActive(true);
        SecondStep();
    }

    void SecondStep()
    {
        Vector3 platformPosition = mainCam.WorldToScreenPoint(Platform.position);
        tutorialHand2.transform.position = new Vector3(platformPosition.x + 150f, platformPosition.y - 150f);
        tutorialHand2.SetActive(true);
    }

    public void SecondStepComplete()
    {
        PlayerPrefs.SetInt("TutorialComplete", 1);
        gameObject.SetActive(false);
        waveSpawner.enabled = true;
        coinHUD.SetActive(true);
        pauseBtn.SetActive(true);
        progressBar.SetActive(true);

    }
}
