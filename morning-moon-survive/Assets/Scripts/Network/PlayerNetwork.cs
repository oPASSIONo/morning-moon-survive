using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerNetwork : NetworkBehaviour
{
    
    [SerializeField] private CinemachineVirtualCamera vc;
    [SerializeField] private Camera cm;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private float speed = 3f;
    
    private float currentSpeed; 
    public Hunger Hunger { private get; set; }
    
    void Start()
    {
        Hunger = GetComponent<Hunger>();

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
            vc.Priority = 1;
        }
        else
        {
            vc.Priority = 0;
        }
        
    }
    
    void Update()
    {

        if (!IsOwner)
            return;

        if (IsClient && IsOwner)
        {
            MovePlayer();
        }
        UpdateSpeed(Hunger.CurrentHunger, Hunger.MaxHunger);

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
        if (!IsOwner)
            return;

        if (currentHunger >= 75)
        {
            currentSpeed = speed * 0.5f; // Reduce the speed to 50%
        }
        else
        {
            // If hunger is below 75, reset the speed to the base speed
            currentSpeed = speed;
        }

        // If this is the owner client, synchronize the speed across the network
        UpdateSpeedServerRpc(currentSpeed);
    }

    // Server RPC to synchronize the speed across the network
    [ServerRpc]
    private void UpdateSpeedServerRpc(float speedValue)
    {
        currentSpeed = speedValue;
    }
  
    
}
