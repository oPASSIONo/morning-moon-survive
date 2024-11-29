using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory.Model;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public UIInventoryPage inventoryUI;
        [SerializeField] private InventorySO inventoryData;
        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AmountController amountController;

        private int currentItemIndex;
        public bool cheatMode = false; // Add this variable

        
        void Start()
        {
            PrepareUI();
            PrepareInventoryData();

            GameInput.Instance.OnInventoryAction += GameInput_OnInventoryAction;
            GameInput.Instance.OnSelectSlotAction += HandleSelectSlotAction;
            Land.OnSeedPlanted += HandleSeedPlanted;

        }

        public InventorySO GetInventoryData()
        {
            return inventoryData;
        }
        private void HandleSeedPlanted(SeedItemSO seedItem)
        {
            inventoryData.RemoveItem(currentItemIndex,1);
            InventoryItem inventoryItem = inventoryData.GetItemAt(currentItemIndex);
            if (inventoryItem.IsEmpty)
            {
                GetComponent<AgentTool>().DeactivateAllSeeds();
            }
        }
        private void HandleSelectSlotAction(object sender, int slotIndex)
        {
            // Ensure the slotIndex is within the bounds of the inventory
            if (slotIndex >= 0)
            {
                PerformAction(slotIndex, 1); // Assuming quantity 1 for this example
            }
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
            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is null. Please assign it in the inspector.");
                return;
            }
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);


            if (inventoryItem.IsEmpty)
            {
                Debug.LogWarning("Inventory item is null or empty.");
                return;
            }

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
            currentItemIndex = itemIndex;
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null && inventoryItem.item is MaterialItemSO materialItemSo/* !(inventoryItem.item is ToolItemSO) && !(inventoryItem.item is IngredientItemSO)*/)
            {
                if (materialItemSo.itemType==ItemType.Consumable)
                {
                    inventoryData.RemoveItem(itemIndex, quantity);
                }
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
        

        public void OpenInventoryUI()
        {
            switch (PlayerStateManager.Instance.currentState)
            {
                case PlayerStateManager.PlayerState.Inventory:
                    inventoryUI.Show(true);
                    inventoryUI.MoveHotbarPanel(true);
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key,item.Value.item.ItemImage,item.Value.quantity);
                    }
                    break;
                case PlayerStateManager.PlayerState.Building:
                    Debug.Log("In State Building");
                    inventoryUI.Show(false);
                    inventoryUI.MoveHotbarPanel(false);
                    break;
                case PlayerStateManager.PlayerState.Normal:
                    inventoryUI.Show(false);
                    inventoryUI.MoveHotbarPanel(false);
                    break;
         
            }
        }
        public bool HasEnoughIngredients(List<RequiredIngredient> requiredIngredients)
        {
            // Bypass the check if cheat mode is enabled
            if (cheatMode)
            {
                Debug.Log("Cheat mode enabled: Ingredients check bypassed.");
                return true;
            }
            
            foreach (var ingredient in requiredIngredients)
            {
                // Check if the player's inventory has enough of each ingredient
                int availableAmount = GetIngredientAmount(ingredient.item); // Implement this method to retrieve the current amount of the ingredient

                if (availableAmount < ingredient.quantity)
                {
                    return false; // Not enough of at least one ingredient
                }
            }
            return true; // All ingredients are sufficient
        }
        
        public int GetIngredientAmount(ItemSO ingredient)
        {
            // Loop through the inventory to find the item
            foreach (var inventoryItem in inventoryData.GetCurrentInventoryState())
            {
                if (inventoryItem.Value.item == ingredient) // Check if the item matches the ingredient
                {
                    return inventoryItem.Value.quantity; // Return the quantity of the ingredient
                }
            }

            // If not found, return 0
            return 0;
        }
        
        public void RemoveIngredients(List<RequiredIngredient> requiredIngredients)
        {
            foreach (var ingredient in requiredIngredients)
            {
                // Assuming you have a method to find the ingredient in the inventory
                int ingredientIndex = GetIngredientIndex(ingredient.item);
                if (ingredientIndex != -1)
                {
                    // Deduct the quantity
                    inventoryData.RemoveItem(ingredientIndex, ingredient.quantity);
                }
            }
        }

        private int GetIngredientIndex(ItemSO item)
        {
            for (int i = 0; i < inventoryData.Size; i++)
            {
                if (inventoryData.GetItemAt(i).item == item)
                {
                    return i; // Return the index of the ingredient
                }
            }
            return -1; // Not found
        }
    }
}