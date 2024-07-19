using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISatietyBar : MonoBehaviour
{
    public Satiety satietyComponent; // Reference to the Hunger component
    [SerializeField] private Slider slider; // Reference to the Slider UI component
    [SerializeField] private TMP_Text hungerText; // Reference to the TextMeshPro text element

    void Start()
    {
        /*// Ensure that the hunger component, slider, and text references are set
        if (satietyComponent == null || slider == null || hungerText == null)
        {
            Debug.LogError("Hunger component, Slider, or TextMeshPro text reference not set.");
            enabled = false; // Disable the script if references are not set
            return;
        }*/

        // Subscribe to the hunger changed event
        satietyComponent.OnSatietyChanged += UpdateHungerBar;

        // Initialize the hunger bar
        UpdateHungerBar(satietyComponent.CurrentSatiety, satietyComponent.MaxSatiety);
    }

    void UpdateHungerBar(float currentHunger, float maxHunger)
    {
        // Update the slider value to reflect the current hunger level
        slider.value = (float)currentHunger / maxHunger;

        // Update the text to display the current hunger value
        hungerText.text = $"{currentHunger}/{maxHunger}";
    }

    void OnDestroy()
    {
        // Unsubscribe from the hunger changed event when the script is destroyed
        if (satietyComponent != null)
        {
            satietyComponent.OnSatietyChanged -= UpdateHungerBar;
        }
    }
}

