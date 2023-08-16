using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float Health;
    Animator _enemyAnimator;
    Inventory _playerInventory;
    NavMeshAgent movement;
    Rigidbody rb;

    bool hasDied;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindObjectOfType<Inventory>();
        _enemyAnimator = GetComponent<Animator>();
        movement = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !hasDied)
        {
            Health = 0;
            hasDied = true;
            movement.enabled = false;
            _playerInventory.Coins += 20;
            if(_enemyAnimator != null)
                _enemyAnimator.SetTrigger("Die");

            Destroy(gameObject,1f);

        }
        
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        _playerInventory.Coins += 5;
    }

}
