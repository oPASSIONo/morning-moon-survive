using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStaminaBar : MonoBehaviour
{
    public Stamina stamina;
    public Slider slider;
    public TMP_Text staminaText; // Reference to the TextMeshPro text component

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the slider value to represent the player's current stamina
        UpdateStaminaBar();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the stamina bar if the player's stamina changes
        UpdateStaminaBar();
    }

    void UpdateStaminaBar()
    {
        // Ensure the stamina and slider references are set
        if (stamina != null && slider != null)
        {
            // Calculate the normalized stamina value (between 0 and 1)
            float normalizedStamina = stamina.CurrentStamina / stamina.MaxStamina;

            // Set the slider value to represent the normalized stamina value
            slider.value = normalizedStamina;

            // Update the stamina text if it's available
            if (staminaText != null)
            {
                // Display the current stamina value as text
                staminaText.text = "Stamina: " + Mathf.RoundToInt(stamina.CurrentStamina).ToString();
            }
        }
    }
}

