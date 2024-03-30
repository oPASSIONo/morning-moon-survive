using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction moveAction;
    
    private float speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
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
        Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;

        // Apply movement
        transform.position += moveDirection;
        
    }
    
    /*void RotationPlayer()
    {
        Vector2 currentMovement = moveAction.ReadValue<Vector2>();
        
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = new Vector3(currentMovement.x,0,currentMovement.y);
        Vector3 positionToLookAt = currentPosition += newPosition;
        transform.LookAt(positionToLookAt);
    }*/
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

}
