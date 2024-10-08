using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingPage : MonoBehaviour
{
    [SerializeField] private UIBuildingItem buildingItemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIBuildingDescription buildingDescription;

    private List<UIBuildingItem> listOfUIBuildingItems = new List<UIBuildingItem>();
    private Dictionary<UIBuildingItem, ObjectData> buildingItemToRecipeMap = new Dictionary<UIBuildingItem, ObjectData>();

    public void PopulateBuildingUI(ObjectDatabaseSO databaseSo)
    {
        ClearBuildingUI();
        foreach (ObjectData data in databaseSo.objectsData)
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
       
            SetDescription(obj);
        }
        else
        {
            Debug.LogError("Object not found for the clicked item.");
        }
    }
    
   private void SetDescription(ObjectData data) 
   {
        buildingDescription.SetDescription(data.Image , data.Name , data.ID , data.Recipe.RequiredIngredients);
   }

}
