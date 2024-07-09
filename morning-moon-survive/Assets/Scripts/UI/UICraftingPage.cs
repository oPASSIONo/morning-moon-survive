using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftingPage : MonoBehaviour
{
    [SerializeField] private UICraftingItem craftingItemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UICraftingDescription craftingDescription;
    [SerializeField] private CraftButtonHandler craftButtonHandler;
    

    private List<UICraftingItem> listOfUICraftingItems = new List<UICraftingItem>();
    private Dictionary<UICraftingItem, Recipe> craftingItemToRecipeMap = new Dictionary<UICraftingItem, Recipe>();

    public void PopulateCraftingUI(CraftingSO craftingSO)
    {
        ClearCraftingUI();
        foreach (Recipe recipe in craftingSO.recipes)
        {
            UICraftingItem craftingItem = Instantiate(craftingItemPrefab, contentPanel);
            craftingItem.SetData(recipe.CraftedItem.ItemImage, recipe.CraftedItem.Name);
            craftingItem.OnItemClicked += OnCraftingItemClicked;
            craftingItemToRecipeMap[craftingItem] = recipe;
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
        craftingItemToRecipeMap.Clear();
    }

    private void OnCraftingItemClicked(UICraftingItem item)
    {
        if (craftingItemToRecipeMap.TryGetValue(item, out Recipe recipe))
        {
            Debug.Log("Crafting item clicked: " + recipe.CraftedItem.Name);
            SetDescription(recipe);
            craftButtonHandler.SetSelectedRecipe(recipe); // Set the selected recipe for crafting
        }
        else
        {
            Debug.LogError("Recipe not found for the clicked item.");
        }
    }

    private void SetDescription(Recipe recipe)
    {
        craftingDescription.SetDescription(
            recipe.CraftedItem.ItemImage,
            recipe.CraftedItem.Name,
            recipe.CraftedItem.ItemAbility,
            recipe.RequiredIngredients
        );
    }
}