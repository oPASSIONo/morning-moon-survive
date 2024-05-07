using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : NetworkBehaviour
{
    [SerializeField] private Hunger Hunger;
    
    public Animator animator;
    public PlayerInput playerInput;

    [Header("Input Action")] 
    private InputAction attackAction;
    public InputAction sheathAction;
    public InputAction drawAction;
    
    private bool attack;
    public bool draw = false ;
    public bool sheath;

    public StateMachine movementSM;

    private AgentTool agentTool;
    
    [Header("Attack State")] 
    private float timePassed;
    private float clipLength;
    private float clipSpeed;
    
    [Header("State Control")] 
    public AttackState attacking;
    public StandingState standing;
    public CombatState combatting;
    
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
           agentTool = GetComponent<AgentTool>();
           animator = GetComponent<Animator>();
           playerInput = new PlayerInput();
           playerInput.PlayerControls.Enable();
           playerInput.PlayerControls.Move.performed += OnMovePerformed;
           playerInput.PlayerControls.Move.canceled += OnMoveCancelled;
           playerInput.PlayerControls.DrawWeapon.performed += ctx => OnDrawWeaponPerformed();
           playerInput.PlayerControls.DrawWeapon.Enable();
           sheathAction = playerInput.PlayerControls.SheathWeapon;
           attackAction = playerInput.PlayerControls.Attack;
           drawAction = playerInput.PlayerControls.DrawWeapon;
           
           movementSM = new StateMachine();
           standing = new StandingState(this, movementSM);
           combatting = new CombatState(this, movementSM, agentTool);
           attacking = new AttackState(this, movementSM);
           
           movementSM.Initialize(standing);
           
           
           SubscribeToDrawWeaponEvent();
       }
       
   }

   private void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.PlayerControls.Move.performed -= OnMovePerformed;
            playerInput.PlayerControls.Move.canceled -= OnMoveCancelled;
            playerInput.PlayerControls.DrawWeapon.performed -= ctx => OnDrawWeaponPerformed();
            playerInput.PlayerControls.DrawWeapon.Disable(); 
            playerInput.PlayerControls.Disable();

            UnsubscribeFromDrawWeaponEvent();
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
           movementSM.ChangeState(new CombatState(this, movementSM, agentTool));
       }
   }
   
   private void Update()
   {
       if (!IsOwner)
           return;
       movementSM.currentState.HandleInput();
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