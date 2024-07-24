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
    
    public int ChopWeakness { get; set; }
    public int BluntWeakness { get; set; }
    public int PierceWeakness { get; set; }
    public int SlashWeakness { get; set; }
    public int AmmoWeakness { get; set; }
    
    public int ThunderWeakness { get; set; }
    public int FireWeakness { get; set; }
    public int IceWeakness { get; set; }
    public int ToxicWeakness { get; set; }
    public int DarkWeakness { get; set; }
    public int UnholyWeakness { get; set; }

    //public Collider bodyCollider;
    //public Collider weakPointCollider;
    
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
        
        ChopWeakness = enemyStatsSO.ChopWeakness;
        BluntWeakness = enemyStatsSO.BluntWeakness;
        PierceWeakness = enemyStatsSO.PierceWeakness;
        SlashWeakness = enemyStatsSO.SlashWeakness;
        AmmoWeakness = enemyStatsSO.AmmoWeakness;

        ThunderWeakness = enemyStatsSO.ThunderWeakness;
        FireWeakness = enemyStatsSO.FireWeakness;
        IceWeakness = enemyStatsSO.IceWeakness;
        ToxicWeakness = enemyStatsSO.ToxicWeakness;
        DarkWeakness = enemyStatsSO.DarkWeakness;
        UnholyWeakness = enemyStatsSO.UnholyWeakness;

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
    
    public void TakeDamage(float amount)
    {
        HP -= amount;
        Debug.Log($"Tree took {amount} damage. Remaining health: {HP}");
        IsDead();
    }

    public int GetAttackTypeWeaknessRank(AttackType attackType)
    {
        int weaknessRank = 0;
        switch (attackType)
        {
            case AttackType.Chop:
                weaknessRank = ChopWeakness;
                break;
            case AttackType.Blunt:
                weaknessRank = BluntWeakness;
                break;
            case AttackType.Pierce:
                weaknessRank = PierceWeakness;
                break;
            case AttackType.Slash:
                weaknessRank = SlashWeakness;
                break;
            case AttackType.Ammo:
                weaknessRank = AmmoWeakness;
                break;
                
        }
        return weaknessRank;
    }
    
    public int GetElementTypeWeaknessRank(Element element)
    {
        int weaknessRank = 0;
        switch (element)
        {
            case Element.Thunder:
                weaknessRank = ThunderWeakness;
                break;
            case Element.Fire:
                weaknessRank = FireWeakness;
                break;
            case Element.Ice:
                weaknessRank = IceWeakness;
                break;
            case Element.Toxic:
                weaknessRank = ToxicWeakness;
                break;
            case Element.Dark:
                weaknessRank = DarkWeakness;
                break;
            case Element.Unholy:
                weaknessRank = UnholyWeakness;
                break;
            case Element.None:
                weaknessRank = 1;
                break;
                
        }
        return weaknessRank;
    }
}