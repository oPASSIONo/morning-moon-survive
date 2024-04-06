using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory.Model;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UIInventoryPage inventoryUI;
        [SerializeField] private InventorySO inventoryData;
        public List<InventoryItem> initialItems = new List<InventoryItem>();
    
        private PlayerInput playerInput;
        private InputAction openInventoryAction;

        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;
    
    
        void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            playerInput = GetComponent<PlayerInput>();
            openInventoryAction = playerInput.actions.FindAction("Inventory");
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key,item.Value.item.ItemImage,item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDraggin;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            
            IItemAction itemAction=inventoryItem.item as IItemAction;
            if (itemAction!=null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName,()=>PerformAction(itemIndex));
                
            }
            IDestroyableItem destroyableItem=inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop",()=>DropItem(itemIndex,inventoryItem.quantity));
            }
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IDestroyableItem destroyableItem=inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex,1);
            }
            IItemAction itemAction=inventoryItem.item as IItemAction;
            if (itemAction!=null)
            {
                Debug.Log("Use Item");
                itemAction.PerformAction(gameObject,inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                /*if(inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();*/
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            Debug.Log("Drop Item");
            inventoryData.RemoveItem(itemIndex,quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }
    
        private void HandleDraggin(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if(inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage,inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex,item.ItemImage,item.name,description,item.ItemCategory,item.ItemRarity);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.ItemAbility);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName}" +
                          $": {inventoryItem.itemState[i].value} / "+
                          $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        void Update()
        {
            if (openInventoryAction.triggered)
            {
                OpenInventoryUI();
            }
        }

        void OpenInventoryUI()
        {
            inventoryUI.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,item.Value.item.ItemImage,item.Value.quantity);
            }
        }

    }
}