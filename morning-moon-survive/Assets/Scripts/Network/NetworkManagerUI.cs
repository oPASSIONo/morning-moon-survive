using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI playerCount;
    [SerializeField] private TMP_InputField ipAddressAndPortInputField;
    [SerializeField] private TextMeshProUGUI portText;

    [SerializeField] private GameObject panel;

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
        if (!NetworkManager.Singleton.IsServer)
            return;
        playerNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

    private void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    private void StartHost()
    {
        System.Random random = new System.Random();
        ushort randomPort = (ushort)random.Next(10000, 20000);

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            "0.0.0.0",  // Use any available IP address
            randomPort
        );
        portText.text =  "Port : " + randomPort.ToString();
        NetworkManager.Singleton.StartHost();
    }
    
    private void StartClient()
    {
        string ipAddressAndPort = ipAddressAndPortInputField.text;

        string[] parts = ipAddressAndPort.Split(':');
        if (parts.Length == 2)
        {
            string ipAddress = parts[0];
            ushort port;
            if (ushort.TryParse(parts[1], out port))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                    ipAddress,
                    port
                );

                NetworkManager.Singleton.StartClient();
                panel.SetActive(false);
                return;
            }
        }
        Debug.LogError("Invalid input format. Please enter IP address and port in the format 'IP:Port'");
    }
        
}
