using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupSystem : NetworkBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private float pickupRange = 2f; // Set the range within which the player can pick up items
    private PlayerInput playerInput;
    private InputAction pickup;
    
    private PlayerAnimation playerAnimation;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();
        pickup = playerInput.PlayerControls.Interaction;
    }

    private void Start()
    {
        // Only do this for the local player
        if (IsLocalPlayer)
        {
            // Get the PlayerAnimation component on this GameObject
            playerAnimation = GetComponent<PlayerAnimation>();
        }
    }
    private void Update()
    {
        if (!IsLocalPlayer) return;

        if (pickup.triggered)
        {
            PerformPickup();
        }
    }
    private void PerformPickup()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        
        foreach (var hitCollider in hitColliders)
        {
            Item item = hitCollider.GetComponent<Item>();

            if (item != null && item.NetworkObject.IsSpawned)
            {
                // Request the server to pick up the item
                RequestPickupServerRPC(item.NetworkObject.NetworkObjectId);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestPickupServerRPC(ulong itemNetworkObjectId, ServerRpcParams rpcParams = default)
    {
        var item = NetworkManager.SpawnManager.SpawnedObjects[itemNetworkObjectId]?.GetComponent<Item>();
        
        if (item == null) return;

        // Try to add the item to the inventory
        int remainder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
        if (remainder == 0)
        {
            // Fully picked up; destroy on all clients
            item.DestroyItemClientRPC();
        }
        else
        {
            // Partially picked up; update quantity
            item.UpdateQuantityClientRPC(remainder);
        }

        // Notify the player who picked it up
        NotifyPickupClientRPC(rpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void NotifyPickupClientRPC(ulong clientId)
    {
        if (IsLocalPlayer && NetworkManager.LocalClientId == clientId)
        {
            playerAnimation?.PlayerPickupAnim();
        }
    }
    /*private void PerformPickup()
    {
        // Use a sphere cast to detect items within the pickup range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        
        foreach (var hitCollider in hitColliders)
        {
            Item item = hitCollider.GetComponent<Item>();

            // Check if the item is valid and can be picked up
            if (item != null)
            {
                // Try to add the item to the inventory
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    // If the item was added successfully, destroy it
                    item.DestroyItem();
                }
                else
                {
                    // If the item was only partially added, update its quantity
                    item.Quantity = reminder;
                }
                playerAnimation.PlayerPickupAnim();
            }
        }
    }*/

    // Optional: Visualize the pickup range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}