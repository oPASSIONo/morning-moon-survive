using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Health healthComponent;
    public Slider slider;

    void Start()
    {
        healthComponent.OnHealthChanged += UpEnemydateHealthBar;
        
        slider.maxValue = healthComponent.MaxHealth;
        slider.value = healthComponent.CurrentHealth;
    }

    public void UpEnemydateHealthBar(float currentHealth, float maxHealth, float minHealth)
    {
        slider.value = currentHealth;
    }
}