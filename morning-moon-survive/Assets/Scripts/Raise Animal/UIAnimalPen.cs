using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimalPen : MonoBehaviour,IInteractable
{
    [SerializeField] private GameObject animalPenUI;
    
    [SerializeField] private AnimalPen animalPen;

    /// <summary>
    /// Interact with the animal pen, retrieving the player's inventory when needed.
    /// </summary>
    public void Interact(GameObject player)
    {
        // Try to get the player's Inventory component
        InventorySO playerInventory = player.GetComponent<InventoryController>()?.GetInventoryData();
        
        if (playerInventory != null)
        {
            animalPenUI.SetActive(true);
            animalPen.GetPlayerInventory(playerInventory);
            PopulateAnimalUI(playerInventory);
        }
        else
        {
            Debug.LogError("Player does not have an inventory!");
        }
    }

    private void PopulateAnimalUI(InventorySO playerInventory)
    {
        ClearAnimalUI();
    }

    private void ClearAnimalUI()
    {
        
    }

    private void OnAddAnimalBtnClicked()
    {
        
    }

    private void SetDescription()
    {
        
    }
    public void ShowInteractPrompt()
    {
        Debug.Log("Press E to interact with the Animal Pen");
    }

    public void HideInteractPrompt()
    {
        Debug.Log("Out of range to interact with the Animal Pen");
    }
    
}
