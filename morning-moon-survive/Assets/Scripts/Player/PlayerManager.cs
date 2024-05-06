using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEditor;
using MorningMoonSurvive.Core.Singletons;

public class PlayerManager : Singleton<PlayerManager>
{
    private NetworkVariable<int> playerIngame = new NetworkVariable<int>();

    public int PlayerInGame
    {
        get
        {
            return playerIngame.Value;
        }
    }

    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                playerIngame.Value++;
            }
        };
        
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                playerIngame.Value--;
            }
        };
    }

    /*private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                playerIngame.Value++;
            }
        };
        
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                playerIngame.Value--;
            }
        };
    }*/
}
