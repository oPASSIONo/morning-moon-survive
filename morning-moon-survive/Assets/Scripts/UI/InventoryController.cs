using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory.Model;
using Unity.Netcode;

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

        [SerializeField] private AmountController amountController;
        
        private void Awake()
        {
            playerInput = new PlayerInput();
            playerInput.PlayerControls.Enable();
            
            //openInventoryAction = playerInput.PlayerControls.Inventory;

        }

        
        void Start()
        {
            PrepareUI();
            PrepareInventoryData();

            GameInput.Instance.OnInventoryAction += GameInput_OnInventoryAction;
        }
        
        private void GameInput_OnInventoryAction(object sender, EventArgs e)
        {
            OpenInventoryUI();
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
            inventoryUI.InitializeInventoryUI(inventoryData.Size,inventoryData.HotBarSize);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDraggin;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        /*private void OnDestroy()
        {
            inventoryUI.OnDescriptionRequested -= HandleDescriptionRequest;
            inventoryUI.OnSwapItems -= HandleSwapItems;
            inventoryUI.OnStartDragging -= HandleDraggin;
            inventoryUI.OnItemActionRequested -= HandleItemActionRequest;
        }*/
        

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(itemAction != null)
            {
                
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex,amountController.InputToAmount()));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("DROP", () => DropItem(itemIndex, amountController.InputToAmount()));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            Debug.Log("Drop Item");
            inventoryData.DropItemToWorld(itemIndex,transform.position,quantity);
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
            inventoryUI.actionPanel.Toggle(false);
        }

        public void PerformAction(int itemIndex,int quantity)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null && !(inventoryItem.item is ToolItemSO) && !(inventoryItem.item is IngredientItemSO))
            {
                inventoryData.RemoveItem(itemIndex, quantity);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState,quantity);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
            inventoryUI.actionPanel.Toggle(false);
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

        
        private void HandleDescriptionRequest(int itemIndex)//Selecting Item
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex,item.ItemImage,item.name,description,item.ItemCategory.ToString(),item.ItemRarity.ToString());
            
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
        

        void OpenInventoryUI()
        {
            inventoryUI.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,item.Value.item.ItemImage,item.Value.quantity);
            }
        }
        
        
        /*public InventorySO GetInventoryData()
        {
            return inventoryData;
        }*/
        
        
        private void OnEnable()
        {
            // Subscribe to hotbar selection actions
            playerInput.FindAction("SelectSlot1").performed += ctx => SelectSlot(1);
            playerInput.FindAction("SelectSlot2").performed += ctx => SelectSlot(2);
            playerInput.FindAction("SelectSlot3").performed += ctx => SelectSlot(3);
            playerInput.FindAction("SelectSlot4").performed += ctx => SelectSlot(4);
            playerInput.FindAction("SelectSlot5").performed += ctx => SelectSlot(5);
            playerInput.FindAction("SelectSlot6").performed += ctx => SelectSlot(6);
            playerInput.FindAction("SelectSlot7").performed += ctx => SelectSlot(7);
            playerInput.FindAction("SelectSlot8").performed += ctx => SelectSlot(8);
            playerInput.FindAction("SelectSlot9").performed += ctx => SelectSlot(9);
            playerInput.FindAction("SelectSlot10").performed += ctx => SelectSlot(10);

        }
        
        
        private void OnDisable()
        {
            // Unsubscribe from hotbar selection actions
            playerInput.FindAction("SelectSlot1").performed -= ctx => SelectSlot(1);
            playerInput.FindAction("SelectSlot2").performed -= ctx => SelectSlot(2);
            playerInput.FindAction("SelectSlot3").performed -= ctx => SelectSlot(3);
            playerInput.FindAction("SelectSlot4").performed -= ctx => SelectSlot(4);
            playerInput.FindAction("SelectSlot5").performed -= ctx => SelectSlot(5);
            playerInput.FindAction("SelectSlot6").performed -= ctx => SelectSlot(6);
            playerInput.FindAction("SelectSlot7").performed -= ctx => SelectSlot(7);
            playerInput.FindAction("SelectSlot8").performed -= ctx => SelectSlot(8);
            playerInput.FindAction("SelectSlot9").performed -= ctx => SelectSlot(9);
            playerInput.FindAction("SelectSlot10").performed -= ctx => SelectSlot(10);
        }
        private void SelectSlot(int slot)
        {
            Debug.Log("Selected slot " + slot);
            PerformAction(slot-1,1);
            /*InventoryItem inventoryItem = inventoryData.GetItemAt(slot - 1);
            inventoryUI.HandleItemSelectionExternally(inventoryItem);*/
            HandleDescriptionRequest(slot-1);
        }
        
    }
}