using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIBuildingDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private RectTransform ingredientsPanel;
    [SerializeField] private GameObject ingredientPrefab;

    private List<GameObject> ingredientUIObjects = new List<GameObject>();
    
    /*public void SetDescription(Sprite itemSprite, string itemName, int id, List<RequiredIngredient> ingredients)
    {
        // Set the item image, title, and description
        itemImage.sprite = itemSprite;
        title.text = itemName;
        description.text = id.ToString();

        // Clear existing ingredient UI objectsS
        foreach (var uiObject in ingredientUIObjects)
        {
            Destroy(uiObject);
        }
        ingredientUIObjects.Clear();

        // Create and populate ingredient UI elements
        foreach (var ingredient in ingredients)
        {
            var ingredientUI = Instantiate(ingredientPrefab, ingredientsPanel);
            var ingredientUIScript = ingredientUI.GetComponent<IngredientUI>();

            ingredientUIScript.SetData(ingredient.item.ItemImage, ingredient.item.name, ingredient.quantity);

            ingredientUIObjects.Add(ingredientUI);
        }
    }*/
    
    public void SetDescription(Sprite itemSprite, string itemName, int id)
    {
        // Set the item image, title, and description
        itemImage.sprite = itemSprite;
        title.text = itemName;
        description.text = id.ToString();

        // Clear existing ingredient UI objectsS
        foreach (var uiObject in ingredientUIObjects)
        {
            Destroy(uiObject);
        }
        ingredientUIObjects.Clear();
    }

}
