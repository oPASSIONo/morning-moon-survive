using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class Satiety : MonoBehaviour
{
    public float MaxSatiety { get; private set; }
    public float MinSatiety { get; private set; }
    public float CurrentSatiety { get; private set;}
    public float SatietyBleeding { get; private set; }
    private float damageInterval = 1f; // Time interval between damage ticks
    private float damageAmount = 1; // Amount of damage per tick
    private Coroutine damageCoroutine;
    public event Action<float, float> OnSatietyChanged; // Event to notify hunger changes

    private Health healthComponent; // Reference to the Health component for the player
    private float nextDamageTime; // Time of the next damage tick

    private void Awake()
    {
        healthComponent = GetComponent<Health>(); // Initialize the health reference
    }

    void Start()
    {
        // Initialize the time of the next damage tick
        damageCoroutine = StartCoroutine(SatietyCheck());

    }
    
    private IEnumerator SatietyCheck()
    {
        while (true)
        {
            if (CurrentSatiety <= 0)
            {
                healthComponent.Die();
                yield break; // Stop the coroutine
            }
            else if (CurrentSatiety <= SatietyBleeding)
            {
                if (healthComponent.CurrentHealth > healthComponent.MinHealth)
                {
                    // Apply damage to the player's health
                    if (healthComponent != null)
                    {
                        healthComponent.TakeDamage(damageAmount);
                    }
                }
            }
            yield return new WaitForSeconds(damageInterval); // Wait for the next damage tick
        }
    }

    public void Initialize(float maxSatiety, float minSatiety, float initialSatiety,float satietyBleeding)
    {
        MaxSatiety = maxSatiety;
        MinSatiety = minSatiety;
        CurrentSatiety = initialSatiety;
        SatietyBleeding = satietyBleeding;
        
        OnSatietyChanged?.Invoke(CurrentSatiety,MaxSatiety);
    }
    
    public void DecreaseSatiety(float amount)
    {
        CurrentSatiety -= amount;
        if (CurrentSatiety<=MinSatiety)
        {
            CurrentSatiety = MinSatiety;
        }
        // Trigger hunger changed event
        OnSatietyChanged?.Invoke(CurrentSatiety, MaxSatiety);
    }

    public void IncreaseSatiety(float amount)
    {
        CurrentSatiety += amount;
        if (CurrentSatiety > MaxSatiety)
        {
            CurrentSatiety = MaxSatiety;
        }
        // Trigger hunger changed event
        OnSatietyChanged?.Invoke(CurrentSatiety, MaxSatiety);
    }
    
    
}
