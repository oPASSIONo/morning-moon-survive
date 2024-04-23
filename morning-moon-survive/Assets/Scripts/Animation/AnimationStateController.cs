using Unity.Netcode;
using UnityEngine;

public class AnimationStateController : NetworkBehaviour
{
    [SerializeField] private Hunger hunger;
    private Animator animator;

    private AgentTool agenTool;

    private int isWalkingHash;
    private int isTriedHash;
    private int isIdleWeapon;
    
    private void Start()
    {
        animator = GetComponent<Animator>();

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
                animator.SetBool(isTriedHash, false);
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