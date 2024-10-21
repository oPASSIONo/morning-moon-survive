using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class UIBuildingPage : MonoBehaviour
{
    [SerializeField] private UIBuildingItem buildingItemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIBuildingDescription buildingDescription;
    
    [SerializeField] private PlacementSystem placementSystem; // Reference to PlacementSystem
    private ObjectData selectedObjectData; // Store the selected ObjectData
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerStateManager playerStateManager;
    [SerializeField] private PreviewSystem previewSystem;
    
    private List<UIBuildingItem> listOfUIBuildingItems = new List<UIBuildingItem>();
    private Dictionary<UIBuildingItem, ObjectData> buildingItemToRecipeMap = new Dictionary<UIBuildingItem, ObjectData>();

    public void PopulateBuildingUI(BuildingObjectSo so)
    {
        ClearBuildingUI();
        foreach (ObjectData data in so.objectsData)
        {
            UIBuildingItem buildingItem = Instantiate(buildingItemPrefab, contentPanel);
            buildingItem.SetData(data.Image , data.Name);
            buildingItem.OnBuildItemClicked += OnBuildingItemClicked;
            buildingItemToRecipeMap[buildingItem] = data;
            listOfUIBuildingItems.Add(buildingItem);
        }
    }

    private void ClearBuildingUI()
    {
        foreach (var item in listOfUIBuildingItems)
        {
            Destroy(item.gameObject);
        }
        listOfUIBuildingItems.Clear();
        buildingItemToRecipeMap.Clear();
    }

    private void OnBuildingItemClicked(UIBuildingItem item)
    {
        if (buildingItemToRecipeMap.TryGetValue(item, out ObjectData obj))
        {
            Debug.Log("Building item clicked : " + obj.Name);
       
            selectedObjectData = obj; // Store the selected ObjectData
            SetDescription(obj);
        }
        else
        {
            Debug.LogError("Object not found for the clicked item.");
        }
    }
    
   private void SetDescription(ObjectData data) 
   {
        buildingDescription.SetDescription(data.Image , data.Name , data.Description , data.Recipe.RequiredIngredients);
   }
   
   public void OnBuildButtonClicked()
   {
       /*
       if (selectedObjectData != null)
       {
           placementSystem.StartPlacement(selectedObjectData.ID); // Use selectedObjectData's ID

           playerStateManager.SetState(PlayerStateManager.PlayerState.Building);
           inventoryController.OpenInventoryUI();
       }
       */
       
       if (selectedObjectData != null)
       {
           // Check if the required ingredients are available
           if (inventoryController.HasEnoughIngredients(selectedObjectData.Recipe.RequiredIngredients))
           {
               placementSystem.StartPlacement(selectedObjectData.ID); // Use selectedObjectData's ID
               playerStateManager.SetState(PlayerStateManager.PlayerState.Building);
               inventoryController.OpenInventoryUI();
               Debug.Log("Building item placed successfully! " +  selectedObjectData.Name);

           }
           else
           {
               // Not enough ingredients, start placement but change preview to indicate issue
               placementSystem.StartPlacement(selectedObjectData.ID); 
               playerStateManager.SetState(PlayerStateManager.PlayerState.Building);
               inventoryController.OpenInventoryUI();
               /*previewSystem.ChangePreviewColor(Color.red); // Change preview color to red
               previewSystem.UpdatePosition(previewSystem.GetCurrentPosition(), false); */
               Debug.Log("Not enough ingredients to place the item!" + selectedObjectData.Name);
           }
       }
       else
       {
           Debug.LogError("No building item selected!");
       }
   }

}
