using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Implement health check or death logic here
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Example: Handle enemy death (e.g., play death animation, disable GameObject)
        gameObject.SetActive(false); // Temporary example
    }
}