using System;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;

public class AnimalPenController : MonoBehaviour,IInteractable
{
    [SerializeField] private AnimalPenModel animalPenModel;
    private AnimalPenView animalPenView;
    

    private InventorySO playerInventory;
    private int eggIndex = -1;
    public int GetEggIndex(int value) => eggIndex = value;
    private void Awake()
    {
        /*if (animalPenModel == null) Debug.LogError("AnimalPenModel is missing!");
        if (animalPenView == null) Debug.LogError("AnimalPenView is missing!");*/
    }

    private void Start()
    {
        animalPenView = GameCanvas.Instance.GetAnimalPenView();
        if (animalPenModel == null) Debug.LogError("AnimalPenModel is missing!");
        if (animalPenView == null) Debug.LogError("AnimalPenView is missing!");
    }


    public void Interact(GameObject player)
    {
        playerInventory = player.GetComponent<InventoryController>()?.GetInventoryData();
        if (playerInventory == null)
        {
            Debug.LogError("Player inventory not found!");
            return;
        }
        animalPenView.Show();
        animalPenView.UpdateEggUI(playerInventory, this);
        animalPenView.SetPenController(this);  // Associate this controller with the view
        UpdateView();
    }

    

    public void ShowInteractPrompt()
    {
        //throw new System.NotImplementedException();
    }

    public void HideInteractPrompt()
    {
        //throw new System.NotImplementedException();
    }

    public void SelectEgg(AnimalEggSO eggData, int index)
    {
        animalPenModel.SetSelectedAnimal(eggData.Animal);
        eggIndex = index;
    }

    public void AddAnimalToPen()
    {
        if (eggIndex == -1 || playerInventory == null)
        {
            Debug.Log($"eggIndex: {eggIndex}");
            Debug.Log($"playerInventory: {playerInventory}");
            Debug.LogError("Invalid egg index or missing player inventory!");
            return;
        }

        if (animalPenModel.CanAddAnimal())
        {
            var selectedAnimal = animalPenModel.GetSelectedAnimal();
            if (selectedAnimal != null)
            {
                if (playerInventory.GetItemAt(eggIndex).quantity<=0)
                {
                    Debug.Log("Cannot add animal to the pen. Player doesn't have enough egg");
                    return;
                }
                playerInventory.RemoveItem(eggIndex, 1);
                animalPenModel.AddAnimal(selectedAnimal);
                animalPenView.UpdateEggUI(playerInventory, this);  // Re-update the egg UI

                UpdateView();
            }
        }
        else
        {
            Debug.Log("Cannot add animal to the pen.");
        }
        animalPenView.Hide();
    }

    public void FeedAnimals()
    {
        animalPenModel.FeedAnimals();
        animalPenView.DisplayFeedFeedback();
        animalPenView.Hide();
    }

    public void RemoveAnimal(Animal animal)
    {
        animalPenModel.RemoveAnimal(animal);
        UpdateView();
    }

    private void UpdateView()
    {
        animalPenView.UpdatePen(animalPenModel.GetAllAnimals());
    }
}
