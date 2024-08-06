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
        SendWorkshopCraftingSO();
        PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Workshop);
        UICraftingManager.Instance.OpenWorkshopUI();
    }

    public void SendWorkshopCraftingSO()
    {
        UICraftingManager.Instance.SetWorkshopCraftingSo(craftingSO);
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