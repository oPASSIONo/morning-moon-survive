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
    [field: SerializeField] public int ChopWeakness { get; set; }
    [field: SerializeField] public int BluntWeakness { get; set; }
    [field: SerializeField] public int PierceWeakness { get; set; }
    [field: SerializeField] public int SlashWeakness { get; set; }
    [field: SerializeField] public int AmmoWeakness { get; set; }
    [field: SerializeField] public int ThunderWeakness { get; set; }
    [field: SerializeField] public int FireWeakness { get; set; }
    [field: SerializeField] public int IceWeakness { get; set; }
    [field: SerializeField] public int ToxicWeakness { get; set; }
    [field: SerializeField] public int DarkWeakness { get; set; }
    [field: SerializeField] public int UnholyWeakness { get; set; }
    
    
}
