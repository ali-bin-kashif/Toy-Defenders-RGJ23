using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float stoppingDistance = 2.0f;

    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        if (player != null)
        {
            navAgent.SetDestination(player.position);
        }
    }
}
