using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine.InputSystem;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playerCount;
    
    [SerializeField] private InputField ipAddressInput;


    private NetworkVariable<int> playerNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    
    
    private void Awake()
    {
        serverButton.onClick.AddListener(StartServer);
        hostButton.onClick.AddListener(StartHost);
        clientButton.onClick.AddListener(StartClient);
    }

    private void Update()
    {
        playerCount.text = "Online : " + playerNum.Value.ToString();
        
        if (!IsServer) return;
        playerNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();

    }
}