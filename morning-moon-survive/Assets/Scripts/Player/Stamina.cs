using System.Collections;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float MaxStamina { get; private set; }
    public float MinStamina { get; private set; }
    public float CurrentStamina { get; private set; }
    public float BaseActionCost { get; private set; }
    public float RegenRate { get; private set; } // Stamina regen per second
    private float regenDelay = 0.5f; // Time to wait before starting regeneration

    public event System.Action<float, float> OnStaminaChanged;

    private Coroutine regenCoroutine;

    void Start()
    {
        CurrentStamina = MaxStamina;
        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);
    }

    public void Initialize(float maxStamina, float minStamina, float initialStamina, float baseActionCost, float staminaRegenRate)
    {
        MaxStamina = maxStamina;
        MinStamina = minStamina;
        CurrentStamina = initialStamina;
        BaseActionCost = baseActionCost;
        RegenRate = staminaRegenRate;
        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);
    }

    private IEnumerator RegenerateStamina()
    {
        // Wait for the regen delay before starting regeneration
        yield return new WaitForSeconds(regenDelay);
        
        while (CurrentStamina < MaxStamina)
        {
            // Regenerate stamina over time
            float regenAmount = RegenRate * Time.deltaTime;
            CurrentStamina = Mathf.Min(CurrentStamina + regenAmount, MaxStamina);
            OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);
            yield return null; // Wait for the next frame
        }

        regenCoroutine = null; // Reset the coroutine reference when done
    }

    public bool CanPerformAction(float staminaCost)
    {
        // Check if there's enough stamina to perform the action
        return CurrentStamina >= staminaCost;
    }

    public void ConsumeStamina(float staminaCost)
    {
        // Consume stamina when performing an action
        CurrentStamina = Mathf.Max(CurrentStamina - staminaCost, 0f);
        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);

        // Stop the current regeneration coroutine if it's running
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
        }

        // Start a new regeneration coroutine
        regenCoroutine = StartCoroutine(RegenerateStamina());
    }

    public void IncreaseStamina(int amount)
    {
        // Increase stamina by the specified amount
        CurrentStamina = Mathf.Min(CurrentStamina + amount, MaxStamina);
        OnStaminaChanged?.Invoke(CurrentStamina, MaxStamina);
    }

    public void TakeAction()
    {
        // Check if there's enough stamina to perform the attack
        if (CanPerformAction(CalculatedActionCost()))
        {
            // Consume stamina
            ConsumeStamina(CalculatedActionCost());
        }
        else
        {
            // Handle case when there's not enough stamina to perform the attack
            Debug.Log("Not enough stamina to perform the attack!");
        }
    }

    private float CalculatedActionCost()
    {
        float actionCost = BaseActionCost;
        // Logic to calculate the Action cost
        return actionCost;
    }
}
