using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;

public class AnimalPen : MonoBehaviour
{
    [SerializeField] private int maxCapacity = 5;
    [SerializeField] private Transform[] penPositions;

    private List<Animal> animalsInPen = new List<Animal>();
    private AnimalSO currentSpecies;

    private InventorySO playerInventory;

    public void GetPlayerInventory(InventorySO _playerInventory)
    {
        playerInventory = _playerInventory;
    }
    
    public void AddAnimalToPen()
    {
        AnimalEggSO eggInInventory = playerInventory.GetAnimalEggInInventory();
        if (eggInInventory != null)
        {
            // Add the animal to the pen
            AnimalSO animalToAdd = eggInInventory.Animal;
            // Logic for adding the animal to the pen goes here
            AddAnimal(animalToAdd);
            // Remove the egg from the inventory
            int eggIndex = playerInventory.GetItemIndex(eggInInventory);
            if (eggIndex != -1)
            {
                playerInventory.RemoveItem(eggIndex, 1); // Removes 1 egg
            }
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
            return ;
        }

        if (currentSpecies == null)
        {
            currentSpecies = animalData;
        }

        if (animalData.speciesName != currentSpecies.speciesName)
        {
            Debug.Log("Can't add different species!");
            return ;
        }

        Animal newAnimal = InstantiateAnimal(animalData);
        animalsInPen.Add(newAnimal);

        //return true;
    }

    private Animal InstantiateAnimal(AnimalSO animalData)
    {
        int positionIndex = animalsInPen.Count;
        GameObject animalObject = Instantiate(animalData.babyPrefab, penPositions[positionIndex].position, Quaternion.identity);

        Animal animal = animalObject.AddComponent<Animal>();
        animal.Initialize(animalData);

        return animal;
    }

    /// <summary>
    /// Feeds all animals in the pen.
    /// </summary>
    public void FeedAllAnimals()
    {
        foreach (var animal in animalsInPen)
        {
            animal.Feed(); // Feed each animal individually
        }
    }

    public void RemoveAnimal(Animal animal)
    {
        animalsInPen.Remove(animal);
        Destroy(animal.gameObject);

        if (animalsInPen.Count == 0)
        {
            currentSpecies = null;
        }
    }
    
}



