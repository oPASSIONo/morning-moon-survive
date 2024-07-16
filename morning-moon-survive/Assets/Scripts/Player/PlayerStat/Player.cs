using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP { get; private set; } = 100;
    public float MaxHP { get; private set; } = 100;
    public float MinHP { get; private set; } = 0;
    public float Stamina { get; private set; } = 100;
    public float MaxStamina { get; private set; } = 100;
    public float MinStamina { get; private set; } = 0;
    public float Satiety { get; private set; } = 50;
    public float SatietyBleeding { get; private set; } = 10;
    public float MaxSatiety { get; private set; } = 100;
    public float MinSatiety { get; private set; } = 0;
    public float Defense { get; private set; } = 10;
    public float[] Resistant { get; private set; } = new float[6] { 0, 0, 0, 0, 0, 0 };
    public float Attack { get; private set; } = 0;
    public float Element { get; private set; } = 0;
    public float[] EXP { get; private set; } = new float[5] { 0, 0, 0, 0, 0 };
    public float Speed { get; private set; } = 5;
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
            staminaComponent.Initialize(MaxStamina,MinStamina,Stamina);
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
            satietyComponent.Initialize(MaxSatiety,MinSatiety,Satiety,SatietyBleeding);
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
        Debug.Log($"Satiety From Player Script : {Satiety}");
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
