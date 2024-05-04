using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : NetworkBehaviour
{
    [SerializeField] private Hunger Hunger;
    
    private Animator animator;
    private PlayerInput playerInput;

    
   /*private void Start()
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
    }*/

   private void Start()
   {
       animator = GetComponent<Animator>();

       if (IsOwner)
       {
           playerInput = new PlayerInput();
           playerInput.PlayerControls.Enable();
           playerInput.PlayerControls.Move.performed += OnMovePerformed;
           playerInput.PlayerControls.Move.canceled += OnMoveCancelled;
           playerInput.PlayerControls.DrawWeapon.performed += OnDrawWeaponPerformed;
           playerInput.PlayerControls.DrawWeapon.Enable(); 

       }
       
   }

   private void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.PlayerControls.Move.performed -= OnMovePerformed;
            playerInput.PlayerControls.Move.canceled -= OnMoveCancelled;
            playerInput.PlayerControls.DrawWeapon.performed -= OnDrawWeaponPerformed;
            playerInput.PlayerControls.DrawWeapon.Disable(); 
            playerInput.PlayerControls.Disable();
        }
    }
   
   [ServerRpc(RequireOwnership = false)]
   private void UpdateAnimationStateServerRpc(float speed)
   {
       UpdateAnimationStateClientRpc(speed);
   }
   
   [ClientRpc]
   private void UpdateAnimationStateClientRpc(float speed)
   {
       animator.SetFloat("Speed", speed);
   }

   private void OnMovePerformed(InputAction.CallbackContext context)
   {
       Vector2 movementInput = context.ReadValue<Vector2>();
       float speed = movementInput.magnitude;

       if (IsOwner)
       {
           animator.SetFloat("Speed", speed);
           UpdateAnimationStateServerRpc(speed);
       }
   }
   private void OnMoveCancelled(InputAction.CallbackContext context)
   {
       if (IsOwner)
       {
           animator.SetFloat("Speed", 0f);
           UpdateAnimationStateServerRpc(0f);
       }
   }
   
   private void OnDrawWeaponPerformed(InputAction.CallbackContext context)
   {
       if (IsOwner)
       {
           animator.SetTrigger("drawWeapon");
           // You can add additional logic here if needed
       }
   }
    
   private void Update()
   {
       if (!IsOwner)
           return;

       Vector2 moveInput = playerInput.PlayerControls.Move.ReadValue<Vector2>();
       float speed = moveInput.magnitude;

       if (speed != animator.GetFloat("Speed"))
       {
           animator.SetFloat("Speed", speed);
           UpdateAnimationStateServerRpc(speed);
       }
   }
}