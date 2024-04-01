using System;
using System.Collections;
using System.Collections.Generic;
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
    private float speed = 5f;

    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
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
        if (!IsOwner) return;
        {
            MovePlayer();
            RotationPlayer();
        }
    }

    /*void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Convert the input direction to a 3D vector
        Vector3 movement = new Vector3(direction.x, 0f, direction.y);

        // Apply speed and deltaTime
        Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;

        // Apply movement
        transform.Translate(moveDirection, Space.World);
    }*/
    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        
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

    void RotationPlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Get the main camera's transform
        Transform cameraTransform = cm.transform;

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
