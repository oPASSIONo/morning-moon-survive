using UnityEngine;

public class Animal : MonoBehaviour
{
    private AnimalSO animalData;
    private GameObject currentModel;

    private int growthStage = 0; // 0 = baby, 1 = juvenile, 2 = adult
    private int startDay;
    private bool isFedToday = false;

    // Drop item variables
    private int lastDropDay = 0;       // Tracks the last day the item was dropped
    private int adultStartDay = -1;    // Tracks the day the animal became an adult

    public void Initialize(AnimalSO data)
    {
        animalData = data;
        growthStage = 0;
        startDay = TimeManager.Instance.DayCount;

        // Start as a baby
        SetAnimalModel(animalData.babyPrefab);

        // Listen to the start of each day for growth and feeding checks
        TimeManager.Instance.OnDayStart.AddListener(OnNewDay);
    }

    private void OnDestroy()
    {
        TimeManager.Instance.OnDayStart.RemoveListener(OnNewDay);
    }

    private void SetAnimalModel(GameObject newModel)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }
        currentModel = Instantiate(newModel, transform.position, transform.rotation, transform);
    }

    private void OnNewDay()
    {
        if (!isFedToday)
        {
            Die();
            return;
        }

        isFedToday = false;
        CheckGrowth();

        // If the animal is an adult and can drop items, check for item drop
        if (growthStage == 2 /*&& animalData.canDropItems*/)
        {
            CheckItemDrop();
        }
    }

    private void CheckGrowth()
    {
        int currentDay = TimeManager.Instance.DayCount;

        if (growthStage == 0 && currentDay >= startDay + animalData.babyToJuvenileTime)
        {
            growthStage = 1;
            SetAnimalModel(animalData.juvenilePrefab);
            Debug.Log($"{animalData.speciesName} has grown to juvenile.");
        }
        else if (growthStage == 1 && currentDay >= startDay + animalData.babyToJuvenileTime + animalData.juvenileToAdultTime)
        {
            growthStage = 2;
            adultStartDay = currentDay;  // Start counting from the day it becomes an adult
            SetAnimalModel(animalData.adultPrefab);
            Debug.Log($"{animalData.speciesName} has grown to adult.");
        }
    }

    public void Feed()
    {
        if (!isFedToday)
        {
            isFedToday = true;
            Debug.Log($"{animalData.speciesName} has been fed.");
        }
    }

    private void CheckItemDrop()
    {
        int currentDay = TimeManager.Instance.DayCount;

        // First, check if the animal has been an adult long enough to drop its first item
        if (adultStartDay != -1 && currentDay >= adultStartDay + animalData.daysAfterAdultBeforeFirstDrop)
        {
            // If the first drop has occurred, start regular drops based on the interval
            if (currentDay >= lastDropDay + animalData.daysBetweenDrops)
            {
                DropItem();
                lastDropDay = currentDay;
            }
        }
    }

    private void DropItem()
    {
        if (animalData.dropItem != null)
        {
            Debug.Log($"{animalData.speciesName} dropped a {animalData.dropItem.Name}!");
            // Add logic to actually spawn the item in the game world here, e.g., 
             Instantiate(animalData.dropItem.ItemPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Die()
    {
        Debug.Log($"{animalData.speciesName} has died.");
        Destroy(gameObject);
    }
}
