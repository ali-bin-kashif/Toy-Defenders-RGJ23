using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_controll : MonoBehaviour
{
    public GameObject targetLight;
    public float delay = 5f;

    private bool isLightOn = false;

    private void Start()
    {
        // Start the coroutine when the script is initialized
        StartCoroutine(TurnOnLight());
    }

    private IEnumerator TurnOnLight()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Turn on the light after the delay
        targetLight.SetActive(true);
        isLightOn = true;
    }
}

