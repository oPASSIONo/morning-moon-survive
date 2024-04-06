using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float MaxStamina { get; private set; } = 100f;
    public float CurrentStamina { get; private set; }
    private float staminaRegenRate = 10f;
    
    private Stamina stamina;//test

    // Start is called before the first frame update
    void Start()
    {
        CurrentStamina = MaxStamina;
        
        stamina = GetComponent<Stamina>();//test
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateStamina();
    }

    void RegenerateStamina()
    {
        // Regenerate stamina over time
        CurrentStamina = Mathf.Min(CurrentStamina + (staminaRegenRate * Time.deltaTime), MaxStamina);
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
    }

    public void IncreaseStamina(int amount)
    {
        // Increase stamina by the specified amount
        CurrentStamina = Mathf.Min(CurrentStamina + amount, MaxStamina);
    }
    
    
    public void TestAction()
    {
        // Check if there's enough stamina to perform the attack
        float attackStaminaCost=10f;//test
        if (stamina.CanPerformAction(attackStaminaCost))
        {
            // Consume stamina
            stamina.ConsumeStamina(attackStaminaCost);

            Debug.Log("Action");
        }
        else
        {
            // Handle case when there's not enough stamina to perform the attack
            Debug.Log("Not enough stamina to perform the attack!");
        }
    }
}
