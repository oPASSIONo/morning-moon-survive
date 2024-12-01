using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public class CameraFollow : NetworkBehaviour
{
   // public static CameraFollow Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public override void OnNetworkSpawn()
    {
        /*if (Instance == null)
        {
            Instance = this;
        }*/
        
        StartCoroutine(AssignCameraWhenReady());
    }


    private IEnumerator AssignCameraWhenReady()
    {
        // Wait for the local player's NetworkObject to be available
        while (NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject() == null)
        {
            yield return null;
        }
        AssignCameraToPlayer();
    }

    public void AssignCameraToPlayer()
    {
        // Get the local player's object
        var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();

        if (playerObject == null)
        {
            Debug.LogError("Local player object not found!");
            return;
        }

        // Find "PlayerCameraRoot" in the local player's hierarchy
        var playerCameraRoot = playerObject.transform.Find("PlayerCameraRoot");
        if (playerCameraRoot != null)
        {
            virtualCamera.Follow = playerCameraRoot;
            virtualCamera.LookAt = playerCameraRoot;
        }
        else
        {
            Debug.LogError("PlayerCameraRoot not found in the player prefab!");
        }
    }

    /*public override void OnNetworkSpawn()
    {

            // Assuming your player prefab has a PlayerRoot object
            playerRoot = GameObject.Find("PlayerCameraRoot"); // Or use another way to get the player's root
            if (virtualCamera != null && playerRoot != null)
            {
                virtualCamera.Follow = playerRoot.transform;
                virtualCamera.LookAt = playerRoot.transform;
            }
            else
            {
                Debug.LogError("Virtual Camera or PlayerRoot not found!");
            }
    }*/
    
    /*private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void FollowPlayer(Transform targetTransform)
    {
        if (cinemachineVirtualCamera != null)
        {
            cinemachineVirtualCamera.Follow = targetTransform;
        }

    }*/
}