using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState // Define enemy states
    {
        Roam,
        Chase,
        Fight,
        Attacking
    }
    
    public NavMeshAgent agent;
    [SerializeField] private float roamRadius; // Radius for enemy roaming
    [SerializeField] private float chaseRadius; // Radius for chasing player
    [SerializeField] private float attackRadius; // Radius for attacking player
    [SerializeField] private float moveSpeed; // Enemy movement speed
    [SerializeField] public Animation anim;
    [SerializeField] public Animator animation;
    [SerializeField] private bool isFriendly;
    
    public float attackDelayTime { get; private set; } = 3f;

    private Vector3 startPosition; // Enemy's starting position
    private float maxChaseDistance = 30;
    private GameObject player;
    private EnemyState currentState;
    private bool isWalking;
    private float attackTimer = 0;
    private float animationSpeed = 0.5f;
    private bool hasPlayedIdle = false;
    protected bool isAttack = false;
    private Coroutine attackcoroutine;
    
    #region Animation State Hashes

    private readonly int animHash_Idle = Animator.StringToHash("Idle");
    private readonly int animHash_Walk = Animator.StringToHash("Walk");
    private readonly int animHash_Attack1 = Animator.StringToHash("Attack1");
    private readonly int animHash_Attack2 = Animator.StringToHash("Attack2");
    private readonly int animHash_Attack3 = Animator.StringToHash("Attack3");
    private readonly int animHash_Dead = Animator.StringToHash("Dead");
  

    #endregion

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        agent.speed = moveSpeed; // Set agent speed
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.Roam;
    }

    void Update()
    {
        
        if (player != null)
        {
            switch (currentState)
            {
                case EnemyState.Roam:
                    Roam();
                    break;
                case EnemyState.Chase:
                    ChasePlayer();
                    break;
                case EnemyState.Fight:
                    FightPlayer();
                    break;
               /* case  EnemyState.Attacking:
                    Attacking();
                    break;*/

            }

            if (currentState == EnemyState.Roam || currentState == EnemyState.Chase)
            {
                IdleWalkController();
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        UnityEngine.Debug.Log(currentState);
        UnityEngine.Debug.Log(isAttack);
    }

    void Roam()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Generate random movement within radius
            Vector3 randomOffset = Random.insideUnitSphere * roamRadius;
            Vector3 targetPosition = startPosition + randomOffset;
            // Move towards target position
            agent.SetDestination(targetPosition);
        }

        // Check if player is within chase radius
        if (IsPlayerInRange() && !isFriendly)
        {
            currentState = EnemyState.Chase;
        }

    }

    void ChasePlayer()
    {
        if (player != null)
        {
            // Check if player is still within chase radius and enemy is within max distance
            if (IsPlayerInRange(chaseRadius) && IsInChaseDistance(startPosition))
            {
                // Move towards player
                agent.SetDestination(player.transform.position);
            }
            // Check if player is outside chase radius (stop chasing)
            else //(IsPlayerInRange(chaseRadius) == false)
            {
                currentState = EnemyState.Roam;
                agent.SetDestination(transform.position);
            }
            
            if (IsPlayerInRange(attackRadius))
            {
                currentState = EnemyState.Fight;
                attackTimer = 0;
            }

            if (isAttack)
            {
                currentState = EnemyState.Fight;
            }

        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Debug.LogError("Player not found with tag 'Player'");
            
        }
    }
    
    void FightPlayer()
    {
        // Stop movement
        agent.isStopped = true;
        attackTimer += Time.deltaTime;

        Vector3 targetPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        transform.LookAt(targetPosition);
        
        // Play idle animation only once on entering FightState
        if (!hasPlayedIdle)
        {
            isWalking = false;
            anim.CrossFade("Idle");
            hasPlayedIdle = true;
        }

        if (!isAttack && IsPlayerInRange(attackRadius))
        {
          //  currentState = EnemyState.Attacking;
          Attack(attackDelayTime);
        }
        
        
        // Check if player is outside attack radius (optional)
        if (IsPlayerInRange(attackRadius) == false && !isAttack)
        {
            currentState = EnemyState.Chase;
            agent.isStopped = false; // Resume movement
            hasPlayedIdle = false;
            isWalking = true;

        }
    }

    void Attacking()
    {
        
    }
    
    bool IsPlayerInRange()
    {
        return IsPlayerInRange(chaseRadius);
    }
    
    bool IsPlayerInRange(float radius)
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= radius;
        }

        return false;
    }

    bool IsInChaseDistance(Vector3 referencePosition)
    {
        float distance = Vector3.Distance(transform.position, referencePosition);
        return distance <= maxChaseDistance;
    }

    void RandomAttackAnim(float attackCD)
    {
       // yield return new WaitForSeconds(attackCD); // Initial delay before first attack
        
        /*while (true)
        {
            if (Time.time - lastAttackTime >= attackDelayTime)
            {
                lastAttackTime = Time.time;
                int randomAttack = Random.Range(0, 3);
                switch (randomAttack)
                {
                    case 0:
                        StartCoroutine(AttackMove1());
                        break;
                    case 1:
                        StartCoroutine(AttackMove2());
                        break;
                    case 2: 
                        StartCoroutine(AttackMove3());
                        break;
                }
            }
            yield return null;
        }*/
        if(attackTimer >= attackCD)
        {
            int randomAttack = Random.Range(0, 3);
            switch (randomAttack)
            {
                case 0: 
                    StartCoroutine(AttackMove1());
                    break;
                case 1:
                    StartCoroutine(AttackMove2());
                    break;
                case 2:
                    StartCoroutine(AttackMove3());
                    break;
            }

            attackTimer = 0;
        }
    }

    void Attack(float attackCD)
    {
        if (!isAttack)
        {
            RandomAttackAnim(attackCD);
        }

    }
    void IdleWalkController()
    {
        isWalking = agent.velocity.magnitude > 0.1f;
        // Update animation based on movement state
        if (isWalking && !anim.IsPlaying("Walk"))
        {
            anim.Play("Walk");
        }
        else if (!isWalking && !anim.IsPlaying("Idle"))
        {
            anim.CrossFade("Idle");
        }
    }

    protected virtual IEnumerator AttackMove1()
    { 
        yield return new WaitForSeconds(anim["Attack1"].length);
    }
    
    protected virtual IEnumerator AttackMove2()
    {
        StartCoroutine(AttackMove1());
        yield return null;
    }
    
    protected virtual IEnumerator AttackMove3()
    {
        StartCoroutine(AttackMove1());
        yield return null;
    }
    
    private void OnDrawGizmosSelected()
    {
        // Draw roaming radius (blue)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(startPosition, roamRadius);
        
        // Draw chase radius (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        
        // Draw attack radius (green)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        
        // Draw max chase range (yellow)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startPosition, maxChaseDistance);
    }
    
}
