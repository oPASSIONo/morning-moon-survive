using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Tree class that manages health and the visibility of parts based on health.
/// </summary>
public class Tree : MonoBehaviour
{
    private float initialHealth = 100f; // Initial health of the tree
    private float health; // Current health of the tree
    [SerializeField] private GameObject[] dropItems; // Array of items to drop when the tree falls
    [SerializeField] private List<GameObject> partsOfObject; // List of parts game objects

    private void Start()
    {
        health = initialHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Tree took {amount} damage. Remaining health: {health}");

        // Update the state of the parts based on the current health
        UpdateParts();

        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
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

    // Method to update the state of the parts based on the current health
    private void UpdateParts()
    {
        float healthPercentage = (health / initialHealth) * 100f;

        // Deactivate parts based on health thresholds
        if (partsOfObject != null && partsOfObject.Count > 0)
        {
            for (int i = 0; i < partsOfObject.Count; i++)
            {
                float threshold = ((float)(i + 1) / partsOfObject.Count) * 100f;
                if (healthPercentage <= threshold)
                {
                    SetPartActive(i, false);
                }
            }
        }
    }

    // Helper method to set the active state of a specific part
    private void SetPartActive(int partIndex, bool isActive)
    {
        if (partIndex >= 0 && partIndex < partsOfObject.Count)
        {
            partsOfObject[partIndex].SetActive(isActive);
        }
    }
}
