using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    [SerializeField] private Image ingredientIcon;
    [SerializeField] private TMP_Text ingredientName;
    [SerializeField] private TMP_Text ingredientQuantity;

    public void SetData(Sprite icon, string name, int quantity)
    {
        ingredientIcon.sprite = icon;
        ingredientName.text = name;
        ingredientQuantity.text = $"x{quantity}";
    }
}