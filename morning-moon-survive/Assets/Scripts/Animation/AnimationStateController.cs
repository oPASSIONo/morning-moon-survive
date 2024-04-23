using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : NetworkBehaviour
{
    [SerializeField] private Hunger Hunger;
    
    private Animator animator;
    private PlayerInput playerInput;

    private int isWalkingHash;
    private int isTriedHash;

   private void Start()
   {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isTriedHash = Animator.StringToHash("isTried");
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool rightPressed = Input.GetKey(KeyCode.D);

        UpdateWalkingState(Hunger.CurrentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);

        UpdateWalkingStateServerRpc(Hunger.CurrentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);

    }

    private void UpdateWalkingState(int currentHunger, bool forwardPressed, bool leftPressed, bool backwardPressed, bool rightPressed)
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isTried = animator.GetBool(isTriedHash);

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
                animator.SetBool(isTriedHash, false);
            }

            if (isWalking && !anyMovementKeyPressed)
            {
                animator.SetBool(isWalkingHash, false);
            }
        }
    }

    [ServerRpc]
    private void UpdateWalkingStateServerRpc(int currentHunger, bool forwardPressed, bool leftPressed, bool backwardPressed, bool rightPressed)
    {
        UpdateWalkingStateClientRpc(currentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);
    }

    [ClientRpc]
    private void UpdateWalkingStateClientRpc(int currentHunger, bool forwardPressed, bool leftPressed, bool backwardPressed, bool rightPressed)
    {
        UpdateWalkingState(currentHunger, forwardPressed, leftPressed, backwardPressed, rightPressed);
    }

}