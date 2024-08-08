using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation Instance { get; private set; }
    [SerializeField] private Animator modelAnimator;
    
    private Combat playerCombat;
    private PlayerMovement playerMovement;
    private bool isModelRunning;
    private bool isModelUsingAction = false;

    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
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
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Attacking);
        playerCombat.SetAttackCollider(true);
        modelAnimator.CrossFade(animHash_Attack,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.3f);
        playerCombat.SetAttackCollider(false);
        modelAnimator.CrossFade(animHash_Idle,0.25f);
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Normal);
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
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Pickup);
        modelAnimator.CrossFade(animHash_Pickup,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        modelAnimator.CrossFade(animHash_Idle,0.25f);
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Normal);
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
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Hit);
        modelAnimator.CrossFade(animHash_Hit,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length + 0.5f);
        modelAnimator.CrossFade(animHash_Idle,0.2f);
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Normal);
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
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Respawn);
        modelAnimator.CrossFade(animHash_Dead,0);
        yield return new WaitForSeconds(1f);
        modelAnimator.CrossFade(animHash_Idle,0.2f);
        yield return new WaitForSeconds(0.5f);
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Normal);
        isModelUsingAction = false;
        isModelRunning = false;
    }
}
