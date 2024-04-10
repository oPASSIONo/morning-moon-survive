using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO,IDestroyableItem,IItemAction
    {
        [FormerlySerializedAs("slot")] public EquipmentType type; // Add a field to specify the equipment slot
        public string ActionName => "EQUIP";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character,List<ItemParameter>itemState)
        {
            AgentEquipment equipmentSystem = character.GetComponent<AgentEquipment>();
            if (equipmentSystem!=null)
            {
                Debug.Log("Equip Equipment");
                equipmentSystem.SetEquipment(type,this,itemState==null? DefaultParametersList : itemState);
                return true;
            }

            return false;
        }
    }
}

