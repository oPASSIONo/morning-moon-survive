using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Inventory/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private List<RequiredIngredient> requiredIngredients = new List<RequiredIngredient>();
    [SerializeField] private ItemSO craftedItem;
    [SerializeField] private int craftedQuantity;

    public List<RequiredIngredient> RequiredIngredients => requiredIngredients;
    public ItemSO CraftedItem => craftedItem;
    public int CraftedQuantity => craftedQuantity;
}

[System.Serializable]
public class RequiredIngredient
{
    public ItemSO item;
    public int quantity;
}

