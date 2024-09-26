using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Land : MonoBehaviour
{
    [field: SerializeField] public Transform PlantingPosition { get; private set; }
    private bool isPlanted = false;
    public bool IsWatered { get; private set; } = false;
    public void SetWatered(bool isWatered) => IsWatered = isWatered; 
    public enum LandStatus
    {
        Farmland,
        Watered
    }
    
    public LandStatus landStatus { get; private set; }
    private new Renderer renderer;
    [SerializeField] private Material farmlandMat, wateredMat;

    [SerializeField] private GameObject select;

    private GameObject plantObject;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Farmland);

        // Subscribe to the OnDayEnd event instead of OnDayStart
        TimeManager.Instance.OnDayEnd.AddListener(ResetLandStatus);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        TimeManager.Instance?.OnDayEnd.RemoveListener(ResetLandStatus);
    }

    // Reset land status at the end of the day (after plant checks growth)
    public void ResetLandStatus()
    {
        SwitchLandStatus(LandStatus.Farmland);
        Debug.Log("Land status reset to Farmland at the end of the day.");
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = farmlandMat;

        switch (statusToSwitch)
        {
            case LandStatus.Farmland:
                materialToSwitch = farmlandMat;
                break;
            case LandStatus.Watered:
                materialToSwitch = wateredMat;
                IsWatered = true;
                break;
        }

        renderer.material = materialToSwitch;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    public void Interact(SeedItemSO seedItemSo)
    {
        PlantingSeed(seedItemSo);
    }

    public void Interact(ToolItemSO toolItemSo)
    {
        switch (toolItemSo.ItemSubCategory)
        {
            case ItemSubCategory.Watering:
                if (landStatus == LandStatus.Farmland)
                {
                    SwitchLandStatus(LandStatus.Watered);
                }
                break;
            case ItemSubCategory.Dig:
                if (isPlanted)
                {
                    SwitchLandStatus(LandStatus.Farmland);
                    Dig();
                }
                break;
        }
    }

    private void Dig()
    {
        Plant plantComponent = plantObject.GetComponent<Plant>();
        isPlanted = false;
        SetWatered(false);

        if (plantComponent.CurrentStage!=Plant.GrowthStage.Full)
        {
            plantComponent.OnDig();
        }
    }
    private void PlantingSeed(SeedItemSO seedItemSo)
    {
        if (!isPlanted)
        {
            plantObject = Instantiate(seedItemSo.ItemPrefab, PlantingPosition);
            Plant plantComponent = plantObject.AddComponent<Plant>();
            plantComponent.Initialize(seedItemSo, this); // Pass the Land reference to the plant
            isPlanted = true;
        }
        else
        {
            Debug.Log("Already Planted");
        }
    }
}
