using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public enum EquipmentType
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
    
    private Dictionary<EquipmentType, EquippableItemSO> equippedItems = new Dictionary<EquipmentType, EquippableItemSO>();


    public void SetEquipment(EquipmentType type, EquippableItemSO equipmentItemSO, List<ItemParameter> itemState)
    {
        // Unequip the existing equipment before equipping the new one
        if (equippedItems.ContainsKey(type))
        {
            inventoryData.AddItem(equippedItems[type], 1, itemCurrentState);
        }

        // Equip the new item
        equippedItems[type] = equipmentItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        //ModifyParameters();
        
        equipUI.SetEquipImage(type, equipmentItemSO.ItemImage);
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

    private List<ItemParameter> GetItemState(EquipmentType type)
    {
        // Implement logic to retrieve the current item state based on the slot
        // For example, you can store item states in a dictionary or use separate variables/lists for each slot
        // Return the item state for the specified slot
        return new List<ItemParameter>(); // Placeholder return statement
    }

    private List<ItemParameter> GetParametersToModify(EquipmentType type)
    {
        // Implement logic to determine which parameters need to be modified based on the slot
        // You can define this per slot or have a common set of parameters to modify for all slots
        // Return the parameters to modify for the specified slot
        return new List<ItemParameter>(); // Placeholder return statement
    }
}
