using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float MaxStamina { get; private set; }
    public float MinStamina { get; private set; }
    public float CurrentStamina { get; private set; }
    private float staminaRegenRate = 10f;
    private float actionStaminaCost = 10;

    public event System.Action<float, float> OnStaminaChanged;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateStamina();
    }

    public void Initialize(float maxStamina,float minStamina,float initialStamina)
    {
        MaxStamina = maxStamina;
        MinStamina = minStamina;
        CurrentStamina = initialStamina;
        
        OnStaminaChanged?.Invoke(CurrentStamina,MaxStamina);
    }

    void RegenerateStamina()
    {
        // Regenerate stamina over time
        CurrentStamina = Mathf.Min(CurrentStamina + (staminaRegenRate * Time.deltaTime), MaxStamina);
        OnStaminaChanged?.Invoke(CurrentStamina,MaxStamina);
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
        OnStaminaChanged?.Invoke(CurrentStamina,MaxStamina);
    }

    public void IncreaseStamina(int amount)
    {
        // Increase stamina by the specified amount
        CurrentStamina = Mathf.Min(CurrentStamina + amount, MaxStamina);
    }
    
    
    public void TakeAction()
    {
        // Check if there's enough stamina to perform the attack
        if (CanPerformAction(actionStaminaCost))
        {
            // Consume stamina
            ConsumeStamina(actionStaminaCost);
        }
        else
        {
            // Handle case when there's not enough stamina to perform the attack
            Debug.Log("Not enough stamina to perform the attack!");
        }
    }
}
