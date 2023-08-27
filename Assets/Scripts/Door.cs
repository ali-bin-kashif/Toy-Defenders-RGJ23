using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Rigidbody body;
    AudioSource doorSound;
    // Start is called before the first frame update
    void Start()
    {
        doorSound = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
        
    }

    void MakeDoorKinematic()
    {
        body.isKinematic = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            doorSound.Play();
            Invoke("MakeDoorKinematic", 8f);
        }
            
    }
}
