using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MonsterStatsJSON
{
    public Dictionary<string, EnemyStatsSO> MonsterStatsList = new Dictionary<string, EnemyStatsSO>();

    public void SetMonstersStats(Enemy monstersStats)
    {
        if (monstersStats.enemyStatsSO == null)
        {
            Debug.LogError("EnemyStatsSO is null for the enemy: " + monstersStats.name);
            return;
        }

        string uniqueKey = monstersStats.enemyStatsSO.Name + "_" + monstersStats.GetInstanceID();

        if (!MonsterStatsList.ContainsKey(uniqueKey))
        {
            MonsterStatsList[uniqueKey] = monstersStats.enemyStatsSO;
        }
        else
        {
            Debug.LogWarning("Monster with uniqueKey " + uniqueKey + " already exists.");
        }
    }
}
