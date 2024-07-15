using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float MaxSpeed { get; private set; }
    public float MinSpeed { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float BaseSpeed { get; private set; }
    public event Action<float, float> OnSpeedChanged;

    
    private Satiety satietyComponent;
    
    void Start()
    {

        satietyComponent = GetComponent<Satiety>();
        if (satietyComponent != null)
        {
            satietyComponent.OnSatietyChanged += UpdateSpeed;

            UpdateSpeed(satietyComponent.CurrentSatiety, satietyComponent.MaxSatiety);
            
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
        Vector3 moveDirection = movement.normalized * CurrentSpeed * Time.deltaTime; // Use currentSpeed here

        // Apply movement
        transform.position += moveDirection;
        Vector3 targetPosition = transform.position + movement.normalized;

        // Make the player look at the target position
        if (movement.magnitude > 0.1f) // Check if there is significant movement
        {
            transform.LookAt(targetPosition);
        }
    }


    void UpdateSpeed(float currentHunger, float maxHunger)
    {
        CheckSatiety(satietyComponent.CurrentSatiety); // Pass the current hunger received as parameter
    }

    // Method to update the player's speed based on the current hunger level

    private void CheckSatiety(float currentSatiety)
    {
        if (satietyComponent.CurrentSatiety <= 25)
        {
            CurrentSpeed = BaseSpeed * 0.5f; // Reduce the speed to 50%
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
        
        OnSpeedChanged?.Invoke(CurrentSpeed,MaxSpeed);
    }
}
