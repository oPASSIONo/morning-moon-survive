using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemeyHealthBar : MonoBehaviour
{
    public Health healthComponent;
    public Slider slider;

    void Start()
    {
        healthComponent.OnHealthChanged += UpdateEnemyHealthBar;
        
        slider.maxValue = healthComponent.MaxHealth;
        slider.value = healthComponent.CurrentHealth;
    }

    public void UpdateEnemyHealthBar(float currentHealth, float maxHealth, float minHealth)
    {
        slider.value = currentHealth;
    }
}