using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;

    public int inventorySize = 10;
    
    private PlayerInput playerInput;
    private InputAction openInventoryAction;
    
    
    void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        playerInput = GetComponent<PlayerInput>();
        openInventoryAction = playerInput.PlayerControls.Inventory;
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
    }

}
