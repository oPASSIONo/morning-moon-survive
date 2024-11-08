using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private GameObject playerRoot;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
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
        }
    }
    
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