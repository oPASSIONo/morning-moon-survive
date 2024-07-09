using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public enum ZoneType
    {
        Head,
        Body,
        Tail
    }

    public ZoneType zoneType;
    public float damageMultiplier = 1f; // Adjust this for different damage multipliers

    private Enemy enemy; // Reference to the parent Enemy component

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("DamageZone script must be attached to a child GameObject of an Enemy.");
        }
    }

    // Method to apply damage based on the zone type
    public void ApplyDamage(float baseDamage)
    {
        float totalDamage = baseDamage * damageMultiplier;
        // Apply damage to the enemy's health
        enemy.TakeDamage(totalDamage);
        // Optionally, you can add effects or animations specific to this zone here
        Debug.Log($"Dealing {totalDamage} damage to {zoneType}");
    }
}