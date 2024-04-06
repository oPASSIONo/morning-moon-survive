using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    private float staminaRegenRate = 10f;

    private Stamina stamina;//test

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        
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
        currentStamina = Mathf.Min(currentStamina + (staminaRegenRate * Time.deltaTime), maxStamina);
    }

    public bool CanPerformAction(float staminaCost)
    {
        // Check if there's enough stamina to perform the action
        return currentStamina >= staminaCost;
    }

    public void ConsumeStamina(float staminaCost)
    {
        // Consume stamina when performing an action
        currentStamina = Mathf.Max(currentStamina - staminaCost, 0f);
    }

    public void IncreaseStamina(int amount)
    {
        // Increase stamina by the specified amount
        currentStamina = Mathf.Min(currentStamina + amount, maxStamina);
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
