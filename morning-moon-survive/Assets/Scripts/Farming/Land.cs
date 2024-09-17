using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Land : MonoBehaviour
{
    [SerializeField] private List<SeedItemSO> seedsList;
    [SerializeField] private Transform plantingPosition;
    public enum LandStatus
    {
        Farmland,Watered
    }
    
    public LandStatus landStatus { get; private set; }
    private new Renderer renderer;
    [SerializeField] private Material farmlandMat, wateredMat;

    [SerializeField] private GameObject select;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Farmland);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch=farmlandMat;
        switch (statusToSwitch)
        {
            
            case LandStatus.Farmland:
                materialToSwitch = farmlandMat;
                break;
            case LandStatus.Watered:
                materialToSwitch = wateredMat;
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
                if (landStatus==LandStatus.Farmland)
                {
                    SwitchLandStatus(LandStatus.Watered);
                }
                else
                {
                    
                }
                break;
            case ItemSubCategory.Dig:
                if (landStatus==LandStatus.Watered)
                {
                    SwitchLandStatus(LandStatus.Farmland);
                }
                else
                {
                    //dig the plant out
                }
                break;
        }
    }

    private void PlantingSeed(SeedItemSO seedItemSo)
    {
        Instantiate(seedItemSo.ItemPrefab, plantingPosition);
    }
}
