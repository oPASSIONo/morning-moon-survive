using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float initialHealth = 100f; // Store the initial health value
    private float health;
    public GameObject[] dropItems; // Array of items to drop when the tree falls
    [SerializeField] private GameObject[] leaf;

    
    private void Start()
    {
        health = initialHealth; // Initialize health to the initial value
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Tree took {amount} damage. Remaining health: {health}");
        
        CheckState();
        /*if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }*/
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
            SetLeavesActive(false);
        }
        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }
    private void SetLeavesActive(bool isActive)
    {
        foreach (GameObject leafObject in leaf)
        {
            if (leafObject != null)
            {
                leafObject.SetActive(isActive);
            }
        }
    }
}