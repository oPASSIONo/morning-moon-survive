using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStaminaBar : MonoBehaviour
{
    public Stamina staminaComponent;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text staminaText; // Reference to the TextMeshPro text component

    // Start is called before the first frame update
    void Start()
    {
        staminaComponent.OnStaminaChanged += UpdateStaminaBar;
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
}

