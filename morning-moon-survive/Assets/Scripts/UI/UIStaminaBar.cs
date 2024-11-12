using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;


public class UIStaminaBar : MonoBehaviour
{
    private Stamina staminaComponent;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text staminaText; // Reference to the TextMeshPro text component

    private void Start()
    {
        // Subscribe to network spawn events
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            // Attempt to find the local player's Stamina component after spawning
            TryAssignLocalPlayerStamina();
        }
    }

    private void TryAssignLocalPlayerStamina()
    {
        // Loop through all NetworkObjects to find the local player
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                staminaComponent = networkObject.GetComponent<Stamina>();
                break;
            }
        }

        // Ensure staminaComponent was found and subscribe to the OnStaminaChanged event
        if (staminaComponent != null)
        {
            staminaComponent.OnStaminaChanged += UpdateStaminaBar;
            UpdateStaminaBar(staminaComponent.CurrentStamina, staminaComponent.MaxStamina);
        }
        else
        {
            Debug.LogError("Local player's Stamina component not found.");
        }
    }
    
    private void UpdateStaminaBar(float currentStamina,float maxStamina)
    {
        // Ensure the stamina and slider references are set
        if (staminaComponent != null && slider != null)
        {
            // Calculate the normalized stamina value (between 0 and 1)
            float normalizedStamina = currentStamina / maxStamina;

            // Set the slider value to represent the normalized stamina value
            slider.value = normalizedStamina;

            // Update the stamina text if it's available
            if (staminaText != null)
            {
                // Display the current stamina value as text
                staminaText.text = $"{Mathf.RoundToInt(currentStamina).ToString()}/{maxStamina}";
            }
        }
    }
    private void OnDestroy()
    {
        // Unsubscribe from network and Stamina events
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }

        if (staminaComponent != null)
        {
            staminaComponent.OnStaminaChanged -= UpdateStaminaBar;
        }
    }
}

