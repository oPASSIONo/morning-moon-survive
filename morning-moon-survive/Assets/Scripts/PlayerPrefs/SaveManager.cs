using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
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

    private GameObject GetLocalPlayer()
    {
        foreach (var networkObject in NetworkManager.Singleton.SpawnManager.SpawnedObjectsList)
        {
            if (networkObject.IsOwner && networkObject.CompareTag("Player"))
            {
                return networkObject.gameObject;
            }
        }
        Debug.LogError("Local player not found!");
        return null;
    }
    public void SavePlayer() 
    {
        SavePlayerStat();
        SaveDayAndTime();
        SavePlayerPosition();
        //DebugSave();
    }
    
    public void LoadPlayer()
    {
        LoadPlayerStat();
        LoadDayAndTime();
        LoadPlayerPosition();
        //DebugLoad();
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All saved data has been deleted.");
    }

    private void SavePlayerPosition()
    {
        GameObject localPlayer = GetLocalPlayer();
        if (localPlayer == null) return;
        Transform playerTransform = localPlayer.transform;
        PlayerPrefs.SetFloat("PlayerPosX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerTransform.position.z);
        
        /*PlayerPrefs.SetFloat("PlayerPosX", Player.Instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", Player.Instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", Player.Instance.transform.position.z);*/
    }

    private void SaveDayAndTime()
    {
        PlayerPrefs.SetInt("Day", TimeManager.Instance.dayCount.Value);
        PlayerPrefs.SetFloat("Time",TimeManager.Instance.currentTimeOfDay.Value);
    }

    private void SavePlayerStat()
    {
        GameObject localPlayer = GetLocalPlayer();
        if (localPlayer == null) return;

        Player player = localPlayer.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player component not found on local player!");
            return;
        }
        
        PlayerPrefs.SetFloat("HP", player.HP);
        PlayerPrefs.SetFloat("MaxHP", player.MaxHP);
        PlayerPrefs.SetFloat("MinHP", player.MinHP);
        PlayerPrefs.SetFloat("Stamina", player.Stamina);
        PlayerPrefs.SetFloat("MaxStamina", player.MaxStamina);
        PlayerPrefs.SetFloat("MinStamina", player.MinStamina);
        PlayerPrefs.SetFloat("StaminaRegenRate", player.StaminaRegenRate);
        PlayerPrefs.SetFloat("BaseActionCost", player.BaseActionCost);
        PlayerPrefs.SetFloat("Satiety", player.Satiety);
        PlayerPrefs.SetFloat("SatietyBleeding", player.SatietyBleeding);
        PlayerPrefs.SetFloat("SatietyConsumePoint", player.SatietyConsumePoint);
        PlayerPrefs.SetFloat("SatietyConsumeRate", player.SatietyConsumeRate);
        PlayerPrefs.SetFloat("MaxSatiety", player.MaxSatiety);
        PlayerPrefs.SetFloat("MinSatiety", player.MinSatiety);
        PlayerPrefs.SetFloat("Defense", player.Defense);
        PlayerPrefs.SetFloat("Resistant", player.Resistant);
        PlayerPrefs.SetFloat("Attack", player.Attack);
        PlayerPrefs.SetFloat("Element", player.Element);
        //PlayerPrefsX.SetFloatArray("EXP", Player.Instance.EXP); // Assuming a utility method for saving arrays
        PlayerPrefs.SetFloat("Speed", player.Speed);
        PlayerPrefs.SetFloat("BaseSpeed", player.BaseSpeed);
        PlayerPrefs.SetFloat("MaxSpeed", player.MaxSpeed);
        PlayerPrefs.SetFloat("MinSpeed", player.MinSpeed);
        /*PlayerPrefsX.SetFloatList("Buff", Player.Instance.Buff); // Assuming a utility method for saving lists
        PlayerPrefsX.SetFloatList("Debuff", Player.Instance.Debuff); // Assuming a utility method for saving lists
        PlayerPrefsX.SetFloatList("ItemSlot", Player.Instance.ItemSlot); // Assuming a utility method for saving lists*/
        PlayerPrefs.SetInt("Weight", player.Weight);
        PlayerPrefs.SetInt("InventorySlot", player.InventorySlot);
    }

    private void LoadPlayerStat()
    {
        GameObject localPlayer = GetLocalPlayer();
        if (localPlayer == null) return;

        Player player = localPlayer.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player component not found on local player!");
            return;
        }
        
        player.SetHP(PlayerPrefs.GetFloat("HP"));
        player.SetMaxHP(PlayerPrefs.GetFloat("MaxHP"));
        player.SetMinHP(PlayerPrefs.GetFloat("MinHP"));
        player.SetStamina(PlayerPrefs.GetFloat("Stamina"));
        player.SetMaxStamina(PlayerPrefs.GetFloat("MaxStamina"));
        player.SetMinStamina(PlayerPrefs.GetFloat("MinStamina"));
        player.SetStaminaRegenRate(PlayerPrefs.GetFloat("StaminaRegenRate"));
        player.SetBaseActionCost(PlayerPrefs.GetFloat("BaseActionCost"));
        player.SetSatiety(PlayerPrefs.GetFloat("Satiety"));
        player.SetSatietyBleeding(PlayerPrefs.GetFloat("SatietyBleeding"));
        player.SetSatietyConsumePoint(PlayerPrefs.GetFloat("SatietyConsumePoint"));
        player.SetSatietyConsumeRate(PlayerPrefs.GetFloat("SatietyConsumeRate"));
        player.SetMaxSatiety(PlayerPrefs.GetFloat("MaxSatiety"));
        player.SetMinSatiety(PlayerPrefs.GetFloat("MinSatiety"));
        player.SetDefense(PlayerPrefs.GetFloat("Defense"));
        player.SetResistant(PlayerPrefs.GetFloat("Resistant"));
        player.SetAttack(PlayerPrefs.GetFloat("Attack"));
        player.SetElement(PlayerPrefs.GetFloat("Element"));
        //Player.Instance.SetEXP(PlayerPrefsX.GetFloatArray("EXP"));
        player.SetSpeed(PlayerPrefs.GetFloat("Speed"));
        player.SetBaseSpeed(PlayerPrefs.GetFloat("BaseSpeed"));
        player.SetMaxSpeed(PlayerPrefs.GetFloat("MaxSpeed"));
        player.SetMinSpeed(PlayerPrefs.GetFloat("MinSpeed"));
        /*Player.Instance.SetBuff(PlayerPrefsX.GetFloatList("Buff"));
        Player.Instance.SetDebuff(PlayerPrefsX.GetFloatList("Debuff"));
        Player.Instance.SetItemSlot(PlayerPrefsX.GetFloatList("ItemSlot"));*/
        player.SetWeight(PlayerPrefs.GetInt("Weight"));
        player.SetInventorySlot(PlayerPrefs.GetInt("InventorySlot"));
    }
    private void LoadPlayerPosition()
    {
        GameObject localPlayer = GetLocalPlayer();
        if (localPlayer == null) return;
        
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            Vector3 playerPosition = new Vector3(x, y, z);
            NavMeshAgent agent = localPlayer.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(playerPosition);
            }
            else
            {
                localPlayer.transform.position = playerPosition;
            }
            //Player.Instance.GetComponent<NavMeshAgent>().Warp(playerPosition);
        }
        else
        {
            Debug.LogWarning("No saved player position found.");
        }
    }
    private void LoadDayAndTime()
    {
        TimeManager.Instance.SetDayCount(PlayerPrefs.GetInt("Day"));
        TimeManager.Instance.currentTimeOfDay.Value =(PlayerPrefs.GetFloat("Time"));
    }

    private void DebugLoad()
    {
        // Debug logs for loaded values
        Debug.Log($"Loaded HP: {PlayerPrefs.GetFloat("HP")}");
        Debug.Log($"Loaded MaxHP: {PlayerPrefs.GetFloat("MaxHP")}");
        Debug.Log($"Loaded MinHP: {PlayerPrefs.GetFloat("MinHP")}");
        Debug.Log($"Loaded Stamina: {PlayerPrefs.GetFloat("Stamina")}");
        Debug.Log($"Loaded MaxStamina: {PlayerPrefs.GetFloat("MaxStamina")}");
        Debug.Log($"Loaded MinStamina: {PlayerPrefs.GetFloat("MinStamina")}");
        Debug.Log($"Loaded StaminaRegenRate: {PlayerPrefs.GetFloat("StaminaRegenRate")}");
        Debug.Log($"Loaded BaseActionCost: {PlayerPrefs.GetFloat("BaseActionCost")}");
        Debug.Log($"Loaded Satiety: {PlayerPrefs.GetFloat("Satiety")}");
        Debug.Log($"Loaded SatietyBleeding: {PlayerPrefs.GetFloat("SatietyBleeding")}");
        Debug.Log($"Loaded SatietyConsumePoint: {PlayerPrefs.GetFloat("SatietyConsumePoint")}");
        Debug.Log($"Loaded SatietyConsumeRate: {PlayerPrefs.GetFloat("SatietyConsumeRate")}");
        Debug.Log($"Loaded MaxSatiety: {PlayerPrefs.GetFloat("MaxSatiety")}");
        Debug.Log($"Loaded MinSatiety: {PlayerPrefs.GetFloat("MinSatiety")}");
        Debug.Log($"Loaded Defense: {PlayerPrefs.GetFloat("Defense")}");
        Debug.Log($"Loaded Resistant: {PlayerPrefs.GetFloat("Resistant")}");
        Debug.Log($"Loaded Attack: {PlayerPrefs.GetFloat("Attack")}");
        Debug.Log($"Loaded Element: {PlayerPrefs.GetFloat("Element")}");
        //Debug.Log($"Loaded EXP: {string.Join(", ", PlayerPrefsX.GetFloatArray("EXP"))}");
        Debug.Log($"Loaded Speed: {PlayerPrefs.GetFloat("Speed")}");
        Debug.Log($"Loaded BaseSpeed: {PlayerPrefs.GetFloat("BaseSpeed")}");
        Debug.Log($"Loaded MaxSpeed: {PlayerPrefs.GetFloat("MaxSpeed")}");
        Debug.Log($"Loaded MinSpeed: {PlayerPrefs.GetFloat("MinSpeed")}");
        /*Debug.Log($"Loaded Buff: {string.Join(", ", PlayerPrefsX.GetFloatList("Buff"))}");
        Debug.Log($"Loaded Debuff: {string.Join(", ", PlayerPrefsX.GetFloatList("Debuff"))}");
        Debug.Log($"Loaded ItemSlot: {string.Join(", ", PlayerPrefsX.GetFloatList("ItemSlot"))}");*/
        Debug.Log($"Loaded Weight: {PlayerPrefs.GetInt("Weight")}");
        Debug.Log($"Loaded InventorySlot: {PlayerPrefs.GetInt("InventorySlot")}");
        Debug.Log($"Loaded Day: {PlayerPrefs.GetInt("Day")}");
        Debug.Log($"Loaded Position: {PlayerPrefs.GetFloat("PlayerPosX")}");
        Debug.Log($"Loaded Position: {PlayerPrefs.GetFloat("PlayerPosY")}");
        Debug.Log($"Loaded Position: {PlayerPrefs.GetFloat("PlayerPosZ")}");

    }

    private void DebugSave()
    {
        
        // Debug logs for saved values
        Debug.Log($"Saved HP: {PlayerPrefs.GetFloat("HP")}");
        Debug.Log($"Saved MaxHP: {PlayerPrefs.GetFloat("MaxHP")}");
        Debug.Log($"Saved MinHP: {PlayerPrefs.GetFloat("MinHP")}");
        Debug.Log($"Saved Stamina: {PlayerPrefs.GetFloat("Stamina")}");
        Debug.Log($"Saved MaxStamina: {PlayerPrefs.GetFloat("MaxStamina")}");
        Debug.Log($"Saved MinStamina: {PlayerPrefs.GetFloat("MinStamina")}");
        Debug.Log($"Saved StaminaRegenRate: {PlayerPrefs.GetFloat("StaminaRegenRate")}");
        Debug.Log($"Saved BaseActionCost: {PlayerPrefs.GetFloat("BaseActionCost")}");
        Debug.Log($"Saved Satiety: {PlayerPrefs.GetFloat("Satiety")}");
        Debug.Log($"Saved SatietyBleeding: {PlayerPrefs.GetFloat("SatietyBleeding")}");
        Debug.Log($"Saved SatietyConsumePoint: {PlayerPrefs.GetFloat("SatietyConsumePoint")}");
        Debug.Log($"Saved SatietyConsumeRate: {PlayerPrefs.GetFloat("SatietyConsumeRate")}");
        Debug.Log($"Saved MaxSatiety: {PlayerPrefs.GetFloat("MaxSatiety")}");
        Debug.Log($"Saved MinSatiety: {PlayerPrefs.GetFloat("MinSatiety")}");
        Debug.Log($"Saved Defense: {PlayerPrefs.GetFloat("Defense")}");
        Debug.Log($"Saved Resistant: {PlayerPrefs.GetFloat("Resistant")}");
        Debug.Log($"Saved Attack: {PlayerPrefs.GetFloat("Attack")}");
        Debug.Log($"Saved Element: {PlayerPrefs.GetFloat("Element")}");
        //Debug.Log($"Saved EXP: {string.Join(", ", PlayerPrefsX.GetFloatArray("EXP"))}");
        Debug.Log($"Saved Speed: {PlayerPrefs.GetFloat("Speed")}");
        Debug.Log($"Saved BaseSpeed: {PlayerPrefs.GetFloat("BaseSpeed")}");
        Debug.Log($"Saved MaxSpeed: {PlayerPrefs.GetFloat("MaxSpeed")}");
        Debug.Log($"Saved MinSpeed: {PlayerPrefs.GetFloat("MinSpeed")}");
        /*Debug.Log($"Saved Buff: {string.Join(", ", PlayerPrefsX.GetFloatList("Buff"))}");
        Debug.Log($"Saved Debuff: {string.Join(", ", PlayerPrefsX.GetFloatList("Debuff"))}");
        Debug.Log($"Saved ItemSlot: {string.Join(", ", PlayerPrefsX.GetFloatList("ItemSlot"))}");*/
        Debug.Log($"Saved Weight: {PlayerPrefs.GetInt("Weight")}");
        Debug.Log($"Saved InventorySlot: {PlayerPrefs.GetInt("InventorySlot")}");
        Debug.Log($"Saved Day: {PlayerPrefs.GetInt("Day")}");
        Debug.Log($"Saved Position: {PlayerPrefs.GetFloat("PlayerPosX")}");
        Debug.Log($"Saved Position: {PlayerPrefs.GetFloat("PlayerPosY")}");
        Debug.Log($"Saved Position: {PlayerPrefs.GetFloat("PlayerPosZ")}");
    }
}
