using UnityEngine;
using UnityEngine.UI;

public class CraftButtonHandler : MonoBehaviour
{
    public Button craftButton;
    public CraftingSystem craftingSystem;

    private Recipe selectedRecipe;

    private void Awake()
    {
        craftButton.onClick.AddListener(OnCraftButtonClick);
    }

    public void SetSelectedRecipe(Recipe recipe)
    {
        selectedRecipe = recipe;
    }

    private void OnCraftButtonClick()
    {
        if (selectedRecipe != null)
        {
            craftingSystem.CraftItem(selectedRecipe);
        }
        else
        {
            Debug.LogWarning("No recipe selected.");
        }
    }
}