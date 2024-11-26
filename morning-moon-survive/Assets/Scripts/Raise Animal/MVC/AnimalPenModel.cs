using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimalPenModel
{
    private List<Animal> animalsInPen = new();
    private AnimalSO selectedAnimal;
    private const int maxCapacity = 5;
    [SerializeField] private List<Transform> spawnPositions = new();  // List of spawn points in the pen

    public bool CanAddAnimal()
    {
        if (animalsInPen.Count >= maxCapacity) return false;
        if (animalsInPen.Count > 0 && animalsInPen[0].GetAnimalData().speciesName != selectedAnimal.speciesName) return false;
        return true;
    }

    public void SetSelectedAnimal(AnimalSO animalData)
    {
        selectedAnimal = animalData;
    }

    public AnimalSO GetSelectedAnimal()
    {
        return selectedAnimal;
    }

    public void AddAnimal(AnimalSO animalData)
    {
        if (!CanAddAnimal()) return;

        
        
        if (spawnPositions.Count > animalsInPen.Count)  // Ensure we have spawn positions available
        {
            // Get the next available spawn position
            Transform spawnPosition = spawnPositions[animalsInPen.Count];

            GameObject animalObject = GameObject.Instantiate(animalData.babyPrefab, spawnPosition.position, Quaternion.identity);
            Animal animal = animalObject.AddComponent<Animal>();
            animal.Initialize(animalData);
            animalsInPen.Add(animal);
        }
        else
        {
            Debug.LogWarning("No available spawn positions for the animal!");
        }
    }
    
    

    public void FeedAnimals()
    {
        foreach (var animal in animalsInPen) animal.Feed();
    }

    public void RemoveAnimal(Animal animal)
    {
        if (animalsInPen.Remove(animal))
        {
            GameObject.Destroy(animal.gameObject);
        }
    }

    public List<Animal> GetAllAnimals()
    {
        return new List<Animal>(animalsInPen);
    }
}