using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Health healthComponent;
    public Slider slider;
    public TMP_Text enemyHealthText;
    public TMP_Text enemyName;

    void Start()
    {
        healthComponent.OnHealthChanged += UpEnemydateHealthBar;
        
        slider.maxValue = healthComponent.MaxHealth;
        slider.value = healthComponent.CurrentHealth;
        
        enemyHealthText.text = $"{healthComponent.CurrentHealth}/{healthComponent.MaxHealth}";
        enemyName.text = enemy.Name;
    }

    public void UpEnemydateHealthBar(float currentHealth, float maxHealth, float minHealth)
    {
        slider.value = currentHealth;
        
        enemyHealthText.text = $"{currentHealth}/{maxHealth}";
    }
}