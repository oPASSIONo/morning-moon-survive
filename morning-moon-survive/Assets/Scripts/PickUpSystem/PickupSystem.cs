using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private float pickupRange = 2f; // Set the range within which the player can pick up items
    private PlayerInput playerInput;
    private InputAction pickup;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();
        pickup = playerInput.PlayerControls.Interaction;
    }

    private void Update()
    {
        // Check if the interaction button is pressed
        if (pickup.triggered)
        {
            PerformPickup();
        }
    }

    private void PerformPickup()
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
            }
        }
    }

    // Optional: Visualize the pickup range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}