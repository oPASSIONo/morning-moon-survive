using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimation : NetworkBehaviour
{
    [SerializeField] private Animator modelAnimator;
    
    private Combat playerCombat;
    private PlayerMovement playerMovement;
    private PlayerStateManager playerStateManager;
     
    private bool isModelRunning;
    private bool isModelUsingAction = false;
        
    private Coroutine coroutine;

    
    private void Start()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerCombat = GetComponent<Combat>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    #region Animation State Hashes

    private readonly int animHash_Idle = Animator.StringToHash("Idle");
    private readonly int animHash_Walk = Animator.StringToHash("Walk");
    private readonly int animHash_Attack = Animator.StringToHash("Attack");
    private readonly int animHash_Hit = Animator.StringToHash("Hit");
    private readonly int animHash_Dead = Animator.StringToHash("Dead");
    private readonly int animHash_Instant_Dead = Animator.StringToHash("InstantDead");
    private readonly int animHash_Pickup = Animator.StringToHash("Pickup");

    #endregion
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
     
        HandlePlayerMoveAnimation();
    }
    
    private void HandlePlayerMoveAnimation()
    {
        if (playerMovement.isPlayerMoving && !isModelUsingAction)
        {
            if (!isModelRunning && !isModelUsingAction)
            {
                isModelRunning = true;
                modelAnimator.CrossFade(animHash_Walk, 0.1f);
            }
        }
        else
        {
            if (isModelRunning && !isModelUsingAction)
            {
                isModelRunning = false;
                modelAnimator.CrossFade(animHash_Idle, 0.25f);
            }
        }
        
    }

    public void PlayerAttackAnim()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(StepPlayerAttackAnim());
    }
    
    private IEnumerator StepPlayerAttackAnim()
    {
        playerCombat.SetIsPerformingAction(true);
        isModelUsingAction = true;
        playerStateManager.SetState(PlayerStateManager.PlayerState.Attacking);
        playerCombat.SetAttackCollider(true);
        modelAnimator.CrossFade(animHash_Attack,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.3f);
        playerCombat.SetAttackCollider(false);
        modelAnimator.CrossFade(animHash_Idle,0.25f);
        playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
        isModelRunning = false;
        isModelUsingAction = false;
        playerCombat.SetIsPerformingAction(false);
    }

    public void PlayerPickupAnim()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
            
        coroutine = StartCoroutine(StepPlayerPickupAnim());
    }
    private IEnumerator StepPlayerPickupAnim()
    {
        isModelUsingAction = true;
        playerStateManager.SetState(PlayerStateManager.PlayerState.Pickup);
        modelAnimator.CrossFade(animHash_Pickup,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        modelAnimator.CrossFade(animHash_Idle,0.25f);
        playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
        isModelRunning = false;
        isModelUsingAction = false;
    }

    
    public void PlayerDeadAnim()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
            
        coroutine = StartCoroutine(StepPlayerDeadAnim());
    }
    
    private IEnumerator StepPlayerDeadAnim()
    {
        isModelUsingAction = true;
        modelAnimator.CrossFade(animHash_Instant_Dead,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        modelAnimator.CrossFade(animHash_Dead,0.2f);
    }

    public void PlayerHitAnim()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            playerCombat.SetIsPerformingAction(false);
        }
            
        coroutine = StartCoroutine(StepPlayerHitAnim());
    }
    private IEnumerator StepPlayerHitAnim()
    {
        isModelUsingAction = true;
        playerStateManager.SetState(PlayerStateManager.PlayerState.Hit);
        modelAnimator.CrossFade(animHash_Hit,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        modelAnimator.CrossFade(animHash_Idle,0.2f);
        playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
        isModelRunning = false;
        isModelUsingAction = false;
    }
    
    public void PlayerRespawnAnim()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            playerCombat.SetIsPerformingAction(false);
        }
            
        coroutine = StartCoroutine(StepPlayerRespawnAnim());
    }
    private IEnumerator StepPlayerRespawnAnim()
    {
        isModelUsingAction = true;
        playerStateManager.SetState(PlayerStateManager.PlayerState.Respawn);
        modelAnimator.CrossFade(animHash_Dead,0);
        yield return new WaitForSeconds(1f);
        modelAnimator.CrossFade(animHash_Idle,0.2f);
        yield return new WaitForSeconds(0.5f);
        playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
        isModelUsingAction = false;
        isModelRunning = false;
    }

    public void setAnimationSpeed(float changeSpeed)
    {
        modelAnimator.SetFloat("speed", changeSpeed);
    }
}
