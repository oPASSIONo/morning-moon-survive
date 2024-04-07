using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth { get; private set; } = 100;
    public int CurrentHealth { get; private set; }

    public event System.Action<int, int> OnHealthChanged; // Event to notify health changes

    void Start()
    {
        // Initialize current health to max health
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        Debug.Log(CurrentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce current health by damage amount
        CurrentHealth -= damageAmount;
        // Clamp current health to ensure it stays within bounds
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        // Check if health is zero
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int amount)
    {
        // Increase current health by the specified amount
        CurrentHealth += amount;
        // Clamp current health to ensure it stays within bounds
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    void Die()
    {
        // Perform death actions here
        Debug.Log("Entity has died.");
        // For example, destroy the GameObject
        //Destroy(gameObject);
    }
}

