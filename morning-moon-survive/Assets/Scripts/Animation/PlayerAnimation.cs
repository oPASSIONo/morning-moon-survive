using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator modelAnimator;
    [SerializeField] private Combat playerCombat;
    private bool isModelRunning;
    private bool isModelUsingAction = false;
    private bool isPlayerMoving;
    
    private Coroutine coroutine;

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
        if (isPlayerMoving && !isModelUsingAction)
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
        isModelUsingAction = true;
        playerCombat.attackCollider.enabled = true;
        modelAnimator.CrossFade(animHash_Attack,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        playerCombat.attackCollider.enabled = false;
        modelAnimator.CrossFade(animHash_Idle,0);
        isModelUsingAction = false;
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
        modelAnimator.CrossFade(animHash_Pickup,0);
        AnimatorStateInfo stateInfo = modelAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        modelAnimator.CrossFade(animHash_Idle,0);
        isModelUsingAction = false;
    }
    
    
}
