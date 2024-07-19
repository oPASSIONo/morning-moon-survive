using System;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    
    public float damageMultiplier = 1f; // Adjust this for different damage multipliers
    
    private Enemy enemy; // Reference to the parent Enemy component
    private Health healthComponent;

    

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("DamageZone script must be attached to a child GameObject of an Enemy.");
        }

        healthComponent = GetComponentInParent<Health>();
        if (healthComponent==null)
        {
            Debug.LogError("DamageZone script must be attached to a child GameObject of an Health.");
        }
    }

    // Method to apply damage based on the zone type
    public void ApplyDamageToEnemy(float baseDamage)
    {
        float totalDamage = baseDamage * damageMultiplier;
        // Apply damage to the enemy's health
        healthComponent.TakeDamage(totalDamage);
        // Optionally, you can add effects or animations specific to this zone here
    }
    
}