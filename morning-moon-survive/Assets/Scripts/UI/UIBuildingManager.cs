using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class UIBuildingManager : MonoBehaviour
{
    public static UIBuildingManager Instance { get; private set; }

    [SerializeField] private BuildingObjectSo buildingObjectSo;
    [SerializeField] private GameObject playerBuildingPage;
    
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
        GameInput.Instance.OnBuildingAction += InstanceOnOnBuildingAction;
    }
    
    public void SetPlayerStateManager(PlayerStateManager stateManager)
    {
        playerStateManager = stateManager;
        
    }

    private void InstanceOnOnBuildingAction(object sender, EventArgs e)
    {
        OpenPlayerBuildingUI();
    }


    private void OpenPlayerBuildingUI()
    {
        switch (playerStateManager.currentState)
        {
            case PlayerStateManager.PlayerState.Building : 
                playerBuildingPage.SetActive(true);
                playerBuildingPage.GetComponent<UIBuildingPage>().PopulateBuildingUI(buildingObjectSo);
                break;
            case PlayerStateManager.PlayerState.Normal :
                playerBuildingPage.SetActive(false);
                break;
        }
    }
}
