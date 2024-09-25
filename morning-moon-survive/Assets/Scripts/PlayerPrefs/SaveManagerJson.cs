using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
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

        Enemy enemy = FindObjectOfType<Enemy>();

        if (enemy == null)
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
        playersJson.PlayersInventory(new ItemJson("Type","Name",0));
        worldDataJSON.playersJsons.Add(playersJson);

        var monsterJSON = new MonstersJson();
        monsterJSON.MonstersList(new MonsterStatsJSON(0,0,0,0,0,0,0));
        //worldDataJSON.monstersJsons.Add(monsterJSON);
        
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
        }
        if (PlayerPrefs.HasKey("WorldData"))
        {
            string worldAsJSON = PlayerPrefs.GetString("WorldData");
            
            WorldDataListJson loadedWorldData = JsonConvert.DeserializeObject<WorldDataListJson>(worldAsJSON);
        
            Debug.Log("Load JSON: " + worldAsJSON);
            
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                PlayersJson loadedPlayerJson = loadedWorldData.playersJsons[0];

                // ตั้งค่า Stats ของ Player กลับมา
                player.HP = loadedPlayerJson.HP;
                Debug.Log("Player HP after load: " + player.HP);
                player.MaxHP = loadedPlayerJson.MaxHP;
                player.MinHP = loadedPlayerJson.MinHP;
                player.Stamina = loadedPlayerJson.Stamina;
                player.MaxStamina = loadedPlayerJson.MaxStamina;
                player.MinStamina = loadedPlayerJson.MinStamina;
                player.Defense = loadedPlayerJson.Defense;
                player.Attack = loadedPlayerJson.Attack;
                player.Speed = loadedPlayerJson.Speed;
                player.BaseSpeed = loadedPlayerJson.BaseSpeed;

                Debug.Log("Player stats loaded successfully.");
            }
            else
            {
                Debug.LogError("ไม่พบ Player ในซีน");
            }
        }
        else
        {
            Debug.Log("ไม่พบข้อมูลใน PlayerPrefs");
        }
    }

}
