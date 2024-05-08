using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthController : MonoBehaviour
{
    // Singleton instance
    private static HealthController instance;

    // Accessor for the singleton instance
    public static HealthController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HealthController>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("HealthController");
                    instance = obj.AddComponent<HealthController>();
                }
            }
            return instance;
        }
    }

    // Health-related data
    private int maxHealth = 100;
    private int currentHealth;

    // Event to notify health changes
    public event Action<int, int> OnHealthChanged;

    // Initialize health
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        //Debug.Log("TakeDamage");
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Method to restore health
    /*public void RestoreHealth(int restoreAmount)
    {
        currentHealth += restoreAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }*/

    // Getters for health data
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void RestoreHealth(int val)
    {
        currentHealth += val;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
