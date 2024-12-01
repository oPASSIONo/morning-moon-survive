using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;



public class UIBuildingItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemName;

    private bool empty = true;
    
    public event Action<UIBuildingItem> OnBuildItemClicked,OnMouseButtonClick; 
    
    public void Awake()
    {
        ResetData();
    }
    public void ResetData()
    {
        empty = true;
    }
    
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
            OnMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnBuildItemClicked?.Invoke(this);
        }
    }
}
