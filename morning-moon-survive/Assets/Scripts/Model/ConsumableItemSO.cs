/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ConsumableItemSO : ItemSO,IDestroyableItem,IItemAction
    {
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
        public string ActionName => "Consume";
        [field: SerializeField] public AudioClip actionSFX {get;private set; }

        public bool PerformAction(GameObject character,List<ItemParameter>itemState,int amount)
        {
            itemState = null;
            foreach (ModifierData data in modifierData)
            {
                for (int i = 0; i < amount; i++)
                {
                    data.statModifier.AffectCharacter(character, data.value);
                    Debug.Log("Consume Item");
                }
            }

            return true;
        }

    }

    public interface IDestroyableItem
    {
        
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character,List<ItemParameter>itemState,int amount);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}*/