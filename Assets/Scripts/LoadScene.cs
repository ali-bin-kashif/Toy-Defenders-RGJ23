using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string scenename;
    // Call this function when the button is clicked
    public void LoadNextScene()
    {
      
    
        SceneManager.LoadScene(scenename);
    }
}

