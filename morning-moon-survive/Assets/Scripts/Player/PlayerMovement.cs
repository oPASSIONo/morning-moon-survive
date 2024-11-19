using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player movement and speed, including dashing.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkBehaviour
{
    public float MaxSpeed { get; private set; }
    public float MinSpeed { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float BaseSpeed { get; private set; }
    private float dashForce = 10f;
    private float dashDuration = 0.5f;
    private bool isDashing;
    private float dashTimeRemaining;
    public bool isPlayerMoving { get; private set; }
    private Rigidbody rb;

    public event Action<float, float> OnSpeedChanged;

    private Satiety satietyComponent;
    private Stamina staminaComponent;
    
    private PlayerAnimation playerAnimation;
    private PlayerStateManager playerStateManager;

    private void Awake()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;;

        staminaComponent = GetComponent<Stamina>();
        satietyComponent = GetComponent<Satiety>();
        if (satietyComponent != null)
        {
            satietyComponent.OnSatietyChanged += UpdateSpeed;
            UpdateSpeed(satietyComponent.CurrentSatiety, satietyComponent.MaxSatiety);
        }
        else
        {
            Debug.LogWarning("Satiety component not found.");
        }

        GameInput.Instance.OnDashAction += HandleDash;
    }

    private void OnClientConnected(ulong obj)
    {
        if (NetworkManager.Singleton.LocalClientId == obj)
        {
            TryAssignLocalPlayer();
        }
    }

    private void TryAssignLocalPlayer()
    {
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                playerStateManager = networkObject.GetComponent<PlayerStateManager>();
                playerAnimation = networkObject.GetComponent<PlayerAnimation>();
                
                break;
            }
        }
        
        if (playerStateManager != null)
        {
            // Perform actions with the inventoryController (e.g., update UI, listen to events)
            Debug.Log("Local player's PlayerMovement found.");
        }
        else
        {
            Debug.Log("Local player's PlayerMovement not found.");
        }
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (isDashing)
        {
            switch (playerStateManager.currentState)
            {
                case PlayerStateManager.PlayerState.Normal:
                    playerAnimation.setAnimationSpeed(2f);
                    playerStateManager.SetState(PlayerStateManager.PlayerState.Dash);
                    break;
            }
            dashTimeRemaining -= Time.deltaTime;
            if (dashTimeRemaining <= 0)
            {
                isDashing = false;
                rb.velocity = Vector3.zero; // Stop the dash
                playerAnimation.setAnimationSpeed(1f);
                switch (playerStateManager.currentState)
                {
                    case PlayerStateManager.PlayerState.Dash:
                        playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
                        break;
                }
            }
        }
        else
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        if (playerStateManager.currentState == PlayerStateManager.PlayerState.Normal)
        {
            Vector2 inputVector = GameInput.Instance.GetMovement();
            Transform cameraTransform = Camera.main.transform;

            Vector3 movement = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
            movement.y = 0f;

            Vector3 moveDirection = movement.normalized * CurrentSpeed * Time.deltaTime;
            transform.position += moveDirection;

            isPlayerMoving = movement.magnitude > 0.1f;
            if (movement.magnitude > 0.1f)
            {
                Vector3 targetPosition = transform.position + movement.normalized;
                transform.LookAt(targetPosition);
            }
        }
        
    }

    private void UpdateSpeed(float currentSatiety, float maxSatiety)
    {
        CheckSatiety(currentSatiety);
    }

    private void CheckSatiety(float currentSatiety)
    {
        if (currentSatiety <= 25)
        {
            CurrentSpeed = BaseSpeed * 0.5f;
        }
        else
        {
            CurrentSpeed = BaseSpeed;
        }
    }

    public void Initialize(float maxSpeed, float minSpeed, float initialSpeed)
    {
        MaxSpeed = maxSpeed;
        MinSpeed = minSpeed;
        BaseSpeed = initialSpeed;
        

        OnSpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
    }

    private void HandleDash(object sender, EventArgs e)
    {
        if (!isDashing)
        {
            Vector2 inputVector = GameInput.Instance.GetMovement();
            if (inputVector != Vector2.zero)
            {
                staminaComponent.TakeAction();
            }
            if (staminaComponent.isAction)
            {
                isDashing = true;
                dashTimeRemaining = dashDuration;
                    //staminaComponent.TakeAction();
                    if (staminaComponent.isAction)
                    {
                        Transform cameraTransform = Camera.main.transform;

                        Vector3 dashDirection = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
                        dashDirection.y = 0f;
                        dashDirection.Normalize();

                        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
                    }
            }
        }
    }
    
    public void SetCurrentSpeed(float value)
    {
        CurrentSpeed = value;
        OnSpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
    }

    public void SetMaxSpeed(float value)
    {
        MaxSpeed = value;
        OnSpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
    }

    public void SetMinSpeed(float value)
    {
        MinSpeed = value;
        OnSpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
    }

    public void SetBaseSpeed(float value)
    {
        BaseSpeed = value;
        OnSpeedChanged?.Invoke(CurrentSpeed, MaxSpeed);
    }

}
