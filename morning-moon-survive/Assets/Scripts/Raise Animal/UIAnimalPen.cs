using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;

public class UIAnimalPen : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject animalPenCanvas;
    [SerializeField] private AnimalPen animalPen;
    [SerializeField] private UIAnimalEgg animalEggUIPrefab;
    [SerializeField] private RectTransform contentPanel;
    
    private InventorySO playerInventory;
    private List<UIAnimalEgg> listOfUIAnimalEggs = new List<UIAnimalEgg>();

    /// <summary>
    /// Interacts with the animal pen and retrieves the player's inventory.
    /// </summary>
    public void Interact(GameObject player)
    {
        playerInventory = player.GetComponent<InventoryController>()?.GetInventoryData();

        if (playerInventory != null)
        {
            animalPenCanvas.SetActive(true);
            animalPen.GetPlayerInventory(playerInventory);
            PopulateAnimalUI();
        }
        else
        {
            Debug.LogError("Player does not have an inventory!");
        }
    }

    private void PopulateAnimalUI()
    {
        ClearAnimalUI();
        
        Dictionary<int, InventoryItem> inventoryItems = playerInventory.GetCurrentInventoryState();

        foreach (var itemPair in inventoryItems)
        {
            if (itemPair.Value.item is AnimalEggSO animalEgg)
            {
                UIAnimalEgg eggUIItem = Instantiate(animalEggUIPrefab, contentPanel);
                eggUIItem.SetData(animalEgg.ItemImage, animalEgg.Name, itemPair.Value.quantity, animalEgg);
                eggUIItem.OnEggClicked += OnEggItemClicked;

                listOfUIAnimalEggs.Add(eggUIItem);
            }
        }
    }

    private void OnEggItemClicked(AnimalEggSO selectedEgg)
    {
        animalPen.SetSelectedEgg(selectedEgg.Animal);
        animalPen.GetEggIndex(playerInventory.GetItemIndex(selectedEgg));
    }

    private void ClearAnimalUI()
    {
        foreach (var item in listOfUIAnimalEggs)
        {
            Destroy(item.gameObject);
        }
        listOfUIAnimalEggs.Clear();
        animalPen.SetSelectedEgg(null);
    }

    public void ShowInteractPrompt() => Debug.Log("Press E to interact with the Animal Pen");

    public void HideInteractPrompt() => Debug.Log("Out of range to interact with the Animal Pen");
}
