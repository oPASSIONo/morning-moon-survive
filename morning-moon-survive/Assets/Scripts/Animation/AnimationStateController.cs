using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AnimationStateController : NetworkBehaviour
{
    public static AnimationStateController Instance { get; private set; }

    [SerializeField] private Hunger Hunger;
    [SerializeField] private Animator animator;

    private AgentTool agenTool;

    private int isWalkingHash;
    private int isTriedHash;
<<<<<<< HEAD
    private int isIdleWeapon;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
=======

    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator reference is not set.");
            return;
        }
>>>>>>> parent of e20685a (Fix animation but client cant pick up item)

        isWalkingHash = Animator.StringToHash("isWalking");
        isTriedHash = Animator.StringToHash("isTried");
        isTriedHash = Animator.StringToHash("idleWeapon");

    }

    private void Update()
    {
        if (!IsOwner) 
            return;

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool rightPressed = Input.GetKey(KeyCode.D);

<<<<<<< HEAD
        UpdateWalkingState(hunger.CurrentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);
        
        UpdateWalkingStateServerRpc(hunger.CurrentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);
        
        OnDrawWeapon();

    }

    private void OnDrawWeapon()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("drawWeapon");
        }
    }
    
=======
        UpdateWalkingState(Hunger.CurrentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);
    }

>>>>>>> parent of e20685a (Fix animation but client cant pick up item)
    private void UpdateWalkingState(int currentHunger, bool forwardPressed, bool leftPressed, bool backwardPressed, bool rightPressed)
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isTried = animator.GetBool(isTriedHash);
        bool idleWeapon = animator.GetBool(isIdleWeapon);

        bool anyMovementKeyPressed = forwardPressed || leftPressed || backwardPressed || rightPressed;

        if (currentHunger >= 75)
        {
            if (!isTried && anyMovementKeyPressed)
            {
                animator.SetBool(isTriedHash, true);
                animator.SetBool(isWalkingHash, false);
            }

            if (isTried && !anyMovementKeyPressed)
            {
                animator.SetBool(isTriedHash, false);
            }
        }
        else
        {
            if (!isWalking && anyMovementKeyPressed)
            {
               
                animator.SetBool(isWalkingHash, true);
            }

            if (isWalking && !anyMovementKeyPressed)
            {
                if (idleWeapon)
                {
                    animator.SetBool(isIdleWeapon, true);
                }
                else
                {
                    animator.SetBool(isWalkingHash, false);

                }
            }
        }
    }  
}
