using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemPicture;
    public ItemCategory itemCategory;
    public ItemSubCategory ItemSubCategory;
    
    public ItemRarity itemRarity;
    public string itemAbility;
}
public enum ItemCategory
{
    Cosmetics,
    Equipment,
    Consumable,
    Treasure,
    ResourceAndFarm,
    Fishing
    // Add more categories as needed
}
public enum ItemSubCategory
{
    Fruit,
    Potion,
    Food
    // Add more sub-categories as needed
}

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legend,
    Relic
    // Add more rarities as needed
}

