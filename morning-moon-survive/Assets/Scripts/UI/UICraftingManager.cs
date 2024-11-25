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
    public void SetPlayerStateManager(PlayerStateManager stateManager)
    {
        Debug.Log("Local player's UICraftManager found.");
        playerStateManager = stateManager;
        
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
