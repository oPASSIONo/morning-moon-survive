using Inventory;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager
{
    public Player Player { get; private set; }
    public Health PlayerHealth { get; private set; }
    public Stamina PlayerStamina { get; private set; }
    public Satiety PlayerSatiety { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public AgentTool PlayerAgentTool { get; private set; }
    public PlayerStateManager PlayerStateManager { get; private set; }
    public InventoryController PlayerInventoryController { get; private set; }
    public NetworkObject PlayerNetworkObject { get; private set; } // Stores the player's NetworkObject

    public bool HasPlayer => PlayerNetworkObject != null; // Checks if the player is assigned

    public void Initialize(NetworkObject playerObject)
    {
        Player = playerObject.GetComponent<Player>();
        PlayerHealth = playerObject.GetComponent<Health>();
        PlayerStamina = playerObject.GetComponent<Stamina>();
        PlayerSatiety = playerObject.GetComponent<Satiety>();
        PlayerAgentTool = playerObject.GetComponent<AgentTool>();
        PlayerAnimation = playerObject.GetComponent<PlayerAnimation>();
        PlayerStateManager = playerObject.GetComponent<PlayerStateManager>();
        PlayerInventoryController = playerObject.GetComponent<InventoryController>();

        PlayerNetworkObject = playerObject;

        if (PlayerHealth != null)
        {
            PlayerHealth.OnEntityDie += OnPlayerDie;
        }
    }
    

    public void Clear()
    {
        PlayerNetworkObject = null;
    }

    private void OnPlayerDie()
    {
        Debug.Log("Player has died.");
    }
}