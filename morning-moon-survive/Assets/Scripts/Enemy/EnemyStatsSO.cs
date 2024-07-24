using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStatsSO : ScriptableObject
{
    [field: SerializeField] public bool IsMonster { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public float HP { get; set; }
    [field: SerializeField] public float MaxHP { get; set; }
    [field: SerializeField] public float MinHP { get; set; }
    [field: SerializeField] public float Defense { get; set; }
    [field: SerializeField] public float BaseATK { get; set; }
    [field: SerializeField] public Element ElementATK { get; set; }
    [field: SerializeField] public float ElementATKDMG { get; set; }
    [field: SerializeField] public AttackTypeWeaknesses AttackWeaknesses { get; set; }
    [field: SerializeField] public ElementTypeWeaknesses ElementWeaknesses { get; set; }
    
    
    [System.Serializable]
    public struct AttackTypeWeaknesses
    {
        public int Chop;
        public int Blunt;
        public int Pierce;
        public int Slash;
        public int Ammo;
    }

    [System.Serializable]
    public struct ElementTypeWeaknesses
    {
        public int Thunder;
        public int Fire;
        public int Ice;
        public int Toxic;
        public int Dark;
        public int Unholy;
    }
}
