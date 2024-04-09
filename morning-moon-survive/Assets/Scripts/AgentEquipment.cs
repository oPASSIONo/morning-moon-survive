using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public enum EquipmentSlot
{
    Garment,
    Accessory,
    Tool,
    // Add more slots as needed
}

public class AgentEquipment : MonoBehaviour
{
    [SerializeField] private EquippableItemSO equipment;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemParameter> parameterToModify, itemCurrentState;
    [SerializeField] private UIEquipmentPage equipUI;
    
    private Dictionary<EquipmentSlot, EquippableItemSO> equippedItems = new Dictionary<EquipmentSlot, EquippableItemSO>();


    /*public void SetEquipment(EquippableItemSO equipmentItemSO, List<ItemParameter> itemState)
    {
        if (equipment!=null)
        {
            inventoryData.AddItem(equipment, 1, itemCurrentState);
        }

        this.equipment = equipmentItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
        equipUI.SetEquipImage(equipmentItemSO.ItemImage);
    }*/
    
    public void SetEquipment(EquipmentSlot slot, EquippableItemSO equipmentItemSO, List<ItemParameter> itemState)
    {
        // Unequip the existing equipment before equipping the new one
        if (equippedItems.ContainsKey(slot))
        {
            inventoryData.AddItem(equippedItems[slot], 1, itemCurrentState);
        }

        // Equip the new item
        equippedItems[slot] = equipmentItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
        equipUI.SetEquipImage(equipmentItemSO.ItemImage);
    }


    /*private void ModifyParameters()
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
    }*/
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

    private List<ItemParameter> GetItemState(EquipmentSlot slot)
    {
        // Implement logic to retrieve the current item state based on the slot
        // For example, you can store item states in a dictionary or use separate variables/lists for each slot
        // Return the item state for the specified slot
        return new List<ItemParameter>(); // Placeholder return statement
    }

    private List<ItemParameter> GetParametersToModify(EquipmentSlot slot)
    {
        // Implement logic to determine which parameters need to be modified based on the slot
        // You can define this per slot or have a common set of parameters to modify for all slots
        // Return the parameters to modify for the specified slot
        return new List<ItemParameter>(); // Placeholder return statement
    }
}
