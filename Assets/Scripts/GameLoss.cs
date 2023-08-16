using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoss : MonoBehaviour
{
    EnemySpawner _waveSystem;
    Tower[] towers;
    EnemyScript[] enemies;

    public Animator kidAnimation;
    public GameObject DeathEffect;
    public Transform EffectPosition;

    bool gameEnded;
    // Start is called before the first frame update
    void Start()
    {
        _waveSystem = GameObject.FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(!gameEnded)
            {
                gameEnded = true;
                _waveSystem.wavesCompleted = true;
                _waveSystem.state = EnemySpawner.SpawnState.IDLE;

                towers = GameObject.FindObjectsOfType<Tower>();
                enemies = GameObject.FindObjectsOfType<EnemyScript>();

                foreach (Tower tower in towers)
                {
                    Destroy(tower.gameObject);
                }

                foreach (EnemyScript enemy in enemies)
                {
                    Destroy(enemy.gameObject);
                }

                Instantiate(DeathEffect, EffectPosition.position, Quaternion.identity);

                kidAnimation.SetTrigger("WakeUp");
            }
            
        }
    }
}
