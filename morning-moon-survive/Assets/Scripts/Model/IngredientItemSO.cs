using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class IngredientItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "CRAFT";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState,int amount)
        {
            throw new System.NotImplementedException();
        }
    }
}