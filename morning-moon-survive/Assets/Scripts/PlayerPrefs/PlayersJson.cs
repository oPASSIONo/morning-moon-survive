using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Unity.VisualScripting;
using Newtonsoft.Json;
using UnityEngine;

public class PlayersJson
{
   //PlayerStatsList
   [JsonProperty] public float HP;
   [JsonProperty] public float MaxHP;
   [JsonProperty] public float MinHP;
   [JsonProperty] public float Stamina;
   [JsonProperty] public float MaxStamina;
   [JsonProperty] public float MinStamina;
   [JsonProperty] public float StaminaRegenRate;
   [JsonProperty] public float BaseActionCost;
   [JsonProperty] public float Satiety;
   [JsonProperty] public float SatietyBleeding;
   [JsonProperty] public float SatietyConsumePoint;
   [JsonProperty] public float SatietyConsumeRate;
   [JsonProperty] public float MaxSatiety;
   [JsonProperty] public float MinSatiety;
   [JsonProperty] public float Defense;
   [JsonProperty] public float Resistant;
   [JsonProperty] public float Attack;
   [JsonProperty] public float Element;
   //[JsonProperty] private float EXP;
   [JsonProperty] public float Speed;
   [JsonProperty] public float BaseSpeed;
   [JsonProperty] public float MaxSpeed;
   [JsonProperty] public float MinSpeed;
   
   public void SetPlayerStats(Player stats)
   {
      HP = stats.HP;
      MaxHP = stats.MaxHP;
      MinHP = stats.MinHP;
      Stamina = stats.Stamina;
      MaxStamina = stats.MaxStamina;
      MinStamina = stats.MinStamina;
      StaminaRegenRate = stats.StaminaRegenRate;
      BaseActionCost = stats.BaseActionCost;
      Satiety = stats.Satiety;
      SatietyBleeding = stats.SatietyBleeding;
      SatietyConsumePoint = stats.SatietyConsumePoint;
      SatietyConsumeRate = stats.SatietyConsumeRate;
      MaxSatiety = stats.MaxSatiety;
      MinSatiety = stats.MinSatiety;
      Defense = stats.Defense;
      Resistant = stats.Resistant;
      Attack = stats.Attack;
      Element = stats.Element;
      //EXP = stats.EXP;
      Speed = stats.Speed;
      BaseSpeed = stats.BaseSpeed;
      MaxSpeed = stats.MaxSpeed;
      MinSpeed = stats.MinSpeed;
   }
   
   public ItemJson inventory;

   public void PlayersInventory(ItemJson inventory)
   {
      this.inventory = inventory;
   }

}
