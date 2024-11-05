using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TMP_Text itemName;
    [SerializeField] protected Image borderImage;

    protected bool empty = true;

    public event Action<UIItem> OnItemClicked;
    public event Action<UIItem> OnRightMouseBtnClick;

    protected virtual void Awake()
    {
        ResetData();
        Deselect();
    }

    public virtual void SetData(Sprite sprite, string name)
    {
        itemImage.sprite = sprite;
        itemName.text = name;
        empty = false;
    }

    public virtual void ResetData()
    {
        itemImage.sprite = null;
        itemName.text = "";
        empty = true;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public virtual void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }
}