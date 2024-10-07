using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDataListJson
{
    public List<PlayersJson> playersJsons;
    public List<MonsterStatsJSON> monstersJsons;
    public List<EnviromentsJson> enviromentsJsons;

    public WorldDataListJson()
    {
        playersJsons = new List<PlayersJson>();
        monstersJsons = new List<MonsterStatsJSON>();
        enviromentsJsons = new List<EnviromentsJson>();
    }
}
