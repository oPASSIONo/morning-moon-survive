using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    public GameObject[] dropItems; // Array of items to drop when the tree falls

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Tree took {amount} damage. Remaining health: {health}");

        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    public void DropItem()
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
}