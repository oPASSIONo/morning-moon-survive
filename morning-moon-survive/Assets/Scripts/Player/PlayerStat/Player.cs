using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
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
    public float BaseSpeed { get; private set; }
    public float MaxSpeed { get; private set; }
    public float MinSpeed { get; private set; }
    public List<float> Buff { get; private set; }
    public List<float> Debuff { get; private set; }
    public List<float> ItemSlot { get; private set; }
    public int Weight { get; private set; }
    public int InventorySlot { get; private set; }

    public void SetHP(float value) => healthComponent.SetCurrentHealth(value);
    public void SetMaxHP(float value) => healthComponent.SetMaxHealth(value);
    public void SetMinHP(float value) => healthComponent.SetMinHealth(value);
    public void SetStamina(float value) => staminaComponent.SetCurrentStamina(value);
    public void SetMaxStamina(float value) => staminaComponent.SetMaxStamina(value);
    public void SetMinStamina(float value) => staminaComponent.SetMinStamina(value);
    public void SetStaminaRegenRate(float value) => staminaComponent.SetRegenRate(value);
    public void SetBaseActionCost(float value) => staminaComponent.SetBaseActionCost(value);
    public void SetSatiety(float value) => satietyComponent.SetCurrentSatiety(value);
    public void SetMaxSatiety(float value) => satietyComponent.SetMaxSatiety(value);
    public void SetMinSatiety(float value) => satietyComponent.SetMinSatiety(value);
    public void SetSatietyBleeding(float value) => satietyComponent.SetSatietyBleeding(value);
    public void SetSatietyConsumeRate(float value) => satietyComponent.SetSatietyConsumeRate(value);
    public void SetSatietyConsumePoint(float value) => satietyComponent.SetSatietyConsumePoint(value);
    public void SetDefense(float value) => Defense = value;
    public void SetResistant(float value) => Resistant = value;
    public void SetAttack(float value) => Attack = value;
    public void SetElement(float value) => Element = value;
    public void SetEXP(float[] value) => EXP = value;
    public void SetSpeed(float value) => playerMovementComponent.SetCurrentSpeed(value);
    public void SetBaseSpeed(float value) => playerMovementComponent.SetBaseSpeed(value);
    public void SetMaxSpeed(float value) =>  playerMovementComponent.SetMaxSpeed(value);
    public void SetMinSpeed(float value) =>  playerMovementComponent.SetMinSpeed(value);
    public void SetBuff(List<float> value) => Buff = value;
    public void SetDebuff(List<float> value) => Debuff = value;
    public void SetItemSlot(List<float> value) => ItemSlot = value;
    public void SetWeight(int value) => Weight = value;
    public void SetInventorySlot(int value) => InventorySlot = value;

    private Health healthComponent;
    private Stamina staminaComponent;
    private Satiety satietyComponent;
    private PlayerMovement playerMovementComponent;
    
    [SerializeField]
    private Transform rootTransform; // Assign the root GameObject in the inspector
    public Transform RootTransform => rootTransform;
    
   
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Initialize();
        //currentState = PlayerState.Normal;
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
        BaseSpeed = playerStats.SpeedStat.BaseSpeed;
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
            playerMovementComponent.OnSpeedChanged += UpdatePlayerSpeed;
            playerMovementComponent.Initialize(MaxSpeed, MinSpeed, BaseSpeed);
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
            staminaComponent.OnStaminaChanged += UpdatePlayerStamina;
            staminaComponent.Initialize(MaxStamina,MinStamina,Stamina,BaseActionCost,StaminaRegenRate);
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
            healthComponent.OnHealthChanged += UpdatePlayerHealth; // Subscribe to health changed event
            healthComponent.Initialize(MaxHP, MinHP, HP);
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
            satietyComponent.OnSatietyChanged += UpdatePlayerSatiety;
            satietyComponent.Initialize(MaxSatiety,MinSatiety,Satiety,SatietyBleeding,SatietyConsumePoint,SatietyConsumeRate);
        }
        else
        {
            Debug.LogError("Satiety component not found on Player GameObject.");
        }
    }
    private void UpdatePlayerSpeed(float currentSpeed, float maxSpeed)
    {
        Speed = currentSpeed;
        MaxSpeed = maxSpeed;

    }
    private void UpdatePlayerSatiety(float currentSatiety,float maxSatiety)
    {
        Satiety = currentSatiety;
        MaxSatiety = maxSatiety;
    }
    private void UpdatePlayerStamina(float currentStamina, float maxStamina)
    {
        Stamina = currentStamina;
        MaxStamina = maxStamina;
    }
    private void UpdatePlayerHealth(float currentHealth, float maxHealth,float minHealth)
    {
        HP = currentHealth;
        MaxHP = maxHealth;
        MinHP = minHealth;
    }
   
    public PlayerStats GetPlayerStatSO()
    {
        return playerStats;
    }
}
