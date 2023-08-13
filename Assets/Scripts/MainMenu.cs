using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public string playSceneName; 
    //public GameObject settingsPanel; 

    // Call this function when the Play button is clicked
    public void OnPlayButtonClick()
    {
        // Load the play scene
        SceneManager.LoadScene(playSceneName);
    }

    // Call this function when the Settings button is clicked
   /* public void OnSettingsButtonClick()
    {
        // Show or hide the settings panel based on its current active state
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    */

    
    public void OnExitButtonClick()
    {
        // Close the application (works for standalone builds)
        Application.Quit();

        // If you're using the Unity Editor, the Quit() method won't work, so you can use this instead:
         UnityEditor.EditorApplication.isPlaying = false;
    }
}

    

