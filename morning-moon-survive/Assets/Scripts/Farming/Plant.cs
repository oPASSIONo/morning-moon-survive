using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum GrowthStage
    {
        Seed,
        Small,
        Medium,
        Large
    }

    private SeedItemSO seedData;
    private GrowthStage currentStage;
    private int daysPlanted;
    private int wateredDaysForCurrentStage;  
    private Land land; 

    public void Initialize(SeedItemSO seedItem, Land plantedLand)
    {
        seedData = seedItem;
        currentStage = GrowthStage.Seed;
        wateredDaysForCurrentStage = 0;
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
            Debug.Log($"Plant watered for {wateredDaysForCurrentStage} day(s) in stage {currentStage}.");
            CheckGrowthStage();
        }
        else
        {
            Debug.Log("Plant cannot grow because the land is not watered.");
        }
        land.SetWatered(false);
    }

    private void CheckGrowthStage()
    {
        switch (currentStage)
        {
            case GrowthStage.Seed:
                if (wateredDaysForCurrentStage >= seedData.daysToSmall)
                {
                    currentStage = GrowthStage.Small;
                    wateredDaysForCurrentStage = 0;
                    UpdatePlantVisual();
                }
                break;
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
                UpdatePlantVisual();
                break;
        }
    }

    private void UpdatePlantVisual()
    {
        switch (currentStage)
        {
            case GrowthStage.Small:
                Debug.Log("Plant is now Small.");
                break;
            case GrowthStage.Medium:
                Debug.Log("Plant is now Medium.");
                break;
            case GrowthStage.Large:
                Debug.Log("Plant is now Large.");
                break;
        }
    }
}
