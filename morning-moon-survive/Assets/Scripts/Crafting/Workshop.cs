using System;
using UnityEngine;

public class Workshop : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject UIWorkshop;
    [SerializeField] private CraftingSO craftingSO;
    [SerializeField] private UICraftingPage craftingPage;
    
    //public event Action<bool> OnWorkshopInteract;
    public void Interact()
    {
        Debug.Log("Interacting with workshop");
        //OnWorkshopInteract?.Invoke(true);
        UIWorkshop.SetActive(true);
        craftingPage.PopulateCraftingUI(craftingSO);
    }
    
    public void ShowInteractPrompt()
    {
        // Implement UI or prompt to indicate interaction (e.g., display "Press E to interact")
    }

    public void HideInteractPrompt()
    {
        // Implement hiding of interaction UI or prompt
    }
}