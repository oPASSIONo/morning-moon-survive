using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP { get; private set; } = 100f;
    public float MaxHP { get; private set; } = 100f;
    public float MinHP { get; private set; } = 0f;
    public float Stamina { get; private set; } = 100f;
    public float MaxStamina { get; private set; } = 100f;
    public float MinStamina { get; private set; } = 0f;
    public float StaminaRegenRate { get; private set; } = 25f;
    public float BaseActionCost { get; private set; } = 20f; 
    public float Satiety { get; private set; } = 50f;
    public float SatietyBleeding { get; private set; } = 10f;
    public float SatietyConsumePoint { get; private set; } = 1f;
    public float SatietyConsumeRate { get; private set; } = 5f;
    public float MaxSatiety { get; private set; } = 100f;
    public float MinSatiety { get; private set; } = 0f;
    public float Defense { get; private set; } = 10f;
    public float[] Resistant { get; private set; } = new float[6] { 0f, 0f, 0f, 0f, 0f, 0f };
    public float Attack { get; private set; } = 0f;
    public float Element { get; private set; } = 0f;
    public float[] EXP { get; private set; } = new float[5] { 0f, 0f, 0f, 0f, 0f };
    public float Speed { get; private set; } = 5f;
    public float MaxSpeed { get; private set; }
    public float MinSpeed { get; private set; }
    public List<float> Buff { get; private set; }
    public List<float> Debuff { get; private set; }
    public List<float> ItemSlot { get; private set; }
    //public ... Equipment {get; private set;}
    public int Weight { get; private set; }
    public int InventorySlot { get; private set; } = 20;

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
        InitializeHealthComponent();
        InitializeStaminaComponent();
        InitializedSatietyComponent();
        InitializePlayerMovementComponent();
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
