using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIBuildingManager : MonoBehaviour
{
    public static UIBuildingManager Instance { get; private set; }

    [SerializeField] private BuildingObjectSo buildingObjectSo;
    [SerializeField] private GameObject playerBuildingPage;

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

    private void InstanceOnOnBuildingAction(object sender, EventArgs e)
    {
        OpenPlayerBuildingUI();
    }


    private void OpenPlayerBuildingUI()
    {
        switch (PlayerStateManager.Instance.currentState)
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
