using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{
    private void Start()
    {
        ClientManager.Instance.OnTryingToJoinGame += ClientManager_OnTryingToJoinGame;
        ClientManager.Instance.OnFailedToJoinGame += ClientManager_OnFailedToJoinGame;
        
        Hide();
    }
    
    
    private void ClientManager_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void ClientManager_OnTryingToJoinGame(object sender, System.EventArgs e)
    {
        Show();
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
        ClientManager.Instance.OnTryingToJoinGame -= ClientManager_OnTryingToJoinGame;
        ClientManager.Instance.OnFailedToJoinGame -= ClientManager_OnFailedToJoinGame;
    }
}
