using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIEquipmentPage : MonoBehaviour
{
    [SerializeField] private Image accessoryImage;
    [SerializeField] private Image garmentImage;
    
    
    public void SetEquipImage(EquipmentType type, Sprite equipSprite)
    {
        switch (type)
        {
            case EquipmentType.Accessory:
                accessoryImage.sprite = equipSprite;
                break;
            case EquipmentType.Garment:
                garmentImage.sprite = equipSprite;
                break;
            // Add more cases for other EquipmentType values as needed
            default:
                Debug.LogWarning("Unsupported EquipmentType: " + type);
                break;
        }
    }

    
}
