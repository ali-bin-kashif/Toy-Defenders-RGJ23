using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioSource explode;
    Rigidbody Bullet;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        Bullet = GetComponent<Rigidbody>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(Damage);
            //make it collide with ground so the sound effect will work and area damage.
           // explode.Play();
        }
    }
}
