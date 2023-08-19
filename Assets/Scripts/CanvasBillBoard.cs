using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBillBoard : MonoBehaviour
{
    Transform mainCamera;

    private void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>().transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
