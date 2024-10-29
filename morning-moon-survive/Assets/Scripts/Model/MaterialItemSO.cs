using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public enum ItemType
    {
        Consumable,
        Ingredient
    }

    [CreateAssetMenu]
    public class MaterialItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] public ItemType itemType; // Determines if it's consumable or ingredient
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();

        public string ActionName
        {
            get
            {
                return itemType == ItemType.Consumable ? "Consume" : "Craft";
            }
        }

        [field: SerializeField] public AudioClip actionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState, int amount)
        {
            switch (itemType)
            {
                case ItemType.Consumable:
                    return PerformConsumeAction(character, amount);
                case ItemType.Ingredient:
                    return PerformCraftAction(character);
                default:
                    Debug.LogError("Invalid Item Type");
                    return false;
            }
        }

        // Logic for consumable action (e.g., health potions, food, etc.)
        private bool PerformConsumeAction(GameObject character, int amount)
        {
            foreach (ModifierData data in modifierData)
            {
                for (int i = 0; i < amount; i++)
                {
                    data.statModifier.AffectCharacter(character, data.value);
                    Debug.Log("Consumed item and applied modifier.");
                }
            }
            return true;
        }

        // Logic for ingredient action (e.g., crafting materials)
        private bool PerformCraftAction(GameObject character)
        {
            // Crafting logic can be implemented here
            Debug.Log("Item used for crafting.");
            return true;
        }
    }

    // Interfaces moved outside for global access
    public interface IDestroyableItem
    {
        // This can remain empty or be filled with relevant methods if needed
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState, int amount);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}
