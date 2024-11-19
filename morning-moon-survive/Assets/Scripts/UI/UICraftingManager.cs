using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UICraftingManager : MonoBehaviour 
{
    public static UICraftingManager Instance { get; private set; }
    [SerializeField] private CraftingSO playerCraftingSO;
    [SerializeField] private GameObject playerCraftingPage;

    [SerializeField] private GameObject workshopCraftingPage;
    [SerializeField] private CraftingSO workshopCraftingSO;
    
    private PlayerStateManager playerStateManager;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            Instance = this;
        }
    }

    private void Start()
    {
        GameInput.Instance.OnCraftingAction += GameInput_OnCraftingAction;
    }
    private void GameInput_OnCraftingAction(object sender, EventArgs e)
    {
        OpenPlayerCraftingUI();
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
            Debug.Log("Local player's UICraftingManager found.");
        }
        else
        {
            Debug.Log("Local player's UICraftingManager not found.");
        }
    }

    private void OpenPlayerCraftingUI()
    {
        switch (playerStateManager.currentState)
        {
            case PlayerStateManager.PlayerState.Crafting:
                playerCraftingPage.SetActive(true);
                playerCraftingPage.GetComponent<UICraftingPage>().PopulateCraftingUI(playerCraftingSO);
                break;
            case PlayerStateManager.PlayerState.Normal:
                playerCraftingPage.SetActive(false);
                break;
        }
    }

    public void SetWorkshopCraftingSo(CraftingSO workshopSO)
    {
        workshopCraftingSO = workshopSO;
    }
    public void OpenWorkshopUI()
    {
        switch (playerStateManager.currentState)
        {
            case PlayerStateManager.PlayerState.Workshop:
                workshopCraftingPage.SetActive(true);
                workshopCraftingPage.GetComponent<UICraftingPage>().PopulateCraftingUI(workshopCraftingSO);
                break;
            case PlayerStateManager.PlayerState.Normal:
                workshopCraftingPage.SetActive(false);
                break;
        }
    }
    public void ToggleWorkshop()
    {
        if (playerStateManager.currentState == PlayerStateManager.PlayerState.Normal)
        {
            playerStateManager.SetState(PlayerStateManager.PlayerState.Workshop);
        }
        else if (playerStateManager.currentState == PlayerStateManager.PlayerState.Workshop)
        {
            playerStateManager.SetState(PlayerStateManager.PlayerState.Normal);
        }
    }
}
