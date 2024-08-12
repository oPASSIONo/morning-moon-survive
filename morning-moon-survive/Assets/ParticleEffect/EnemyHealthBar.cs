using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Health healthComponent;
    public Slider slider;
    public TMP_Text enemyHealthText;

    void Start()
    {
        healthComponent.OnHealthChanged += UpEnemydateHealthBar;
        
        slider.maxValue = healthComponent.MaxHealth;
        slider.value = healthComponent.CurrentHealth;
        
        enemyHealthText.text = $"{healthComponent.CurrentHealth}/{healthComponent.MaxHealth}";
    }

    public void UpEnemydateHealthBar(float currentHealth, float maxHealth, float minHealth)
    {
        slider.value = currentHealth;
        
        enemyHealthText.text = $"{currentHealth}/{maxHealth}";
    }
}