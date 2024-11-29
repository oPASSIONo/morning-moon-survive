using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPenView : MonoBehaviour
{
    [SerializeField] private GameObject animalPenCanvas;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private UIAnimalEgg eggUIPrefab;
    [SerializeField] private Button addAnimalButton;  // Button to add animal
    [SerializeField] private Button feedAnimalButton;
    private readonly List<UIAnimalEgg> eggUIInstances = new();

    // Store the associated controller for this pen
    private AnimalPenController associatedController;

    public void SetPenController(AnimalPenController controller)
    {
        associatedController = controller;
        // Set up the button to call AddAnimalToPen for this pen controller
        addAnimalButton.onClick.RemoveAllListeners();  // Remove any old listeners
        addAnimalButton.onClick.AddListener(() => associatedController.AddAnimalToPen());
        feedAnimalButton.onClick.RemoveAllListeners();
        feedAnimalButton.onClick.AddListener(()=>associatedController.FeedAnimals());
    }
    public void UpdatePen(List<Animal> animals)
    {
        Debug.Log($"Updated pen with {animals.Count} animals.");
        // Here you can add animations or visual updates for animals in the pen.
    }

    public void UpdateEggUI(InventorySO playerInventory, AnimalPenController controller)
    {
        ClearUI();
        
        Dictionary<int, InventoryItem> inventoryItems = playerInventory.GetCurrentInventoryState();
        Debug.Log(inventoryItems.Count);
        
        foreach (var itemPair in inventoryItems)
        {
            if (itemPair.Value.item is AnimalEggSO animalEgg)
            {
                var eggUI = Instantiate(eggUIPrefab, contentPanel);
                eggUI.SetData(animalEgg.ItemImage, animalEgg.Name, itemPair.Value.quantity, animalEgg);
                eggUI.OnEggClicked += egg =>
                {
                    // Get the index of the clicked egg
                    int eggIndex = playerInventory.GetItemIndex(egg);

                    // Now pass the correct ItemSO to GetEggIndex
                    controller.SelectEgg(egg, controller.GetEggIndex(eggIndex)); 
                    
                };
                eggUIInstances.Add(eggUI);
            }
        }
        
    }

    public void DisplayFeedFeedback()
    {
        Debug.Log("All animals have been fed!");
        // Trigger animations or audio feedback here.
    }

    public void Show() => animalPenCanvas.SetActive(true);
    public void Hide() => animalPenCanvas.SetActive(false);

    private void ClearUI()
    {
        foreach (var ui in eggUIInstances)
        {
            Destroy(ui.gameObject);
        }
        eggUIInstances.Clear();
    }
}