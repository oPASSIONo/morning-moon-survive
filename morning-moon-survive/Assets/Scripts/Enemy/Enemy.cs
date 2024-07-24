using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public EnemyStatsSO enemyStatsSO;
    
    public List<GameObject> PoolDrop { get; private set; }
    
    public GameObject[] dropItems; // Array of items to drop upon death
    
    public Health healthComponent;
    
    public bool IsMonster { get; private set; }
    
    public string Name { get; private set; }
    public float HP { get; private set; }
    public float MaxHP { get; private set; }
    public float MinHP { get; private set; }
    public float Defense { get; private set; }
    public float BaseATK { get; private set; }
    public Element ElementATK { get; private set; }
    public float ElementATKDMG { get; private set; }
    
    private EnemyStatsSO.AttackTypeWeaknesses _attackWeaknesses;
    private EnemyStatsSO.ElementTypeWeaknesses _elementWeaknesses;
    
    
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
        InitialzeStat();
        InitializeHealthComponent();
    }

    private void InitialzeStat()
    {
        HP = enemyStatsSO.HP;
        MinHP = enemyStatsSO.MinHP;
        MaxHP = enemyStatsSO.MaxHP;
        Defense = enemyStatsSO.Defense;
        BaseATK = enemyStatsSO.BaseATK;
        Name = enemyStatsSO.Name;
        IsMonster = enemyStatsSO.IsMonster;
        ElementATK = enemyStatsSO.ElementATK;
        ElementATKDMG = enemyStatsSO.ElementATKDMG;

        _attackWeaknesses = enemyStatsSO.AttackWeaknesses;
        _elementWeaknesses = enemyStatsSO.ElementWeaknesses;
        
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
    
    /*public void TakeDamage(float amount)
    {
        HP -= amount;
        Debug.Log($"{Name} took {amount} damage. Remaining health: {HP}");
        IsDead();
    }*/
    public int GetAttackTypeWeaknessRank(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Chop: return _attackWeaknesses.Chop;
            case AttackType.Blunt: return _attackWeaknesses.Blunt;
            case AttackType.Pierce: return _attackWeaknesses.Pierce;
            case AttackType.Slash: return _attackWeaknesses.Slash;
            case AttackType.Ammo: return _attackWeaknesses.Ammo;
            default: return 0;
        }
    }

    public int GetElementTypeWeaknessRank(Element element)
    {
        switch (element)
        {
            case Element.Thunder: return _elementWeaknesses.Thunder;
            case Element.Fire: return _elementWeaknesses.Fire;
            case Element.Ice: return _elementWeaknesses.Ice;
            case Element.Toxic: return _elementWeaknesses.Toxic;
            case Element.Dark: return _elementWeaknesses.Dark;
            case Element.Unholy: return _elementWeaknesses.Unholy;
            case Element.None: return 1;
            default: return 0;
        }
    }
}