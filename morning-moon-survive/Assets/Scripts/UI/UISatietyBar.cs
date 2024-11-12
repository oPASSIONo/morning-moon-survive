using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class UISatietyBar : MonoBehaviour
{
    private Satiety satietyComponent; // Reference to the Hunger component
    [SerializeField] private Slider slider; // Reference to the Slider UI component
    [SerializeField] private TMP_Text hungerText; // Reference to the TextMeshPro text element

    #region Satiety Single Play

    /*
        void Start()
        {
            /#1#/ Ensure that the hunger component, slider, and text references are set
            if (satietyComponent == null || slider == null || hungerText == null)
            {
                Debug.LogError("Hunger component, Slider, or TextMeshPro text reference not set.");
                enabled = false; // Disable the script if references are not set
                return;
            }#1#
    
            // Subscribe to the hunger changed event
            satietyComponent.OnSatietyChanged += UpdateHungerBar;
    
            // Initialize the hunger bar
            UpdateHungerBar(satietyComponent.CurrentSatiety, satietyComponent.MaxSatiety);
        }
        */

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
            // Attempt to find the local player's Satiety component after spawning
            TryAssignLocalPlayerSatiety();
        }
    }

    private void TryAssignLocalPlayerSatiety()
    {
        // Loop through all NetworkObjects to find the local player
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                satietyComponent = networkObject.GetComponent<Satiety>();
                break;
            }
        }

        // Ensure satietyComponent was found and subscribe to the OnSatietyChanged event
        if (satietyComponent != null)
        {
            satietyComponent.OnSatietyChanged += UpdateHungerBar;
            UpdateHungerBar(satietyComponent.CurrentSatiety, satietyComponent.MaxSatiety);
        }
        else
        {
            Debug.LogError("Local player's Satiety component not found.");
        }
    }

    void UpdateHungerBar(float currentHunger, float maxHunger)
    {
        // Update the slider value to reflect the current hunger level
        slider.value = (float)currentHunger / maxHunger;

        // Update the text to display the current hunger value
        hungerText.text = $"{currentHunger}/{maxHunger}";
    }

    private void OnDestroy()
    {
        // Unsubscribe from network and Satiety events
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }

        if (satietyComponent != null)
        {
            satietyComponent.OnSatietyChanged -= UpdateHungerBar;
        }
    }
}

