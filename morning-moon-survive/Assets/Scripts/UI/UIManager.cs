using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CraftingSO playerCraftingSO;
    [SerializeField] private GameObject playerCraftingPage;

    private void Start()
    {
        GameInput.Instance.OnCraftingAction += GameInput_OnCraftingAction;
    }

    private void GameInput_OnCraftingAction(object sender, EventArgs e)
    {
        Debug.Log("Crafting UI Call");
        OpenPlayerCraftingUI();
    }
    private void OpenPlayerCraftingUI()
    {
        switch (PlayerStateManager.Instance.currentState)
        {
            case PlayerStateManager.PlayerState.Crafting:
                playerCraftingPage.SetActive(true);
                playerCraftingPage.GetComponent<UICraftingPage>().PopulateCraftingUI(playerCraftingSO);
                Debug.Log("Set Active Crafting UI");
                break;
            case PlayerStateManager.PlayerState.Normal:
                playerCraftingPage.SetActive(false);
                Debug.Log("Set InActive Crafting UI");
                break;
        }
    }
}
