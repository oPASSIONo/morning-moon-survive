using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name { get; private set; }
    public int LV { get; private set; }
    public float EXP { get; private set; }
    public bool IsMonster { get; private set; }
    public float HP { get; private set; } = 200;
    public float MaxHP { get; private set; } = 200;
    public float MinHP { get; private set; } = 0;
    public float Defense { get; private set; }
    public float BaseAttack { get; private set; }
    public string[] Element { get; private set; } = new string[6] {"Fire","Ice","Thunder","Toxic","Dark","Unholy" };
    public float[] ElementAttack { get; private set; } = new float[6] { 0, 0, 0, 0, 0, 0 };
    
    public List<GameObject> PoolDrop { get; private set; }
    
    public GameObject[] dropItems; // Array of items to drop upon death
    
    private Health healthComponent;
    
    private void Awake()
    {
        Initialize();
    }
    private void Start()
    {
        // Subscribe to health changed event
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged += UpdateEnemyHealth;
        }
    }
    private void Initialize()
    {
        InitializeHealthComponent();
    }
    private void InitializeHealthComponent()
    {
        healthComponent = GetComponent<Health>(); // Ensure GetComponent is finding the correct component
        if (healthComponent != null)
        {
            healthComponent.Initialize(MaxHP, MinHP, HP);
            healthComponent.OnHealthChanged += UpdateEnemyHealth; // Subscribe to health changed event
        }
        else
        {
            Debug.LogError("Health component not found on Enemy GameObject.");
        }
    }
    private void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        HP=currentHealth;
        Debug.Log($"HP From Enemy Script : {HP}"); 
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        Debug.Log($"Health: {HP}");
        IsDead();
    }

    private void IsDead()
    {
        if (HP <= 0)
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