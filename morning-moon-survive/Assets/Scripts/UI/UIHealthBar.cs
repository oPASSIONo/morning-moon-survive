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
        // Ensure that the health component, slider, and text references are set
        if (healthComponent == null || slider == null || healthText == null)
        {
            Debug.LogError("Health component, Slider, or TextMeshPro text reference not set.");
            enabled = false; // Disable the script if references are not set
            return;
        }

        // Subscribe to the health changed event
        healthComponent.OnHealthChanged += UpdateHealthBar;

        // Call UpdateHealthBar immediately to ensure correct initialization
        UpdateHealthBar(healthComponent.CurrentHealth, healthComponent.MaxHealth);
    }


    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        // Update the slider value to reflect the current health
        slider.value = (float)currentHealth / maxHealth;

        // Update the text to display the current health value
        healthText.text = $"{currentHealth}/{maxHealth}";
    }

    void OnDestroy()
    {
        // Unsubscribe from the health changed event when the script is destroyed
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged -= UpdateHealthBar;
        }
    }
}

    
/*[SerializeField] private Slider slider;
[SerializeField] private TMP_Text healthText;

void Start()
{
   // Ensure slider and text are set
   if (slider == null || healthText == null)
   {
       Debug.LogError("Slider or TextMeshPro text reference not set.");
       enabled = false;
       return;
   }

   // Subscribe to health changes
   HealthController.Instance.OnHealthChanged += UpdateHealthBar;

   // Update health bar immediately
   UpdateHealthBar(HealthController.Instance.GetCurrentHealth(), HealthController.Instance.GetMaxHealth());
}

void UpdateHealthBar(int currentHealth, int maxHealth)
{
   slider.value = (float)currentHealth / maxHealth;
   healthText.text = $"{currentHealth}/{maxHealth}";
}

void OnDestroy()
{
   // Unsubscribe from health changes
   HealthController.Instance.OnHealthChanged -= UpdateHealthBar;
}*/