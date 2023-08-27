using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    
    public float Health;
    public int damageReward, killReward;

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
        movement = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        enemySound = GetComponent<AudioSource>();
        healthBar.SetMaxHealth(Health);

        if(enemySpawn != null)
        {
            enemySound.PlayOneShot(enemySpawn);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !hasDied)
        {
           
            Health = 0;
            StartCoroutine(healthBar.ShowHealthBar());

            StartCoroutine(Death());

            _playerInventory.Coins += killReward;
            _playerInventory.gameUI.CoinsUpdate(killReward, '+');
            _playerInventory.CheckBuyCriteria();

            if (_enemyAnimator != null)
                _enemyAnimator.SetTrigger("Die");

        }
        
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        _playerInventory.Coins += damageReward;
        _playerInventory.gameUI.CoinsUpdate(damageReward, '+');
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
