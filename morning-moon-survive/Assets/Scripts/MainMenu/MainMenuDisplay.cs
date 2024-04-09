using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuDisplay : MonoBehaviour
{
    [SerializeField] private string gamePlaySceneName = "GameScene";
    
    [SerializeField] private Button hostButton;


    private void Awake()
    {
        hostButton.onClick.AddListener(StartHost);
    }


    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(gamePlaySceneName, LoadSceneMode.Single);

    }
}
