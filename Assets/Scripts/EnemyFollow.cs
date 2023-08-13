using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    GameObject player;
    public float stoppingDistance = 2.0f;

    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = stoppingDistance;
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            navAgent.SetDestination(player.transform.position);
        }
    }

    private void Update()
    {
        
    }
}
