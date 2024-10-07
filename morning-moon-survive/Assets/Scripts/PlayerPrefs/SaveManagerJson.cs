using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SaveManagerJson : MonoBehaviour
{
    public TimeManager time;

    public static SaveManagerJson Instance { get; set; }
    private void Awake()
    {
        if (Instance!=null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SaveGames()
    {
        Player player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("ไม่พบ Player ในซีน");
            return;
        }

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
        {
            Debug.Log("ไม่พบ Enemy ในซีน");
            return;
        }

        WorldDataListJson worldDataJSON = new WorldDataListJson();

        var enviromentsJSON = new EnviromentsJson();
        enviromentsJSON.SetTime(time);
        worldDataJSON.enviromentsJsons.Add(enviromentsJSON);

        var playersJson = new PlayersJson();
        playersJson.SetPlayerStats(player);
        playersJson.PlayersInventory(new ItemJson("Type", "Name", 0));
        worldDataJSON.playersJsons.Add(playersJson);

        var monsterJSON = new MonsterStatsJSON();
        foreach (var enemy in enemies)
        {
            // Save health from the Health component
            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                enemy.enemyStatsSO.HP = health.CurrentHealth;
                enemy.enemyStatsSO.MaxHP = health.MaxHealth;
                enemy.enemyStatsSO.MinHP = health.MinHealth;
            }
            monsterJSON.SetMonstersStats(enemy);
        }
        worldDataJSON.monstersJsons.Add(monsterJSON);
        
        var worldAsJSON = JsonConvert.SerializeObject(worldDataJSON);

        //Save JSON To PlayerPrefs
        PlayerPrefs.SetString("WorldData", worldAsJSON);
        PlayerPrefs.Save();

        Debug.Log("Save JSON: " + worldAsJSON);
    }

    public void LoadGames()
    {

        if (PlayerPrefs.HasKey("WorldData"))
        {
            string worldAsJSON = PlayerPrefs.GetString("WorldData");
            WorldDataListJson loadedWorldData = JsonConvert.DeserializeObject<WorldDataListJson>(worldAsJSON);

            EnviromentsJson loadedTime = loadedWorldData.enviromentsJsons[0];

            time.CurrentTimeOfDay = loadedTime.Time;
            time.DayCount = loadedTime.Day;

            Debug.Log("Load JSON: " + worldAsJSON);
            
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                PlayersJson loadedPlayerJson = loadedWorldData.playersJsons[0];

                // ตั้งค่า Stats ของ Player กลับมา
                player.SetHP(loadedPlayerJson.HP);
                player.SetMaxHP(loadedPlayerJson.MaxHP);
                player.SetMinHP(loadedPlayerJson.MinHP);
                player.SetStamina(loadedPlayerJson.Stamina);
                player.SetMaxStamina(loadedPlayerJson.MaxStamina);
                player.SetMinStamina(loadedPlayerJson.MinStamina);
                player.SetSatiety(loadedPlayerJson.Satiety);
                player.SetMaxSatiety(loadedPlayerJson.MaxSatiety);
                player.SetMinSatiety(loadedPlayerJson.MinSatiety);
                player.SetDefense(loadedPlayerJson.Defense);
                player.SetAttack(loadedPlayerJson.Attack);
                player.SetSpeed(loadedPlayerJson.Speed);
                player.SetBaseSpeed(loadedPlayerJson.BaseSpeed);

                Debug.Log("Player stats loaded successfully.");
            }
            else
            {
                Debug.LogError("ไม่พบ Player ในซีน");
            }

            /*foreach (var enemyStats in loadedWorldData.monstersJsons)
            {
                Enemy enemy = FindObjectOfType<Enemy>();
                if (enemy != null)
                {
                    Health health = enemy.GetComponent<Health>();
                    if (health != null)
                    {
                        health.SetMaxHealth(enemyStats.MaxHealth);
                        health.SetMinHealth(enemyStats.MinHealth);
                        health.SetCurrentHealth(enemyStats.CurrentHealth);
                    }
                }
            }*/
            Debug.Log("ไม่พบข้อมูลใน PlayerPrefs");
        }
    }

}
