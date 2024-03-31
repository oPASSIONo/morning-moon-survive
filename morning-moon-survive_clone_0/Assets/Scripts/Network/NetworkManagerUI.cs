using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Networking.Transport;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private InputField ipAddressInput;
    
    private void Start()
    {
        serverButton.onClick.AddListener(StartServer);
        hostButton.onClick.AddListener(StartHost);
        clientButton.onClick.AddListener(StartClient);
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