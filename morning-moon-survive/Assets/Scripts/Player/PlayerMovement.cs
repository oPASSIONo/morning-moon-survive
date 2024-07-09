using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /*private PlayerInput playerInput;

    private InputAction moveAction;*/
    
    private float baseSpeed = 5f; // Base movement speed
    private float currentSpeed; // Current movement speed
    
    // Reference to the Hunger component
    public Hunger Hunger { private get; set; } // Reference to the Health component for the player

    // Start is called before the first frame update
    void Start()
    {
        // Get the Hunger component
        Hunger = GetComponent<Hunger>();

        // Check if Hunger component exists
        if (Hunger != null)
        {
            // Subscribe to the OnHungerChanged event
            Hunger.OnHungerChanged += UpdateSpeed;

            // Initialize the player speed
            UpdateSpeed(Hunger.CurrentHunger, Hunger.MaxHunger);
            
        }
        else
        {
            Debug.LogWarning("Hunger component not found.");
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
       // RotationPlayer();
    }
    
    void MovePlayer()
    {
        Vector2 inputVector = GameInput.Instance.GetMovement();
    
        // Get the main camera's transform
        Transform cameraTransform =  Camera.main.transform;

        // Calculate movement direction relative to the camera's forward direction
        Vector3 movement = cameraTransform.forward * inputVector.y + cameraTransform.right * inputVector.x;
        movement.y = 0f; // Ensure the movement stays in the horizontal plane

        // Apply speed and deltaTime
        Vector3 moveDirection = movement.normalized * currentSpeed * Time.deltaTime; // Use currentSpeed here

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
        CheckHunger(currentHunger); // Pass the current hunger received as parameter
    }

    // Method to update the player's speed based on the current hunger level

    private void CheckHunger(int currentHunger)
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
        if (Hunger != null)
        {
            Hunger.OnHungerChanged -= UpdateSpeed;
        }
    }
    
        
    /*void RotationPlayer()
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
    }*/
}
