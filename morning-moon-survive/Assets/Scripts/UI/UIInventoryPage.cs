using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        public static UIInventoryPage Instance { get; private set; }
        
        [SerializeField] private UIInventoryItem itemPrefab;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private UIInventoryDescription itemDescription;
        [SerializeField] private MouseFollower mouseFollower;
        private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();
        [SerializeField] private RectTransform hotbarPanel;

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;
        public ItemActionPanel actionPanel;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button nextButton;

        private int indexOfSelectingItem;

        private void Awake()
        {
            Instance = this;
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void InitializeInventoryUI(int inventorysize,int hotbarsize)
        {
            
            for (int i = 0; i < 10; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(hotbarPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
            
            for (int i = 0; i < inventorysize-hotbarsize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
            
        }
        
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count>itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage,itemQuantity);
            }
        }
        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            indexOfSelectingItem = index;
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }
                
        public void OnNextButtonClicked()
        {
            do
            {
                indexOfSelectingItem++;
            } while (indexOfSelectingItem < listOfUIItems.Count && listOfUIItems[indexOfSelectingItem].IsEmpty);

            if (indexOfSelectingItem >= listOfUIItems.Count)
            {
                indexOfSelectingItem = listOfUIItems.Count - 1;
            }
            
            HandleShowItemActions(listOfUIItems[indexOfSelectingItem]);
            Debug.Log(indexOfSelectingItem);
        }

        public void OnPreviousButtonClicked()
        {
            do
            {
                indexOfSelectingItem--;
            } while (indexOfSelectingItem >= 0 && listOfUIItems[indexOfSelectingItem].IsEmpty);

            if (indexOfSelectingItem < 0)
            {
                indexOfSelectingItem = 0;
            }

            HandleShowItemActions(listOfUIItems[indexOfSelectingItem]);
            Debug.Log(indexOfSelectingItem);
        }


        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex,index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if(index==-1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if(index==-1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }
        
        // Define a public method to expose the functionality
        public void HandleItemSelectionExternally(UIInventoryItem inventoryItemUI)
        {
            HandleItemSelection(inventoryItemUI); // Call the private method internally
        }

        public void Show()
        {
            if(!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                ResetSelection();
            }
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName,performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            HandleItemSelection(listOfUIItems[itemIndex]);
            actionPanel.Toggle(true);
            //actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }
        
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }
        public void Hide()
        {
            actionPanel.Toggle(false);
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string ability,string category,string rarity)
        {
            //actionPanel.dropBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            //actionPanel.useBtn.GetComponent<Button>().onClick.RemoveAllListeners();
            itemDescription.SetDescription(itemImage,name,ability,category,rarity);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfUIItems)
            {
               item.ResetData();
               item.Deselect();
            }
        }

        
    }
}