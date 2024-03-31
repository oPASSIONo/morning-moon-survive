using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

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

    }
}