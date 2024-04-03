using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private InventorySO inventoryData;
    
    private PlayerInput playerInput;
    private InputAction openInventoryAction;
    
    
    void Start()
    {
        PrepareUI();
        //inventoryData.Initialize();
        playerInput = GetComponent<PlayerInput>();
        openInventoryAction = playerInput.actions.FindAction("Inventory");
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        inventoryUI.OnSwapItems += HandleSwapItems;
        inventoryUI.OnStartDragging += HandleDraggin;
        inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        
    }
    
    private void HandleDraggin(int itemIndex)
    {
        
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex,item.ItemImage,item.name,item.ItemAbility);
    }

    void Update()
    {
        if (openInventoryAction.triggered)
        {
            OpenInventoryUI();
        }
    }

    void OpenInventoryUI()
    {
        inventoryUI.Show();
        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key,item.Value.item.ItemImage,item.Value.quantity);
        }
    }

}
