using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody Bullet;
    public float Damage;
    public GameObject bulletHitEffect;
    // Start is called before the first frame update
    void Start()
    {
        Bullet = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(bulletHitEffect, transform.position, transform.rotation);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
