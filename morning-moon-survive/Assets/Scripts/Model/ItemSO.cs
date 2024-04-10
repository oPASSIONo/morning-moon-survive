using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string ItemAbility { get; set; }

        [field: SerializeField] public Sprite ItemImage { get; set; }
        [field: SerializeField] public ItemCategory ItemCategory { get; set; }
        [field: SerializeField] public string ItemSubCategory { get; set; }
        [field: SerializeField] public ItemRarity ItemRarity { get; set; }
        [field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }
        
        [field: SerializeField] public GameObject ItemPrefab { get; set; }
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
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

