using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : NetworkBehaviour
{
    [SerializeField] private Hunger Hunger;
    
    public Animator animator;
    public PlayerInput playerInput;

    private InputAction attackAction;
    private InputAction sheathAction;
    private bool attack;
    public bool draw = false;

    public StateMachine movementSM;
    
    [Header("Attack State")] 
    private float timePassed;
    private float clipLength;
    private float clipSpeed;
    
    [Header("State Control")] 
    public AttackState attacking;
    public StandingState standing;
    
    [Header("Animation Smooth")] 
    [Range(0,1)]
    public float speedDampTime = 0.1f;
    [Range(0,1)]
    public float velocityDampTime = 0.9f;
    
        
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
       if (IsOwner)
       {       
           animator = GetComponent<Animator>();
           playerInput = new PlayerInput();
           playerInput.PlayerControls.Enable();
           playerInput.PlayerControls.Move.performed += OnMovePerformed;
           playerInput.PlayerControls.Move.canceled += OnMoveCancelled;
           //playerInput.PlayerControls.DrawWeapon.performed += OnDrawWeaponPerformed;
           playerInput.PlayerControls.DrawWeapon.performed += ctx => OnDrawWeaponPerformed();
           playerInput.PlayerControls.DrawWeapon.Enable();
           playerInput.PlayerControls.Attack.performed += OnAttackPerformed;
           playerInput.PlayerControls.Attack.Enable();
           playerInput.PlayerControls.SheathWeapon.performed += OnSheathWeaponPerformed;
           playerInput.PlayerControls.SheathWeapon.Enable();

           sheathAction = playerInput.PlayerControls.SheathWeapon;
           attackAction = playerInput.PlayerControls.Attack;
           movementSM = new StateMachine();
           movementSM.Initialize(new StandingState(this, movementSM));
           
           SubscribeToDrawWeaponEvent();
       }
       
   }

   private void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.PlayerControls.Move.performed -= OnMovePerformed;
            playerInput.PlayerControls.Move.canceled -= OnMoveCancelled;
            //playerInput.PlayerControls.DrawWeapon.performed -= OnDrawWeaponPerformed;
            playerInput.PlayerControls.DrawWeapon.performed -= ctx => OnDrawWeaponPerformed();
            playerInput.PlayerControls.DrawWeapon.Disable(); 
            playerInput.PlayerControls.Attack.performed -= OnAttackPerformed;
            playerInput.PlayerControls.Attack.Disable();
            playerInput.PlayerControls.SheathWeapon.performed -= OnSheathWeaponPerformed;
            playerInput.PlayerControls.SheathWeapon.Disable();
            playerInput.PlayerControls.Disable();

            UnsubscribeFromDrawWeaponEvent();
        }
    }

   private void OnAttackPerformed(InputAction.CallbackContext context)
   {
       if (draw = true)
       {
           if (attackAction.triggered)
           {
               attack = true;
               movementSM.ChangeState(new AttackState(this, movementSM));
           }
       }
   }

   private void OnSheathWeaponPerformed(InputAction.CallbackContext context)
   {
       if (sheathAction.triggered)
       {
           animator.SetTrigger("sheathWeapon");
           movementSM.ChangeState(new StandingState(this, movementSM));
           Debug.Log("dddddddddddddddddddddddddddddddddddd");
       }
       
   }
   
   
   private void SubscribeToDrawWeaponEvent()
   {
       AgentTool.OnDrawWeapon += OnDrawWeaponPerformed;
   }

   private void UnsubscribeFromDrawWeaponEvent()
   {
       AgentTool.OnDrawWeapon -= OnDrawWeaponPerformed;
   }
   
   [ServerRpc(RequireOwnership = false)]
   private void UpdateAnimationStateServerRpc(float speed)
   {
       UpdateAnimationStateClientRpc(speed);
   }
   
   [ClientRpc]
   private void UpdateAnimationStateClientRpc(float speed)
   {
       animator.SetFloat("speed", speed);
   }

   private void OnMovePerformed(InputAction.CallbackContext context)
   {
       Vector2 movementInput = context.ReadValue<Vector2>();
       float speed = movementInput.magnitude;

       if (IsOwner)
       {
           animator.SetFloat("speed", speed);
           UpdateAnimationStateServerRpc(speed);
       }
   }
   private void OnMoveCancelled(InputAction.CallbackContext context)
   {
       if (IsOwner)
       {
           animator.SetFloat("speed", 0f);
           UpdateAnimationStateServerRpc(0f);
       }
   }
 
   private void OnDrawWeaponPerformed()
   {
       if (IsOwner)
       {
           animator.SetTrigger("drawWeapon");
           draw = true;
       }
   }
   
   
   private void Update()
   {
       if (!IsOwner)
           return;
       
       movementSM.currentState.LogicUpdate();
       
       Vector2 moveInput = playerInput.PlayerControls.Move.ReadValue<Vector2>();
       float speed = moveInput.magnitude;

       if (speed != animator.GetFloat("speed"))
       {
           animator.SetFloat("speed", speed);
           UpdateAnimationStateServerRpc(speed);
       }
   }
}