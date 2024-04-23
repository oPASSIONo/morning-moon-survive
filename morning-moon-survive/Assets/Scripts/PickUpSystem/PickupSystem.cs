using System;
using System.Collections;
using Inventory.Model;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PickupSystem : NetworkBehaviour
{
    public static PickupSystem Instance { get; private set; }

    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private float duration = 0.3f;
    private InputAction pickup;
    private bool isInRange = false;
    private Collider enteredCollider;
    private PlayerInput playerInput;

      private void Awake()
    {
        Instance = this;
        playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();

        pickup = playerInput.PlayerControls.Interaction;
        pickup.performed += OnPickupPerformed;
    }

    private void OnDisable()
    {
        pickup.performed -= OnPickupPerformed;
    }

    private void OnPickupPerformed(InputAction.CallbackContext context)
    {
        if (!IsOwner || !isInRange)
            return;

        PerformPickupServerRpc();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner)
            return;

        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            isInRange = true;
            enteredCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner)
            return;

        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            isInRange = false;
            enteredCollider = null;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void PerformPickupServerRpc()
    {
        PerformPickup();
    }
    
    private void PerformPickup()
    {
        if (enteredCollider != null)
        {
            Item item = enteredCollider.GetComponent<Item>();
            if (item != null)
            {
                item.NetworkObject.ChangeOwnership(OwnerClientId);
                
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    item.DestroyItemServerRpc();
                }
                else
                {
                    item.Quantity = reminder;
                }
            }
        }
    }
}
