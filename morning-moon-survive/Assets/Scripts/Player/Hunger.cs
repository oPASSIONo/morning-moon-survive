using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hunger : MonoBehaviour
{
    public int maxHunger = 100;
    public int currentHunger;
    private float damageInterval = 1f; // Time interval between damage ticks
    private int damageAmount = 1; // Amount of damage per tick

    public event Action<int, int> OnHungerChanged; // Event to notify hunger changes

    public Health health; // Reference to the Health component for the player

    float nextDamageTime; // Time of the next damage tick

    void Start()
    {
        // Initialize current hunger to 0
        currentHunger = 0;

        // Initialize the time of the next damage tick
        nextDamageTime = Time.time + damageInterval;
    }

    void Update()
    {
        // Check if hunger has reached the maximum value
        if (currentHunger >= maxHunger)
        {
            // Check if it's time for the next damage tick
            if (Time.time >= nextDamageTime)
            {
                // Apply damage to the player's health
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }

                // Update the time of the next damage tick
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    public void IncreaseHunger(int amount)
    {
        // Increase current hunger by the specified amount
        currentHunger += amount;
        // Clamp current hunger to ensure it stays within bounds
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);

        // Trigger hunger changed event
        OnHungerChanged?.Invoke(currentHunger, maxHunger);
    }

    public void DecreaseHunger(int amount)
    {
        // Decrease current hunger by the specified amount
        currentHunger -= amount;
        // Clamp current hunger to ensure it stays within bounds
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);

        // Trigger hunger changed event
        OnHungerChanged?.Invoke(currentHunger, maxHunger);
    }
}


