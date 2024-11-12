using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;


public class BuildInputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayerMask;
    
    private PlayerStateManager playerStateManager;
    private Vector3 lastPosition;
    private float minX, maxX , minY, maxY, minZ, maxZ;

    
    public event Action OnClicked, OnExit;
    
    private void Start()
    {
        // Subscribe to the client connected callback to find the local player’s PlayerStateManager
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        // Only run for the local client
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            TryAssignLocalPlayerStateManager();
        }
    }
    
    private void TryAssignLocalPlayerStateManager()
    {
        // Find all NetworkObjects and identify the local player's PlayerStateManager
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                playerStateManager = networkObject.GetComponent<PlayerStateManager>();
                break;
            }
        }

        if (playerStateManager == null)
        {
            Debug.LogError("Local player's PlayerStateManager component not found.");
        }
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
            OnExit?.Invoke();
        }
    }

    public bool IsPointOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            //lastPosition = hit.point;
            lastPosition = GetValidPlacementPosition(hit.point);

        }

        return lastPosition;

    }

    // Calculates the valid placement position based on camera view
    private Vector3 GetValidPlacementPosition(Vector3 targetPosition)
    {
        // Get the camera's current position
        Vector3 cameraPosition = sceneCamera.transform.position;

        // Calculate the half size of the camera's view based on the orthographic size
        float halfWidth = sceneCamera.orthographicSize * ((float)Screen.width / Screen.height);
        float halfHeight = sceneCamera.orthographicSize;

        // Define the bounds based on the camera's position
        minX = cameraPosition.x ;
        maxX = cameraPosition.x + halfWidth;
        minY = cameraPosition.y - halfHeight;
        maxY = cameraPosition.y + halfHeight;
        minZ = cameraPosition.z ;
        maxZ = cameraPosition.z + halfWidth;

        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);
        float clampedZ = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        return new Vector3(clampedX, clampedY, clampedZ);
    }
    
    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }
   
}

