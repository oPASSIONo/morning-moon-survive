using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MonstersJson
{
    [JsonProperty] private string Name;
    [JsonProperty] private float Amount;

    public MonsterStatsJSON monsterStats;

    public void MonstersList(MonsterStatsJSON monsterStats)
    {
        this.monsterStats = monsterStats;
    }
}
