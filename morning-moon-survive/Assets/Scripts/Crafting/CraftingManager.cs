using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private UICraftingPage craftingPage;
    [SerializeField] private CraftingSO[] craftingSOs;
    private int currentCraftingIndex = 0;

    private void Start()
    {
        // Example: Start with the first crafting category
        SwitchCraftingCategory(0);
    }

    public void SwitchCraftingCategory(int index)
    {
        if (index < 0 || index >= craftingSOs.Length)
        {
            Debug.LogError("Crafting index out of range.");
            return;
        }

        currentCraftingIndex = index;
        craftingPage.PopulateCraftingUI(craftingSOs[currentCraftingIndex]);
    }

    public void NextCraftingCategory()
    {
        currentCraftingIndex = (currentCraftingIndex + 1) % craftingSOs.Length;
        craftingPage.PopulateCraftingUI(craftingSOs[currentCraftingIndex]);
    }

    public void PreviousCraftingCategory()
    {
        currentCraftingIndex = (currentCraftingIndex - 1 + craftingSOs.Length) % craftingSOs.Length;
        craftingPage.PopulateCraftingUI(craftingSOs[currentCraftingIndex]);
    }
}