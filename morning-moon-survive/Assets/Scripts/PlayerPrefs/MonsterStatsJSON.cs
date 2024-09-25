using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MonsterStatsJSON : MonoBehaviour
{
    [JsonProperty] private float HP;
    [JsonProperty] private float MaxHP;
    [JsonProperty] private float MinHP;
    [JsonProperty] private float Defense;
    [JsonProperty] private float BaseATK;
    [JsonProperty] private float PositionX;
    [JsonProperty] private float PositionZ;

    public void SetMonstersStats(Enemy monstersStats)
    {
        HP = monstersStats.HP;
        MaxHP = monstersStats.MaxHP;
        MinHP = monstersStats.MinHP;
        Defense = monstersStats.Defense;
        BaseATK = monstersStats.BaseATK;
    }

    private List<MonsterStatsJSON> monsterStats;

    public MonsterStatsJSON(float hp, float maxHp, float minHp, float defense, float baseAtk, float positionX, float positionZ)
    {
        this.HP = hp;
        this.MaxHP = maxHp;
        this.MinHP = minHp;
        this.Defense = defense;
        this.BaseATK = baseAtk;
        this.PositionX = positionX;
        this.PositionZ = positionZ;

        monsterStats = new List<MonsterStatsJSON>();
    }
}
