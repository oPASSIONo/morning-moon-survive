using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ToolItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "USE";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState)
        {
            throw new System.NotImplementedException();
        }
    }
}
