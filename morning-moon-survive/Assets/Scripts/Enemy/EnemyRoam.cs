using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRoam : MonoBehaviour
{
    public NavMeshAgent agent;
    public float roamRadius; // Radius for enemy roaming
    public float chaseRadius; // Radius for chasing player
    public float moveSpeed; // Enemy movement speed
    public Transform playerTransform; // Reference to player transform

    private Vector3 startPosition; // Enemy's starting position
    private bool isChasing = false; // Flag to check if chasing player

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        agent.speed = moveSpeed; // Set agent speed
    }

    void Update()
    {
        if (!isChasing)
        {
            Roam();
        }
        else
        {
            ChasePlayer();
        }
    }

    void Roam()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Generate random movement within radius
            Vector3 randomOffset = Random.insideUnitSphere * roamRadius;
            Vector3 targetPosition = startPosition + randomOffset;

            // Check if path is clear using Raycast (optional)
            /*
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetPosition - transform.position, out hit, roamRadius))
            {
                if (hit.collider.isTrigger)
                {
                    Roam();
                    return;
                }
            }
            */

            // Move towards target position
            agent.SetDestination(targetPosition);
        }

        // Check if player is within chase radius
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRadius)
        {
            isChasing = true;
        }
        

    }

    void ChasePlayer()
    {
        // Move towards player
        agent.SetDestination(playerTransform.position);

        // Check if player is outside chase radius (stop chasing)
        if (Vector3.Distance(transform.position, playerTransform.position) > chaseRadius)
        {
            isChasing = false;
            agent.SetDestination(transform.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw roaming radius (blue)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, roamRadius);
        
        // Draw chase radius (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
