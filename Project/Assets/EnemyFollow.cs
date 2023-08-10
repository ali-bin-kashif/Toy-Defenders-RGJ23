using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public float stoppingDistance = 2.0f;

    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = stoppingDistance;

        if (player != null)
        {
            navAgent.SetDestination(player.transform.position);
        }
    }

    private void Update()
    {
        
    }
}
