using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_controll : MonoBehaviour
{
    public GameObject targetLight;
    public AudioClip doorsound;
    public AudioSource sound;
    public float delay = 5f;
    public float newdelay = 2f;

    private bool isLightOn = false;

    private void Start()
    {
        // Start the coroutine when the script is initialized
        StartCoroutine(TurnOnLight());
        StartCoroutine(DoorSound());
    }

    private IEnumerator TurnOnLight()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Turn on the light after the delay
        targetLight.SetActive(true);

        isLightOn = true;
    }



    private IEnumerator DoorSound()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(newdelay);

        if (sound != null )
        {
            sound.Play();
        }
        else
        {
            Debug.LogError("AudioSource or Audio clip is missing!");
        }
    }

}

