using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler,IDropHandler,IDragHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;

    [SerializeField] private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,OnRightMouseBtnClick;
    private bool empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityTxt.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    // Implement the OnBeginDrag method required by IBeginDragHandler interface
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        OnItemBeginDrag?.Invoke(this);
    }

    // Implement the OnDrag method required by IDragHandler interface
    public void OnDrag(PointerEventData eventData)
    {
        // Your implementation here
    }

    // Implement the OnDrop method required by IDropHandler interface
    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    // Implement the OnEndDrag method required by IEndDragHandler interface
    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    // Implement the OnPointerClick method required by IPointerClickHandler interface
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button==PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
