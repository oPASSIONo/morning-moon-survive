using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftingPage : MonoBehaviour
{
    [SerializeField] private UICraftingItem craftingItemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UICraftingDescription craftingDescription;

    private List<UICraftingItem> listOfUICraftingItems = new List<UICraftingItem>();

    public void PopulateCraftingUI(CraftingSO craftingSO)
    {
        ClearCraftingUI();
        foreach (Recipe recipe in craftingSO.recipes)
        {
            UICraftingItem craftingItem = Instantiate(craftingItemPrefab, contentPanel);
            craftingItem.SetData(recipe.CraftedItem.ItemImage,recipe.CraftedItem.Name); // Example: SetData method to display icon
            craftingItem.OnItemClicked += OnCraftingItemClicked;
            listOfUICraftingItems.Add(craftingItem);
        }
    }

    private void ClearCraftingUI()
    {
        foreach (var item in listOfUICraftingItems)
        {
            Destroy(item.gameObject);
        }
        listOfUICraftingItems.Clear();
    }

    private void OnCraftingItemClicked(UICraftingItem item)
    {
        // Handle crafting item click
        Debug.Log("Crafting item clicked: " + item.name);
    }
}