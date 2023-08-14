using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Health;
    Animator _enemyAnimator;
    Inventory _playerInventory;

    bool hasDied;
    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindObjectOfType<Inventory>();
        _enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0 && !hasDied)
        {
            Health = 0;
            hasDied = true;
            _playerInventory.Coins += 20;
            if(_enemyAnimator != null)
                _enemyAnimator.SetTrigger("Die");

            Destroy(gameObject,3f);

        }
        
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        _playerInventory.Coins += 5;
    }
}
