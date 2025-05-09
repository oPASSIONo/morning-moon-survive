using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float initialHealth = 100f; // Store the initial health value
    private float health;
    [SerializeField] private GameObject[] dropItems; // Array of items to drop when the tree falls
    [SerializeField] private GameObject[] rocks;

    private void Start()
    {
        health = initialHealth; // Initialize health to the initial value
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Rock took {amount} damage. Remaining health: {health}");
        
        CheckState();
    }
    private void DropItem()
    {
        if (dropItems != null && dropItems.Length > 0)
        {
            foreach (GameObject item in dropItems)
            {
                if (item != null)
                {
                    Instantiate(item, transform.position, Quaternion.identity);
                }
            }
        }
    }
    private void CheckState()
    {
        if (health <= (initialHealth / 2)) // Compare with half of the initial health
        {
            //SetRockActive(false);
        }
        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }
    /*private void SetRockActive(bool isActive)
    {
        foreach (GameObject  in )
        {
            if ( != null)
            {
                .SetActive(isActive);
            }
        }
    }*/
}
