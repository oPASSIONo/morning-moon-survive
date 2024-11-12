using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;
public class UIHealthBar : MonoBehaviour
{
    private Health healthComponent; // Reference to the Health component
    [SerializeField] private Slider slider; // Reference to the Slider UI component
    [SerializeField] private TMP_Text healthText; // Reference to the TextMeshPro text element

    #region HealthSinglePlayer

     /*void Start()
        {
            // Subscribe to the health changed event
            healthComponent.OnHealthChanged += UpdateHealthBar;
    
            // Call UpdateHealthBar immediately to ensure correct initialization
            UpdateHealthBar(healthComponent.CurrentHealth, healthComponent.MaxHealth,healthComponent.MinHealth);
        }*/

    #endregion
   
    
    private void Start()
    {
        // Subscribe to network spawn events
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            // Delay setting up the health component until the local player has spawned
            TryAssignLocalPlayerHealth();
        }
    }

    private void TryAssignLocalPlayerHealth()
    {
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                healthComponent = networkObject.GetComponent<Health>();
                break;
            }
        }

        // Ensure healthComponent was found and subscribe to events
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(healthComponent.CurrentHealth, healthComponent.MaxHealth, healthComponent.MinHealth);
        }
        else
        {
            Debug.LogError("Local player's Health component not found.");
        }
    }


    private void UpdateHealthBar(float currentHealth, float maxHealth,float minHealth)
    {
        // Update the slider value to reflect the current health
        slider.value = (float)currentHealth / maxHealth;
        
        // Update the text to display the current health value
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid potential memory leaks
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }

        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged -= UpdateHealthBar;
        }
    }
    
}
