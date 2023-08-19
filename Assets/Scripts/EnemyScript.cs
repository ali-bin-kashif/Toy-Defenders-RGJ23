using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float Health;

    Animator _enemyAnimator;
    Inventory _playerInventory;
    NavMeshAgent movement;
    Rigidbody rb;

    public HealthBar healthBar;

    bool isHealthShown;
    
    bool hasDied;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindObjectOfType<Inventory>();
        _enemyAnimator = GetComponent<Animator>();
        movement = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        healthBar.SetMaxHealth(Health);

    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !hasDied)
        {
            Health = 0;
            StartCoroutine(healthBar.ShowHealthBar());
            hasDied = true;
            movement.enabled = false;
            _playerInventory.Coins += 40;
            _playerInventory.CheckBuyCriteria();
            if (_enemyAnimator != null)
                _enemyAnimator.SetTrigger("Die");

            Destroy(gameObject,1f);

        }
        
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        _playerInventory.Coins += 5;
        if(!isHealthShown)
        {
            isHealthShown = true;
            StartCoroutine(healthBar.ShowHealthBar());
            isHealthShown = false;
        }
        _playerInventory.CheckBuyCriteria();
    }

   

}
