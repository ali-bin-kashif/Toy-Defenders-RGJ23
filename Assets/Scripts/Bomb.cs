using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioSource bombSound;
    public float damageRadius;
    public float Damage;

    public GameObject explosionEffect;

    EnemyScript[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        bombSound.Play();
        GetComponent<MeshRenderer>().enabled = false;
        enemies = GameObject.FindObjectsOfType<EnemyScript>();

        foreach(EnemyScript enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance <= damageRadius)
            {
                enemy.TakeDamage(Damage);
            }
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject,2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }
}
