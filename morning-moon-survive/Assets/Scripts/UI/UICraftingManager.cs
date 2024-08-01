using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftingManager : MonoBehaviour
{
    public static UICraftingManager Instance { get; private set; }
    [SerializeField] private CraftingSO playerCraftingSO;
    [SerializeField] private GameObject playerCraftingPage;

    [SerializeField] private GameObject workshopCraftingPage;
    [SerializeField] private CraftingSO workshopCraftingSO;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    private void OpenPlayerCraftingUI()
    {
        switch (PlayerStateManager.Instance.currentState)
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
        switch (PlayerStateManager.Instance.currentState)
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
        if (PlayerStateManager.Instance.currentState == PlayerStateManager.PlayerState.Normal)
        {
            PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Workshop);
        }
        else if (PlayerStateManager.Instance.currentState == PlayerStateManager.PlayerState.Workshop)
        {
            PlayerStateManager.Instance.SetState(PlayerStateManager.PlayerState.Normal);
        }
    }
}
