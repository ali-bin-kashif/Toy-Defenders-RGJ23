using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            Health = 0;
            Destroy(gameObject);
        }
        
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
