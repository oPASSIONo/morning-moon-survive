using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum GrowthStage
    {
        Small,
        Medium,
        Large,
        Full,
        Rot
    }

    private SeedItemSO seedData;
    private GrowthStage currentStage;
    private int daysPlanted;
    private int wateredDaysForCurrentStage;  
    private int daysWithoutWater;  // Track consecutive days without watering
    private Land land; 

    public void Initialize(SeedItemSO seedItem, Land plantedLand)
    {
        seedData = seedItem;
        currentStage = GrowthStage.Small;
        wateredDaysForCurrentStage = 0;
        daysWithoutWater = 0; // Initialize the counter
        land = plantedLand;

        // Subscribe to the day start event
        TimeManager.Instance.OnDayStart.AddListener(UpdateGrowth);
        UpdatePlantVisual();
    }

    private void OnDestroy()
    {
        TimeManager.Instance?.OnDayStart.RemoveListener(UpdateGrowth);
    }

    private void UpdateGrowth()
    {
        if (land != null && land.IsWatered)
        {
            wateredDaysForCurrentStage++;
            daysWithoutWater = 0; // Reset days without water when watered
            Debug.Log($"Plant watered for {wateredDaysForCurrentStage} day(s) in stage {currentStage}.");
            CheckGrowthStage();
        }
        else
        {
            daysWithoutWater++;
            Debug.Log($"Plant has not been watered for {daysWithoutWater} day(s).");
            CheckForRot();
            
        }
        land.SetWatered(false);
    }
    private void CheckForRot()
    {
        if (daysWithoutWater >= seedData.rotTime)
        {
            currentStage = GrowthStage.Rot; // Change to rot state
            UpdatePlantVisual();
        }
    }
    private void CheckGrowthStage()
    {
        switch (currentStage)
        {
            case GrowthStage.Small:
                if (wateredDaysForCurrentStage >= seedData.daysToMedium)
                {
                    currentStage = GrowthStage.Medium;
                    wateredDaysForCurrentStage = 0;
                    UpdatePlantVisual();
                }
                break;
            case GrowthStage.Medium:
                if (wateredDaysForCurrentStage >= seedData.daysToLarge)
                {
                    currentStage = GrowthStage.Large;
                    wateredDaysForCurrentStage = 0;
                    UpdatePlantVisual();
                }
                break;
            case GrowthStage.Large:
                if (wateredDaysForCurrentStage >= seedData.daysToFull)
                {
                    currentStage = GrowthStage.Full;
                    wateredDaysForCurrentStage = 0;
                    UpdatePlantVisual();
                }
                break;
            case GrowthStage.Full:
                UpdatePlantVisual();
                break;
            case GrowthStage.Rot:
                // Handle any special logic when the plant is rotted
                Debug.Log("Plant has rotted and cannot grow.");
                break;
        }
    }

    private void UpdatePlantVisual()
    {
        switch (currentStage)
        {
            case GrowthStage.Medium:
                
                Debug.Log("Plant is now Medium.");
                break;
            case GrowthStage.Large:
                
                Debug.Log("Plant is now Large.");
                break;
            case GrowthStage.Full:
                
                Debug.Log("Plant is now Full.");
                break;
            case GrowthStage.Rot:
                Debug.Log("Plant has rotted.");
                // Optionally, you can change the visual representation of the plant to show it's rotted
                break;
        }
    }
}
