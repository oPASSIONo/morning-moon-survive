using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class AnimalPen : MonoBehaviour
{
    [SerializeField] private int maxCapacity = 5;
    [SerializeField] private Transform[] penPositions;

    private List<Animal> animalsInPen = new List<Animal>();
    private InventorySO playerInventory;
    private AnimalSO selectingEgg;
    private int eggIndex;

    public void GetPlayerInventory(InventorySO _playerInventory) => playerInventory = _playerInventory;

    public void GetEggIndex(int value) => eggIndex = value;

    public void SetSelectedEgg(AnimalSO animalSo) => selectingEgg = animalSo;

    public void AddAnimalToPen()
    {
        if (selectingEgg != null)
        {
            AddAnimal(selectingEgg);
        }
        else
        {
            Debug.Log("No Animal Egg found in the inventory.");
        }
    }

    private void AddAnimal(AnimalSO animalData)
    {
        if (animalsInPen.Count >= maxCapacity)
        {
            Debug.Log("Pen is full!");
            return;
        }

        if (animalsInPen.Count > 0 && animalData.speciesName != animalsInPen[0].GetAnimalData().speciesName)
        {
            Debug.Log("Can't add different species!");
            return;
        }

        if (eggIndex != -1 && !playerInventory.GetItemAt(eggIndex).IsEmpty)
        {
            playerInventory.RemoveItem(eggIndex, 1); // Removes 1 egg
            Animal newAnimal = InstantiateAnimal(animalData);
            animalsInPen.Add(newAnimal);
        }
    }

    private Animal InstantiateAnimal(AnimalSO animalData)
    {
        GameObject animalObject = Instantiate(animalData.babyPrefab, penPositions[animalsInPen.Count].position, Quaternion.identity);
        Animal animal = animalObject.AddComponent<Animal>();
        animal.Initialize(animalData);
        return animal;
    }

    public void FeedAllAnimals()
    {
        foreach (var animal in animalsInPen)
        {
            animal.Feed(); // Feed each animal individually
        }
    }

    public void RemoveAnimal(Animal animal)
    {
        if (animalsInPen.Remove(animal))
        {
            Destroy(animal.gameObject);
        }

        if (animalsInPen.Count == 0)
        {
            selectingEgg = null; // Reset the selecting egg when the pen is empty
        }
    }
}
