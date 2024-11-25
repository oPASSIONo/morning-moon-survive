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
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

    }
    
    private void Start()
    {
        /*if (IsLocalPlayer)
        {
            playerStateManager = GetComponent<PlayerStateManager>();

        }*/
        GameInput.Instance.OnBuildingAction += InstanceOnOnBuildingAction;
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
            Debug.Log("Local player's UBuildingManager found.");
        }
        else
        {
            Debug.Log("Local player's UBuildingManager not found.");
        }
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
