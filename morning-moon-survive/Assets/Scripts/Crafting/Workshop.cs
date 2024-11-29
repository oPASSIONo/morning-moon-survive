using System;
using Unity.Netcode;
using UnityEngine;

public class Workshop : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject UIWorkshop;
    [SerializeField] private CraftingSO craftingSO;
    [SerializeField] private UICraftingPage craftingPage;
    
    private PlayerStateManager playerStateManager;

    //public event Action<bool> OnWorkshopInteract;

    private void Awake()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }
    
    private void OnClientConnected(ulong obj)
    {
        if (NetworkManager.Singleton.LocalClientId == obj)
        {
            TryAssignLocalPlayer();
        }
    }
    private void TryAssignLocalPlayer()
    {
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                playerStateManager = networkObject.GetComponent<PlayerStateManager>();
                break;
            }
        }
        
        if (playerStateManager != null)
        {
            // Perform actions with the inventoryController (e.g., update UI, listen to events)
            Debug.Log("Local player's Workshop found.");
        }
        else
        {
            Debug.Log("Local player's Workshop not found.");
        }
    }
    public void Interact(GameObject player)
    {
        Debug.Log("Interacting with workshop");
        SendWorkshopCraftingSO();
        playerStateManager.SetState(PlayerStateManager.PlayerState.Workshop);
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