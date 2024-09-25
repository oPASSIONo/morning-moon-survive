using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class LoadManagerJson : MonoBehaviour
{
    /*public static LoadManagerJson Instance { get; private set; }
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
    
    public void LoadGames()
    {
        if (PlayerPrefs.HasKey("PlayerStats"))
        {
            string entityAsJSON = PlayerPrefs.GetString("PlayerStats");

            EntityJson loadedEnity = JsonConvert.DeserializeObject<EntityJson>(entityAsJSON);
            
            Debug.Log("Load JSON: " + entityAsJSON);
        }
        else
        {
            Debug.Log("ไม่พบข้อมูลใน PlayerPrefs");
        }

    }*/
}
