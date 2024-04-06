using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public event System.Action<int, int> OnHealthChanged; // Event to notify health changes

    void Start()
    {
        // Initialize current health to max health
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce current health by damage amount
        currentHealth -= damageAmount;
        // Clamp current health to ensure it stays within bounds
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Trigger health changed event
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Check if health is zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int amount)
    {
        // Increase current health by the specified amount
        currentHealth += amount;
        // Clamp current health to ensure it stays within bounds
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Trigger health changed event
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Die()
    {
        // Perform death actions here
        Debug.Log("Entity has died.");
        // For example, destroy the GameObject
        //Destroy(gameObject);
    }
}
