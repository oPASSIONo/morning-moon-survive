using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{

    [SerializeField] private Button serverBt;
    [SerializeField] private Button hostBt;
    [SerializeField] private Button clientBt;

    private void Awake()
    {
        serverBt.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        hostBt.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        clientBt.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
