using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int HotBarSize { get; private set; } = 10;
        [field: SerializeField] public int Size { get; private set; } = 20;
        

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
            
            
        }
        
        
        public int AddItem(ItemSO item,int quantity,List<ItemParameter>itemState=null)
        {
            if (!item.IsStackable)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && !IsInventoryFull())
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1,itemState);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity,List<ItemParameter>itemState=null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item, quantity = quantity,itemState = new List<ItemParameter>(itemState==null?item.DefaultParametersList:itemState)
            };
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }

            return 0;
        }

        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;
        
        public bool InventoryIsFull()
        {
            return IsInventoryFull();
        }
        
        

        

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].IsEmpty)
                    continue;
                if (inventoryItems[i].item.ID==item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;
                    if (quantity>amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while (quantity>0&&!IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }
        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count>itemIndex)
            {
                if(inventoryItems[itemIndex].IsEmpty)
                    return;
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                InformAboutChange();
            }
        }

        public void DropItemToWorld(int itemIndex,Vector3 position)
        {
            Vector3 dropOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Instantiate(inventoryItems[itemIndex].item.ItemPrefab,position+dropOffset, Quaternion.identity);
            //Debug.Log(inventoryItems[itemIndex].item.name);
        }
        
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item,item.quantity);
        }
        

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
        public int GetItemIndex(ItemSO item)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (!inventoryItems[i].IsEmpty && inventoryItems[i].item == item)
                {
                    return i;
                }
            }
            return -1; // Item not found
        }

    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = item, quantity = newQuantity,itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null, quantity = 0,itemState = new List<ItemParameter>()
        };
    }
}
