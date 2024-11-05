using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAnimalEgg : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Image eggImage;

    [SerializeField] private TMP_Text eggName;

    [SerializeField] private TMP_Text eggQuantity;

    [SerializeField] private Image borderImage;

    private bool empty = true;

    public event Action<UIAnimalEgg> OnEggClicked;

    public void Awake()
    {
        ResetData();
        Deselect();
    }

    public void SetData(Sprite sprite, string name, int quantity)
    {
        eggImage.sprite = sprite;
        eggName.text = name;
        eggQuantity.text = quantity + "";
        empty = false;
    }

    public void ResetData()
    {
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
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button==PointerEventData.InputButton.Left)
        {
            OnEggClicked?.Invoke(this);
        }
    }
}
