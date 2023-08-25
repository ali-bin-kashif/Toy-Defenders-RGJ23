using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    
    public float Health;

    public Animator _enemyAnimator;
    Inventory _playerInventory;
    NavMeshAgent movement;
    Rigidbody rb;

    public HealthBar healthBar;

    bool isHealthShown;
    
    public bool hasDied;

    AudioSource enemySound;

    public GameObject deathEffect;

    public AudioClip enemySpawn, enemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindObjectOfType<Inventory>();
        //_enemyAnimator = GetComponent<Animator>();
        movement = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemySound = GetComponent<AudioSource>();
        healthBar.SetMaxHealth(Health);
        enemySound.PlayOneShot(enemySpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !hasDied)
        {
           
            Health = 0;
            StartCoroutine(healthBar.ShowHealthBar());

            StartCoroutine(Death());

            _playerInventory.Coins += 40;
            _playerInventory.gameUI.CoinsUpdate(40, '+');
            _playerInventory.CheckBuyCriteria();

            if (_enemyAnimator != null)
                _enemyAnimator.SetTrigger("Die");

        }
        
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        _playerInventory.Coins += 5;
        _playerInventory.gameUI.CoinsUpdate(5, '+');
        if (!isHealthShown)
        {
            isHealthShown = true;
            StartCoroutine(healthBar.ShowHealthBar());
            isHealthShown = false;
        }
        _playerInventory.CheckBuyCriteria();
    }

    IEnumerator Death()
    {
        hasDied = true;
        enemySound.PlayOneShot(enemyDeath);
        movement.enabled = false;
        yield return new WaitForSeconds(1f);
        Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        Destroy(gameObject);
    }

   

}
