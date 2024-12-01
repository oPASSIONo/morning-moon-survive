using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnObject : MonoBehaviour
{
    /*private void Start()
    {
        /*if (NetworkManager.Singleton.IsServer)
        {
            NetworkObject networkObject = GetComponent<NetworkObject>();
            if (networkObject != null && !networkObject.IsSpawned)
            {
                networkObject.Spawn();
            }
        }#1#
        if (NetworkManager.Singleton.IsServer && NetworkManager.Singleton.IsListening)
        {
            NetworkObject networkObject = GetComponent<NetworkObject>();
            if (networkObject != null && !networkObject.IsSpawned)
            {
                networkObject.Spawn();
                Debug.Log($"Spawned {gameObject.name} as a NetworkObject.");
            }
            else if (networkObject == null)
            {
                Debug.LogError("Missing NetworkObject component on the object.");
            }
        }
    }*/
}
