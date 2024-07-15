using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Health healthComponent; // Reference to the Health component
    [SerializeField] private Slider slider; // Reference to the Slider UI component
    [SerializeField] private TMP_Text healthText; // Reference to the TextMeshPro text element

    void Start()
    {
        // Subscribe to the health changed event
        healthComponent.OnHealthChanged += UpdateHealthBar;

        // Call UpdateHealthBar immediately to ensure correct initialization
        UpdateHealthBar(healthComponent.CurrentHealth, healthComponent.MaxHealth);
    }


    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Update the slider value to reflect the current health
        slider.value = (float)currentHealth / maxHealth;
        
        // Update the text to display the current health value
        healthText.text = $"{currentHealth}/{maxHealth}";
    }
    
}
