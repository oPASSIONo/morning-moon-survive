using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestHealthBar : MonoBehaviour
{
    /*public int maxHealth = 100;
    public int currentHealth;
    
    public float visibleDuration = 4.0f;
    public float destroyParticle = 3.0f;

    public HealthBar healthBar;
    public GameObject healthBarshow;
    
    public GameObject damageParticlePrefab;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthBar.SetHealth(currentHealth);
        healthBarshow.SetActive(true);
        
        DamagePopup.current.CreatePopup(transform.position, damage.ToString());
        
        GameObject particleInstance = Instantiate(damageParticlePrefab, transform.position, Quaternion.identity);
        Destroy(particleInstance, destroyParticle);

        StartCoroutine(HideHealthBar());
    }

    private IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(visibleDuration);
        healthBarshow.SetActive(false);
    }*/
}
