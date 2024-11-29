using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManagerMultiplayer : NetworkBehaviour
{
    public static GameManagerMultiplayer Instance { get; private set; }
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);    
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
