using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    public float MinHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnEntityDie;
    
    public void Initialize(float maxHealth, float minHealth, float initialHealth)
    {
        MaxHealth = maxHealth;
        MinHealth = minHealth;
        CurrentHealth = initialHealth;
        
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
    
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        
        if (CurrentHealth<=MinHealth)
        {
            SetCurrentHealth(MinHealth);
        }

        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        IsDie();
    }
    
    public void AddHealth(float amount)
    {
        SetCurrentHealth(CurrentHealth + amount);
        if (CurrentHealth>=MaxHealth)
        {
            SetCurrentHealth(MaxHealth);
        }
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        Debug.Log("Entity add health");
    }

    private void IsDie()
    {
        switch (CurrentHealth)
        {
            case 0:
                Die();
                break;
        }
    }
    
    public void SetCurrentHealth(float value)
    {
        CurrentHealth = value;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
    }
    
    public void Die()
    {
        CurrentHealth = MinHealth;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth);
        OnEntityDie?.Invoke();
        Debug.Log($"Entity has died.Current Health : {CurrentHealth}");
    }
    
    public void SetMaxHealth(float value)
    {
        MaxHealth = value;
    }

    public void SetMinHealth(float value)
    {
        MinHealth = value;
    }
}