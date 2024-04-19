using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Object = UnityEngine.Object;

public class PlayerNetwork : NetworkBehaviour
{
    public static PlayerNetwork Instance { get; private set; }
    
    [SerializeField] private CinemachineVirtualCamera vc;
    [SerializeField] private Camera cm;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private float speed = 3f;
    
      
    //private float baseSpeed = 5f; // Base movement speed
    private float currentSpeed; // Current movement speed
  
    // Reference to the Hunger component
    public Hunger Hunger { private get; set; }
    
    void Start()
    {
        // Get the Hunger component
        Hunger = GetComponent<Hunger>();

        // Check if Hunger component exists
        if (Hunger != null)
        {
            // Subscribe to the OnHungerChanged event
            Hunger.OnHungerChanged += UpdateSpeed;

        }
        else
        {
            Debug.LogWarning("Hunger component not found.");
        }
    }
    

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Instance = this;
            vc.Priority = 1;
        }
        else
        {
            vc.Priority = 0;
        }

        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        }
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == OwnerClientId)
        {
            
        }
    }
    
    void Update()
    {
        if (!IsOwner) return;
        {
            MovePlayer();
            UpdateSpeed(Hunger.CurrentHunger , Hunger.MaxHunger);
        }
    }
    
    void MovePlayer()
    {
        Vector2 inputVector = GameInput.Instance.GetMovement();
        
        // Get the main camera's transform
        Transform cameraTransform = cm.transform;

        // Calculate movement direction relative to the camera's forward direction
        Vector3 movement = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
        movement.y = 0f; // Ensure the movement stays in the horizontal plane

        // Apply speed and deltaTime
        Vector3 moveDirection = movement.normalized * currentSpeed * Time.deltaTime;

        // Apply movement
        transform.position += moveDirection;
        Vector3 targetPosition = transform.position + movement.normalized;

        // Make the player look at the target position
        if (movement.magnitude > 0.1f) // Check if there is significant movement
        {
            transform.LookAt(targetPosition);
        }

    }
    
    void UpdateSpeed(int currentHunger, int maxHunger)
    {
        if (!IsOwner)return;
        {
            CheckHunger(currentHunger); // Pass the current hunger received as parameter
        }
    }

    // Method to update the player's speed based on the current hunger level

    private void CheckHunger(int currentHunger)
    {
        if (!IsOwner) return;
        {
            if (currentHunger >= 75)
            {
                currentSpeed = speed * 0.5f; // Reduce the speed to 50%
                Debug.Log(currentSpeed);
            }
            else
            {
                // If hunger is below 75, reset the speed to the base speed
                currentSpeed = speed;
            }
        }
    }
   

    // Unsubscribe from the OnHungerChanged event when the script is destroyed
    private void OnDestroy()
    {
        if (Hunger != null)
        {
            Hunger.OnHungerChanged -= UpdateSpeed;
        }
    }

    void RotationPlayer()
    {
        Vector2 inputVector = GameInput.Instance.GetMovement();

        // Get the main camera's transform
        Transform cameraTransform = cm.transform;

        // Calculate movement direction relative to the camera's forward direction
        Vector3 movement = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
        movement.y = 0f; // Ensure the movement stays in the horizontal plane

        // Calculate the target position to look at
        Vector3 targetPosition = transform.position + movement.normalized;

        // Make the player look at the target position
        if (movement.magnitude > 0.1f) // Check if there is significant movement
        {
            transform.LookAt(targetPosition);
        }
    }
    
    private void MovePlayerServerAuth()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        MovePlayerServerRpc(direction);
    }

    [ServerRpc(RequireOwnership = false)]
    private void MovePlayerServerRpc(Vector2 direction)
    {
         
        // Get the main camera's transform
        Transform cameraTransform = cm.transform;

        // Calculate movement direction relative to the camera's forward direction
        Vector3 movement = cameraTransform.forward * direction.y + cameraTransform.right * direction.x;
        movement.y = 0f; // Ensure the movement stays in the horizontal plane

        // Apply speed and deltaTime
        Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;

        // Apply movement
        transform.position += moveDirection;

    }

}
