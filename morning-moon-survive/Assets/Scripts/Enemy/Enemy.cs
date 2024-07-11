using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 100f;
    public GameObject[] dropItems; // Array of items to drop upon death

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");
        IsDead();
    }

    private void IsDead()
    {
        if (health <= 0)
        {
            DropRandomItem();
            Destroy(gameObject);
        }
    }

    private void DropRandomItem()
    {
        if (dropItems != null && dropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, dropItems.Length);
            GameObject itemToDrop = dropItems[randomIndex];

            if (itemToDrop != null)
            {
                Instantiate(itemToDrop, transform.position, Quaternion.identity);
            }
        }
    }
}