using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject targetLight;
    public float delayInSeconds = 5f;

    private bool isLightOn = false;

    private void Start()
    {
        // Start the coroutine when the script is initialized
        StartCoroutine(TurnOnLightAfterDelay());
    }

    private IEnumerator TurnOnLightAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Turn on the light after the delay
        targetLight.SetActive(true);
        isLightOn = true;
    }
}

