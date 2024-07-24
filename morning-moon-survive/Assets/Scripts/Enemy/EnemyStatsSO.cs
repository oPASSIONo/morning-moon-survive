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
    [field: SerializeField] public int chopWeakness { get; set; }
    [field: SerializeField] public int bluntWeakness { get; set; }
    [field: SerializeField] public int pierceWeakness { get; set; }
    [field: SerializeField] public int slashWeakness { get; set; }
    [field: SerializeField] public int ammoWeakness { get; set; }
    
    
}
