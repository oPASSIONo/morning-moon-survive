using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button quitButton;

    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private string sceneName = "PlayScene";

    private void Awake()
    {
        QuitGame();
    }
    private async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Player ID : " + AuthenticationService.Instance.PlayerId);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }
    }

    public void StartHost()
    {
        HostManager.Instance.StartHost();
        Loader.Load(Loader.Scene.PlayScene);
    }

    public void StartClient()
    {
        ClientManager.Instance.StartClient(joinCodeInputField.text);
    }

    private void QuitGame()
    {
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    
    


}
