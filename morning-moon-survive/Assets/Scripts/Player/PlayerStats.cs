using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [field: SerializeField] public HealthStats HealthStat { get; private set; }
    [field: SerializeField] public StaminaStats StaminaStat { get; private set; }
    [field: SerializeField] public SatietyStats SatietyStat { get; private set; }
    [field: SerializeField] public SpeedStats SpeedStat { get; private set; }
    [field: SerializeField] public CombatStats CombatStat { get; private set; }
    [field: SerializeField] public float BaseActionCost { get; private set; } 
    
    [field: SerializeField] public float[] EXP { get; private set; }
    
    [field: SerializeField] public List<float> Buff { get; private set; }
    [field: SerializeField] public List<float> Debuff { get; private set; }
    [field: SerializeField] public List<float> ItemSlot { get; private set; }
    [field: SerializeField] public int Weight { get; private set; }
    [field: SerializeField] public int InventorySlot { get; private set; } = 20;
    
    [System.Serializable]
    public struct HealthStats
    {
        public float HP;
        public float MaxHP;
        public float MinHP;
    }
    [System.Serializable]
    public struct StaminaStats
    {
        public float Stamina;
        public float MaxStamina;
        public float MinStamina;
        public float StaminaRegenRate;
    }
    [System.Serializable]
    public struct SatietyStats
    {
        public float Satiety;
        public float MaxSatiety;
        public float MinSatiety;
        public float SatietyBleeding;
        public float SatietyConsumePoint;
        public float SatietyConsumeRate;
    }
    [System.Serializable]
    public struct SpeedStats
    {
        public float Speed;
        public float BaseSpeed;
        public float MaxSpeed;
        public float MinSpeed;
    }
    [System.Serializable]
    public struct CombatStats
    {
        public float Attack;
        public float Defense;
        public float Element;
        public float Resistant;
    }
}
