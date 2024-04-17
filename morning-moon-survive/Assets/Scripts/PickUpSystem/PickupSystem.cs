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
    private bool isInRange = false;
    private PlayerInput playerInput;
    private InputAction pickup;
    private Collider enteredCollider; // Store the collider of the item when player enters its trigger area

    private void Awake()
    {
        
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();
            
        pickup = playerInput.PlayerControls.Interaction;
    }

    public override void OnNetworkSpawn()
    {
        if (!base.IsOwner)
        {
            enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the collision is with an item
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log("Step on Item");
            isInRange = true;
            enteredCollider = other; // Store the collider of the item
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        // Check if the collision with the item has ended
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log("Step out of Item");
            isInRange = false;
            enteredCollider = null; // Clear the stored collider
        }
    }
    
    private void Update()
    {
        // Check if the interaction button is pressed and the player is in range of an item
        if (isInRange && pickup.triggered)
        {
            PerformPickup();
        }
    }
    private void PerformPickup()
    {
        if (enteredCollider!=null)
        {
            // Get the item from the collision
            Item item = enteredCollider.GetComponent<Item>();

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


}
