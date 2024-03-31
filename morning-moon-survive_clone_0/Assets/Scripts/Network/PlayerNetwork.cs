using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetwork : NetworkBehaviour 
{
    private PlayerInput playerInput;

    private InputAction moveAction;
    
    private float speed = 5f;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnPositionRange = 5f;
    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(Random.Range(spawnPositionRange, -spawnPositionRange), 0,
            Random.Range(spawnPositionRange, -spawnPositionRange));
        transform.rotation = new Quaternion(0, 180, 0, 0);
        
        
    }

    void Update()
    {
        if (!IsOwner ) return;
        {
            MovePlayer();
            RotationPlayer();
        }
    }
    
    void MovePlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        
        Transform cameraTransform = Camera.main.transform;

        Vector3 movement = cameraTransform.forward * direction.y + cameraTransform.right * direction.x;
        movement.y = 0f; 

        Vector3 moveDirection = movement.normalized * speed * Time.deltaTime;

        transform.position += moveDirection;
        
    }
    
    void RotationPlayer()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        Transform cameraTransform = Camera.main.transform;

        Vector3 movement = cameraTransform.forward * direction.y + cameraTransform.right * direction.x;
        movement.y = 0f; 

        Vector3 targetPosition = transform.position + movement.normalized;

        if (movement.magnitude > 0.1f) // Check if there is significant movement
        {
            transform.LookAt(targetPosition);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdatePositionServerRpc()
    {
        transform.position = new Vector3(Random.Range(spawnPositionRange, -spawnPositionRange), 0,
            Random.Range(spawnPositionRange, -spawnPositionRange));
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }

}
