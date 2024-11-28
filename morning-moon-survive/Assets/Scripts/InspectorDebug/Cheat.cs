using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    //public Player player;
    
    public static Cheat Instance { get; set; }
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

    /*public void RegenPlayerHP()
    {
        player.SetHP(player.MaxHP);
    }*/
}
