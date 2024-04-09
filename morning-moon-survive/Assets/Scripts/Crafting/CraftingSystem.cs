using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    [SerializeField] private List<Recipe> recipes = new List<Recipe>();
    [SerializeField] private InventorySO playerInventory;

    public void CraftItem(Recipe recipe)
    {
        if (recipe == null)
        {
            Debug.LogWarning("Recipe is null.");
            return;
        }

        // Check if the player has all required ingredients
        foreach (var ingredient in recipe.RequiredIngredients)
        {
            if (!PlayerHasIngredient(ingredient.item, ingredient.quantity))
            {
                Debug.LogWarning("Player doesn't have enough ingredients to craft this item.");
                return;
            }
        }

        // Check if the inventory is full before crafting the item
        if (playerInventory.InventoryIsFull())
        {
            Debug.LogWarning("Inventory is full. Cannot craft item.");
            return;
        }


        // Craft the item
        AddCraftedItemToInventory(recipe.CraftedItem, recipe.CraftedQuantity);

        // Remove consumed ingredients from player's inventory
        ConsumeIngredients(recipe.RequiredIngredients);
    }


    private bool PlayerHasIngredient(ItemSO item, int quantity)
    {
        int itemIndex = playerInventory.GetItemIndex(item);
        if (itemIndex != -1)
        {
            InventoryItem inventoryItem = playerInventory.GetItemAt(itemIndex);
            return inventoryItem.quantity >= quantity;
        }
        return false;
    }

    private void AddCraftedItemToInventory(ItemSO item, int quantity)
    {
        playerInventory.AddItem(item, quantity);
    }

    private void ConsumeIngredients(List<RequiredIngredient> ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            int itemIndex = playerInventory.GetItemIndex(ingredient.item);
            if (itemIndex != -1)
            {
                playerInventory.RemoveItem(itemIndex, ingredient.quantity);
            }
        }
    }
    


}
