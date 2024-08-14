using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState // Define enemy states
    {
        Roam,
        Chase,
        Fight,
        Attacking,
        Normal
    }
    
    public NavMeshAgent agent;
    [SerializeField] private float roamRadius; // Radius for enemy roaming
    [SerializeField] private float chaseRadius; // Radius for chasing player
    [SerializeField] private float attackRadius; // Radius for attacking player
    [SerializeField] protected float moveSpeed; // Enemy movement speed
    //[SerializeField] public Animation anim;
    [SerializeField] public Animator enemyAnimation;
    [SerializeField] private bool isFriendly;
    
    public float attackDelayTime { get; private set; } = 2f;

    private Vector3 startPosition; // Enemy's starting position
    private float maxChaseDistance = 30;
    protected GameObject player;
    private EnemyState currentState;
    //private bool isWalking;
    private float attackTimer = 0;
    private float animationSpeed = 0.5f;
    private bool hasPlayedIdle = false;
    protected bool isAttack = false;
    private bool isEnemyMoving = false;
    protected bool isModelUsingAction = false;
    protected bool isModelRunning = false;
    protected bool lookAtPlayer = false;
    protected EnemyCombat enemyCombat;
    private Coroutine coroutine;
    
    #region Animation State Hashes

    protected readonly int animHash_Idle = Animator.StringToHash("Idle");
    protected readonly int animHash_Walk = Animator.StringToHash("Walk");
    protected readonly int animHash_Attack1 = Animator.StringToHash("Attack1");
    protected readonly int animHash_Attack2 = Animator.StringToHash("Attack2");
    protected readonly int animHash_Attack3 = Animator.StringToHash("Attack3");
    protected readonly int animHash_Dead = Animator.StringToHash("Dead");
  

    #endregion

    void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        agent.speed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.Roam;
        
        SetState();
    }

    private void Update()
    {
        HandleEnemyMoveAnimation();
        if (lookAtPlayer)
        {
            LookAtPlayer();
        }
    }

    void SetState()
    {
        
        if (player != null)
        {
            switch (currentState)
            {
                case EnemyState.Roam:
                    StartCoroutine(Roam());
                    break;
                case EnemyState.Chase:
                    StartCoroutine(ChasePlayer());
                    break;
                case EnemyState.Fight:
                    StartCoroutine(FightPlayer());
                    break;
                case  EnemyState.Attacking:
                    StartCoroutine(Attacking());
                    break;

            }
            
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        /*UnityEngine.Debug.Log(currentState);
        UnityEngine.Debug.Log(isAttack);*/
    }

    private IEnumerator Roam()
    {
        while (currentState == EnemyState.Roam )
        {
            lookAtPlayer = false;
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
            yield return null;
        }
        SetState();
    }

    private IEnumerator ChasePlayer()
    {
        while (currentState == EnemyState.Chase)
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
                else
                {
                    currentState = EnemyState.Roam;
                    agent.SetDestination(transform.position);
                    StartCoroutine(DisableChase());
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

            yield return null;
        }
        SetState();
    }
    
    private IEnumerator FightPlayer()
    {
        attackTimer = 0;
        agent.isStopped = true;
        while (currentState == EnemyState.Fight)
        {
            // Stop movement
            attackTimer += Time.deltaTime;

            lookAtPlayer = true;
        
            // Play idle animation only once on entering FightState
            if (!hasPlayedIdle)
            {
                //anim.CrossFade("Idle");
                enemyAnimation.CrossFade(animHash_Idle,1f);
                hasPlayedIdle = true;
            }

            if (!isAttack && IsPlayerInRange(attackRadius) && attackTimer >= attackDelayTime)
            {
                currentState = EnemyState.Attacking;
            }
        
        
            // Check if player is outside attack radius (optional)
            if (IsPlayerInRange(attackRadius) == false && !isAttack)
            {
                currentState = EnemyState.Chase;
                hasPlayedIdle = false;

            }

            yield return null;
        }
        agent.isStopped = false; // Resume movement
        lookAtPlayer = false;
        SetState();
    }

    private IEnumerator Attacking()
    {
        lookAtPlayer = true;
        Attack();
            while (isAttack)
            {
                yield return null;
            }

            //StartCoroutine(SoftenLookAt());

            currentState = EnemyState.Fight;
            SetState();
    }
    
    protected bool IsPlayerInRange()
    {
        return IsPlayerInRange(chaseRadius);
    }
    
    protected bool IsPlayerInRange(float radius)
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

    void RandomAttackAnim()
    {
        isAttack = true;
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

    void Attack()
    {
        if (!isAttack)
        {
            RandomAttackAnim();
        }
        
    }

    private void HandleEnemyMoveAnimation()
    {
        isEnemyMoving = agent.velocity.magnitude > 0.1f;
        if (isEnemyMoving && !isModelUsingAction)
        {
            if (!isModelRunning && !isModelUsingAction)
            {
                isModelRunning = true;
                enemyAnimation.CrossFade(animHash_Walk, 0.1f);
            }
        }
        else
        {
            if (isModelRunning && !isModelUsingAction)
            {
                isModelRunning = false;
                enemyAnimation.CrossFade(animHash_Idle, 0.25f);
            }
        }
        
    }

    private void LookAtPlayer()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);
    }
    
    private IEnumerator SoftenLookAt()
    {
        float elapsedTime = 0f;
        float lerpDuration = 0.5f; // Adjust lerp duration as needed

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;
            Vector3 targetDirection = player.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);  

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Re-enable full LookAt behavior
        lookAtPlayer = true;
    }

    private IEnumerator DisableChase()
    {
        float tempChaseRadius = chaseRadius;
        chaseRadius = 0;
        yield return new WaitForSeconds(3);
        chaseRadius = tempChaseRadius;
    }

    protected virtual IEnumerator AttackMove1()
    { 
        isAttack = true;
        enemyAnimation.CrossFade(animHash_Attack1,0);
        AnimatorStateInfo stateInfo = enemyAnimation.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        enemyAnimation.CrossFade(animHash_Idle,0);
        isAttack = false;
        enemyCombat.SetMoveset(1);
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
