using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    public float MinHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public event Action<float, float,float> OnHealthChanged;
    public event Action OnEntityDie;

    public void Initialize(float maxHealth, float minHealth, float initialHealth)
    {
        MaxHealth = maxHealth;
        MinHealth = minHealth;
        CurrentHealth = initialHealth;
        
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);
        
    }
    
    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        
        if (CurrentHealth<=MinHealth)
        {
            SetCurrentHealth(MinHealth);
        }
        
        DamagePopup.current.CreatePopup(transform.position, damageAmount.ToString());
        
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);

        IsDie();
        Debug.Log($"{name} Take Damage ! Current HP : {CurrentHealth}");
    }
    
    public void AddHealth(float amount)
    {
        SetCurrentHealth(CurrentHealth + amount);
        if (CurrentHealth>=MaxHealth)
        {
            SetCurrentHealth(MaxHealth);
        }
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);
        Debug.Log($"{name} add health");
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
    
    public void Die()
    {
        CurrentHealth = MinHealth;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
        OnEntityDie?.Invoke();
        Debug.Log($"{name} has died.Current Health : {CurrentHealth}");
    }
        
    public void SetCurrentHealth(float hp)
    {
        CurrentHealth = hp;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
    }

    public void SetMaxHealth(float val)
    {
        MaxHealth = val;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
    }

    public void SetMinHealth(float val)
    {
        MinHealth = val;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
    }
    
}