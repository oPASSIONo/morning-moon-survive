using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string ItemAbility { get; set; }

        [field: SerializeField] public Sprite ItemImage { get; set; }
        [field: SerializeField] public string ItemCategory { get; set; }
        [field: SerializeField] public string ItemSubCategory { get; set; }
        [field: SerializeField] public string ItemRarity { get; set; }
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
}

