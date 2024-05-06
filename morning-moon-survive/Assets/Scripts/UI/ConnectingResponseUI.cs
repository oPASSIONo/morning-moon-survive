using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConnectingResponseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        ClientManager.Instance.OnFailedToJoinGame += ClientManager_OnFailedToJoinGame;
        
        Hide();
    }

    private void ClientManager_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        Show();

        messageText.text = NetworkManager.Singleton.DisconnectReason;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ClientManager.Instance.OnFailedToJoinGame -= ClientManager_OnFailedToJoinGame;
    }
}
