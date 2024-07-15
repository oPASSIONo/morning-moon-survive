using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{
    private void Start()
    {
        NetworkGameManager.Instance.OnMultiplayerGamePaused += GameUIManager_OnMultiplayerGamePause;
        NetworkGameManager.Instance.OnMultiplayerGameUnPaused += GameUIManager_OnMultiplayerGameUnPause;
        
        Hide();
    }

    private void GameUIManager_OnMultiplayerGamePause(object sender, System.EventArgs e)
    {
        Show();
    }
    
    private void GameUIManager_OnMultiplayerGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
