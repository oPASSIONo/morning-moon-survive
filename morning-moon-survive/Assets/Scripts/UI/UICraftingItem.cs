using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICraftingItem : UIItem/*MonoBehaviour,IPointerClickHandler*/
{
    protected override void Awake()
    {
        base.Awake();
        // Any additional initialization specific to UICraftingItem
    }
    /*[SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;

    //[SerializeField] private Image borderImage;
    private bool empty = true;


    public event Action<UICraftingItem> OnItemClicked,OnRightMouseBtnClick; 
    public void Awake()
    {
        ResetData();
        //Deselect();
    }
    public void ResetData()
    {
        //itemImage.gameObject.SetActive(false);
        empty = true;
    }
    /*public void Select()
    {
        borderImage.enabled = true;
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }#1#

    public void SetData(Sprite sprite,string name)
    {
        //itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        itemName.text = name;
        empty = false;
    }

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
    }*/
}
