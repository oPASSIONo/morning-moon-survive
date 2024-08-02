using System.Collections;
using UnityEngine;
using System;

public class Satiety : MonoBehaviour
{
    public float MaxSatiety { get; private set; }
    public float MinSatiety { get; private set; }
    public float CurrentSatiety { get; private set; }
    public float SatietyBleeding { get; private set; }
    public float SatietyConsumeRate { get; private set; }
    public float SatietyConsumePoint { get; private set; }
    public float InitialDelay { get; private set; } // Delay before starting the satiety decrease


    private Coroutine satietyDecreaseCoroutine;

    public event Action<float, float> OnSatietyChanged;

    private Health healthComponent; // Reference to the Health component for the player

    private void Awake()
    {
        healthComponent = GetComponent<Health>(); // Initialize the health reference
    }

    public void InitialSatietyConsumeOvertime()
    {
        satietyDecreaseCoroutine = StartCoroutine(DecreaseSatietyOverTime());
    }
    public void Initialize(float maxSatiety, float minSatiety, float initialSatiety, float satietyBleeding, float satietyConsumePoint, float satietyConsumeRate)
    {
        MaxSatiety = maxSatiety;
        MinSatiety = minSatiety;
        CurrentSatiety = initialSatiety;
        SatietyBleeding = satietyBleeding;
        SatietyConsumePoint = satietyConsumePoint;
        SatietyConsumeRate = satietyConsumeRate;
        InitialDelay = SatietyConsumeRate;
        OnSatietyChanged?.Invoke(CurrentSatiety, MaxSatiety);

        // Start the coroutine for decreasing satiety gradually
        satietyDecreaseCoroutine = StartCoroutine(DecreaseSatietyOverTime());
    }

    private IEnumerator DecreaseSatietyOverTime()
    {
        yield return new WaitForSeconds(InitialDelay);

        while (CurrentSatiety > SatietyConsumePoint && healthComponent.CurrentHealth>0)
        {
            
            DecreaseSatiety(SatietyConsumePoint); // Decrease by SatietyConsumeRate amount

            yield return new WaitForSeconds(SatietyConsumeRate); // Wait for SatietyConsumeRate seconds before next decrease
        }

        // Ensure CurrentSatiety is not below MinSatiety
        CurrentSatiety = Mathf.Max(CurrentSatiety, MinSatiety);

        // Trigger event after satiety decrease completes
        OnSatietyChanged?.Invoke(CurrentSatiety, MaxSatiety);

        // Optionally, you can restart the coroutine or handle completion based on your game logic
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

    private void OnDestroy()
    {
        if (satietyDecreaseCoroutine != null)
        {
            StopCoroutine(satietyDecreaseCoroutine);
        }
    }

    public void SetCurrentSatiety(float value)
    {
        CurrentSatiety = value;
        OnSatietyChanged?.Invoke(CurrentSatiety,MaxSatiety);
    }
    public void SetMaxSatiety(float value)
    {
        MaxSatiety = value;
        OnSatietyChanged?.Invoke(CurrentSatiety, MaxSatiety);
    }

    public void SetMinSatiety(float value)
    {
        MinSatiety = value;
        OnSatietyChanged?.Invoke(CurrentSatiety, MaxSatiety);
    }

    public void SetSatietyBleeding(float value)
    {
        SatietyBleeding = value;
    }

    public void SetSatietyConsumeRate(float value)
    {
        SatietyConsumeRate = value;
    }

    public void SetSatietyConsumePoint(float value)
    {
        SatietyConsumePoint = value;
    }
}
