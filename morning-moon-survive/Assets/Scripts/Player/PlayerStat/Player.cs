using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    public float HP { get; private set; }
    public float MaxHP { get; private set; } 
    public float MinHP { get; private set; }
    public float Stamina { get; private set; } 
    public float MaxStamina { get; private set; } 
    public float MinStamina { get; private set; }
    public float StaminaRegenRate { get; private set; } 
    public float BaseActionCost { get; private set; }  
    public float Satiety { get; private set; }
    public float SatietyBleeding { get; private set; }
    public float SatietyConsumePoint { get; private set; }
    public float SatietyConsumeRate { get; private set; }
    public float MaxSatiety { get; private set; }
    public float MinSatiety { get; private set; }
    public float Defense { get; private set; }
    public float Resistant { get; private set; } 
    public float Attack { get; private set; }
    public float Element { get; private set; }
    public float[] EXP { get; private set; }
    public float Speed { get; private set; }
    public float MaxSpeed { get; private set; }
    public float MinSpeed { get; private set; }
    public List<float> Buff { get; private set; }
    public List<float> Debuff { get; private set; }
    public List<float> ItemSlot { get; private set; }
    public int Weight { get; private set; }
    public int InventorySlot { get; private set; } 

    private Health healthComponent;
    private Stamina staminaComponent;
    private Satiety satietyComponent;
    private PlayerMovement playerMovementComponent;
    
    [SerializeField]
    private Transform rootTransform; // Assign the root GameObject in the inspector
    public Transform RootTransform => rootTransform;
    
    private void Awake()
    {
        Initialize();
    }
    

    private void Initialize()
    {
        InitializePlayerStat();
        InitializeHealthComponent();
        InitializeStaminaComponent();
        InitializedSatietyComponent();
        InitializePlayerMovementComponent();
    }

    private void InitializePlayerStat()
    {
        HP = playerStats.HealthStat.HP;
        MaxHP = playerStats.HealthStat.MaxHP;
        MinHP = playerStats.HealthStat.MinHP;
        Stamina = playerStats.StaminaStat.Stamina;
        MaxStamina = playerStats.StaminaStat.MaxStamina;
        MinStamina = playerStats.StaminaStat.MinStamina;
        StaminaRegenRate = playerStats.StaminaStat.StaminaRegenRate;
        BaseActionCost = playerStats.BaseActionCost;
        Satiety = playerStats.SatietyStat.Satiety;
        SatietyBleeding = playerStats.SatietyStat.SatietyBleeding;
        SatietyConsumePoint = playerStats.SatietyStat.SatietyConsumePoint;
        SatietyConsumeRate = playerStats.SatietyStat.SatietyConsumeRate;
        MaxSatiety = playerStats.SatietyStat.MaxSatiety;
        MinSatiety = playerStats.SatietyStat.MinSatiety;
        Defense = playerStats.CombatStat.Defense;
        Resistant = playerStats.CombatStat.Resistant;
        Attack = playerStats.CombatStat.Attack;
        Element = playerStats.CombatStat.Element;
        EXP = playerStats.EXP;
        Speed = playerStats.SpeedStat.Speed;
        MaxSpeed = playerStats.SpeedStat.MaxSpeed;
        MinSpeed = playerStats.SpeedStat.MinSpeed;
        Buff = new List<float>(playerStats.Buff);
        Debuff = new List<float>(playerStats.Debuff);
        ItemSlot = new List<float>(playerStats.ItemSlot);
        Weight = playerStats.Weight;
        InventorySlot = playerStats.InventorySlot;
        
    }

    private void InitializePlayerMovementComponent()
    {
        playerMovementComponent = GetComponent<PlayerMovement>();
        if (playerMovementComponent != null)
        {
            playerMovementComponent.Initialize(MaxSpeed, MinSpeed, Speed);
            playerMovementComponent.OnSpeedChanged += UpdatePlayerSpeed;
        }
        else
        {
            Debug.LogError("PlayerMovement component not found on Player GameObject.");
        }
    }
    
    private void InitializeStaminaComponent()
    {
        staminaComponent = GetComponent<Stamina>();
        if (staminaComponent!=null)
        {
            staminaComponent.Initialize(MaxStamina,MinStamina,Stamina,BaseActionCost,StaminaRegenRate);
            staminaComponent.OnStaminaChanged += UpdatePlayerStamina;
        }
        else
        {
            Debug.LogError("Stamina component not found on Player GameObject.");
        }
    }

    private void InitializeHealthComponent()
    {
        healthComponent = GetComponent<Health>(); // Ensure GetComponent is finding the correct component
        if (healthComponent != null)
        {
            healthComponent.Initialize(MaxHP, MinHP, HP);
            healthComponent.OnHealthChanged += UpdatePlayerHealth; // Subscribe to health changed event
        }
        else
        {
            Debug.LogError("Health component not found on Player GameObject.");
        }
    }

    private void InitializedSatietyComponent()
    {
        satietyComponent = GetComponent<Satiety>();
        if (satietyComponent!=null)
        {
            satietyComponent.Initialize(MaxSatiety,MinSatiety,Satiety,SatietyBleeding,SatietyConsumePoint,SatietyConsumeRate);
            satietyComponent.OnSatietyChanged += UpdatePlayerSatiety;
        }
        else
        {
            Debug.LogError("Satiety component not found on Player GameObject.");
        }
    }
    private void UpdatePlayerSpeed(float currentSpeed, float maxSpeed)
    {
        Speed = currentSpeed;
        Debug.Log($"Speed From Player Script : {Speed}");
    }
    private void UpdatePlayerSatiety(float currentSatiety,float maxSatiety)
    {
        Satiety = currentSatiety;
    }
    private void UpdatePlayerStamina(float currentStamina, float maxStamina)
    {
        Stamina = currentStamina;
    }
    private void UpdatePlayerHealth(float currentHealth, float maxHealth)
    {
        HP = currentHealth;
        Debug.Log($"HP From Player Script : {HP}"); 
    }
}
