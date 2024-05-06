using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class Hunger : NetworkBehaviour
{
    public int MaxHunger { get; private set; } = 100;
    public int CurrentHunger { get; private set; }
    private float damageInterval = 1f; // Time interval between damage ticks
    private int damageAmount = 1; // Amount of damage per tick

    public event Action<int, int> OnHungerChanged; // Event to notify hunger changes

    private Health health; // Reference to the Health component for the player

    private float nextDamageTime; // Time of the next damage tick

    private void Awake()
    {
        health = GetComponent<Health>(); // Initialize the health reference
    }

    void Start()
    {
        if (IsServer)
        // Initialize current hunger to 0
        CurrentHunger = 0;

        // Initialize the time of the next damage tick
        nextDamageTime = Time.time + damageInterval;
    }

    void Update()
    {
        // Check if hunger has reached the maximum value
        if (CurrentHunger >= MaxHunger)
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
        CurrentHunger += amount;
        // Clamp current hunger to ensure it stays within bounds
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, MaxHunger);

        // Trigger hunger changed event
        OnHungerChanged?.Invoke(CurrentHunger, MaxHunger);
    }

    public void DecreaseHunger(int amount)
    {
        // Decrease current hunger by the specified amount
        CurrentHunger -= amount;
        // Clamp current hunger to ensure it stays within bounds
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, MaxHunger);

        // Trigger hunger changed event
        OnHungerChanged?.Invoke(CurrentHunger, MaxHunger);
    }
    
}
