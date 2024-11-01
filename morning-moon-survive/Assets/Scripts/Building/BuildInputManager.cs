using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildInputManager : MonoBehaviour
{


    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private PlayerStateManager playerStateManager;

    private Vector3 lastPosition;
    
    public event Action OnClicked, OnExit;
    
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
        float minX = cameraPosition.x ;
        float maxX = cameraPosition.x + halfWidth;
        float minY = cameraPosition.y - halfHeight;
        float maxY = cameraPosition.y + halfHeight;
        float minZ = cameraPosition.z ;
        float maxZ = cameraPosition.z + halfWidth;

        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);
        float clampedZ = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        return new Vector3(clampedX, clampedY, clampedZ);
    }
   
}
