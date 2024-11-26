using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAnimalEgg : UIItem
{
    [SerializeField] private TMP_Text eggQuantity;
    private AnimalEggSO animalEggData;

    public event Action<AnimalEggSO> OnEggClicked;

    public void SetData(Sprite sprite, string name, int quantity, AnimalEggSO eggData)
    {
        base.SetData(sprite, name);
        eggQuantity.text = quantity.ToString();
        animalEggData = eggData;
    }

    public override void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnEggClicked?.Invoke(animalEggData);
        }
        else
        {
            base.OnPointerClick(pointerData);
        }
    }
}