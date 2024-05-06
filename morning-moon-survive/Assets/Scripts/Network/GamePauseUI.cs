using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
           // NetworkManager.Singleton.Shutdown();
           GameMultiplayerManager.Instance.OnDisconnect();
           Loader.Load(Loader.Scene.MainMenu);
        });
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        
    }
    private void Start()
    {
        GameManager.Instance.OnLocalGamePaused += GameUIManager_OnLocalGamePaused;
        GameManager.Instance.OnLocalGameUnPaused += GameUIManager_OnLocalGameUnPaused;
        
        Hide();
    }

    private void GameUIManager_OnLocalGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }
    
    private void GameUIManager_OnLocalGameUnPaused(object sender, System.EventArgs e)
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
