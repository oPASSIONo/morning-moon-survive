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

    private bool isInRange = false;
    private PlayerInput playerInput;
    private InputAction pickup;
    private Collider enteredCollider;

    private void Awake()
    {
        Instance = this;
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
        if (!IsOwner)
            return;

        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log("Step on Item");
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
            Debug.Log("Step out of Item");
            isInRange = false;
            enteredCollider = null;
        }
    }
    
    private void Update()
    {
        if (!IsOwner)
            return;

        if (isInRange && pickup.triggered)
        { 
            PerformPickupServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void PerformPickupServerRpc()
    {
        if (IsOwner)
        {
            PerformPickup();
        }
        else
        {
            PerformPickupClientRpc();
        }
    }

    [ClientRpc(RequireOwnership = false)]
    private void PerformPickupClientRpc()
    {
        if (!IsOwner) return;
        PerformPickup();
    }
    
    private void PerformPickup()
    {
        if (enteredCollider != null)
        {
            Item item = enteredCollider.GetComponent<Item>();
            if (item != null)
            {
                int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                if (reminder == 0)
                {
                    // Call the network synchronized method to destroy the item
                    item.DestroyItem();
                }
                else
                {
                    item.Quantity = reminder;
                }
            }
        }
    }
}
