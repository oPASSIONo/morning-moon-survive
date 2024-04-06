using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction moveAction;
    
    private float baseSpeed = 5f; // Base movement speed
    private float currentSpeed; // Current movement speed
    
    // Reference to the Hunger component
    [SerializeField] private Hunger hunger;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        // Set the initial speed to the base speed
        currentSpeed = baseSpeed;

        // Subscribe to the OnHungerChanged event
        if (hunger != null)
        {
            hunger.OnHungerChanged += UpdateSpeed;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotationPlayer();
    }
    
    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        
        // Get the main camera's transform
        Transform cameraTransform = Camera.main.transform;
        

        // Calculate movement direction relative to the camera's forward direction
        Vector3 movement = cameraTransform.forward * direction.y + cameraTransform.right * direction.x;
        movement.y = 0f; // Ensure the movement stays in the horizontal plane

        // Apply speed and deltaTime
        Vector3 moveDirection = movement.normalized * currentSpeed * Time.deltaTime;

        // Apply movement
        transform.position += moveDirection;
        
    }
    
    void RotationPlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Get the main camera's transform
        Transform cameraTransform = Camera.main.transform;

        // Calculate movement direction relative to the camera's forward direction
        Vector3 movement = cameraTransform.forward * direction.y + cameraTransform.right * direction.x;
        movement.y = 0f; // Ensure the movement stays in the horizontal plane

        // Calculate the target position to look at
        Vector3 targetPosition = transform.position + movement.normalized;

        // Make the player look at the target position
        if (movement.magnitude > 0.1f) // Check if there is significant movement
        {
            transform.LookAt(targetPosition);
        }
    }

    // Method to update the player's speed based on the current hunger level
    void UpdateSpeed(int currentHunger, int maxHunger)
    {
        // If hunger is greater than or equal to 75, decrease the speed
        if (currentHunger >= 75)
        {
            currentSpeed = baseSpeed * 0.5f; // Reduce the speed to 50%
            Debug.Log(currentSpeed);
            
        }
        else
        {
            // If hunger is below 75, reset the speed to the base speed
            currentSpeed = baseSpeed;
        }
    }
    
    // Unsubscribe from the OnHungerChanged event when the script is destroyed
    void OnDestroy()
    {
        if (hunger != null)
        {
            hunger.OnHungerChanged -= UpdateSpeed;
        }
    }
    
}
