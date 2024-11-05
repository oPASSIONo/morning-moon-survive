using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAnimalEgg : UIItem
{
    [SerializeField] private TMP_Text eggQuantity;
    private AnimalEggSO animalEggData;

    // Event specifically for when an animal egg is clicked
    public event Action<AnimalEggSO> OnEggClicked;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetData(Sprite sprite, string name, int quantity, AnimalEggSO animalEgg)
    {
        base.SetData(sprite, name);
        eggQuantity.text = quantity.ToString();
        animalEggData = animalEgg; // Store the reference
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