using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public class AgentEquipment : MonoBehaviour
{
    [SerializeField] private EquippableItemSO equipment;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemParameter> parameterToModify, itemCurrentState;
    [SerializeField] private UIEquipmentPage equipUI;

    public void SetEquipment(EquippableItemSO equipmentItemSO, List<ItemParameter> itemState)
    {
        if (equipment!=null)
        {
            inventoryData.AddItem(equipment, 1, itemCurrentState);
        }

        this.equipment = equipmentItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
        equipUI.SetEquipImage(equipmentItemSO.ItemImage);
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parameterToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter()
                {
                    itemParameter = parameter.itemParameter, value = newValue
                };
            }
        }
    }
}
