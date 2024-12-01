using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ForestNetworkObjectManager : MonoBehaviour
{
     private void Start()
        {
            // Ensure this logic only runs on the server
            if (NetworkManager.Singleton.IsServer)
            {
                // Get all NetworkObjects in the scene
                var networkObjects = FindObjectsOfType<NetworkObject>();
    
                foreach (var networkObject in networkObjects)
                {
                    if (!networkObject.IsSpawned)
                    {
                        networkObject.Spawn();
                    }
                }
            }
        }
}
