using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO,IDestroyableItem,IItemAction
    {
        public string ActionName => "Equip";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character,List<ItemParameter>itemState)
        {
            AgentEquipment equipmentSystem = character.GetComponent<AgentEquipment>();
            if (equipmentSystem!=null)
            {
                Debug.Log("Equip Equipment");
                equipmentSystem.SetEquipment(this,itemState==null? DefaultParametersList : itemState);
                return true;
            }

            return false;
        }
    }
}

