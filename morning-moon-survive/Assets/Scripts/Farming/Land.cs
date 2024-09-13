using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Land : MonoBehaviour
{
    public enum LandStatus
    {
        Soil,Farmland,Watered
    }
    
    public LandStatus landStatus { get; private set; }
    private new Renderer renderer;
    [SerializeField] private Material soilMat,farmlandMat, wateredMat;

    [SerializeField] private GameObject select;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch=soilMat;
        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                materialToSwitch = soilMat;
                break;
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

    public void Interact(ToolItemSO toolItemSo)
    {
        switch (toolItemSo.ItemSubCategory)
        {
            case ItemSubCategory.Watering:
                SwitchLandStatus(LandStatus.Watered);
                Debug.Log("Watered");
                break;
            case ItemSubCategory.Dig:
                SwitchLandStatus(LandStatus.Farmland);
                Debug.Log("Dig");
                break;
        }
    }
}
