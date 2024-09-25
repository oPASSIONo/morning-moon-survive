using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        PlayerPrefs.SetFloat("PlayerPosX", Player.Instance.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", Player.Instance.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", Player.Instance.transform.position.z);
    }

    private void SaveDayAndTime()
    {
        PlayerPrefs.SetInt("Day", TimeManager.Instance.DayCount);
        PlayerPrefs.SetFloat("Time",TimeManager.Instance.CurrentTimeOfDay);
    }

    private void SavePlayerStat()
    {
        PlayerPrefs.SetFloat("HP", Player.Instance.HP);
        PlayerPrefs.SetFloat("MaxHP", Player.Instance.MaxHP);
        PlayerPrefs.SetFloat("MinHP", Player.Instance.MinHP);
        PlayerPrefs.SetFloat("Stamina", Player.Instance.Stamina);
        PlayerPrefs.SetFloat("MaxStamina", Player.Instance.MaxStamina);
        PlayerPrefs.SetFloat("MinStamina", Player.Instance.MinStamina);
        PlayerPrefs.SetFloat("StaminaRegenRate", Player.Instance.StaminaRegenRate);
        PlayerPrefs.SetFloat("BaseActionCost", Player.Instance.BaseActionCost);
        PlayerPrefs.SetFloat("Satiety", Player.Instance.Satiety);
        PlayerPrefs.SetFloat("SatietyBleeding", Player.Instance.SatietyBleeding);
        PlayerPrefs.SetFloat("SatietyConsumePoint", Player.Instance.SatietyConsumePoint);
        PlayerPrefs.SetFloat("SatietyConsumeRate", Player.Instance.SatietyConsumeRate);
        PlayerPrefs.SetFloat("MaxSatiety", Player.Instance.MaxSatiety);
        PlayerPrefs.SetFloat("MinSatiety", Player.Instance.MinSatiety);
        PlayerPrefs.SetFloat("Defense", Player.Instance.Defense);
        PlayerPrefs.SetFloat("Resistant", Player.Instance.Resistant);
        PlayerPrefs.SetFloat("Attack", Player.Instance.Attack);
        PlayerPrefs.SetFloat("Element", Player.Instance.Element);
        //PlayerPrefsX.SetFloatArray("EXP", Player.Instance.EXP); // Assuming a utility method for saving arrays
        PlayerPrefs.SetFloat("Speed", Player.Instance.Speed);
        PlayerPrefs.SetFloat("BaseSpeed", Player.Instance.BaseSpeed);
        PlayerPrefs.SetFloat("MaxSpeed", Player.Instance.MaxSpeed);
        PlayerPrefs.SetFloat("MinSpeed", Player.Instance.MinSpeed);
        /*PlayerPrefsX.SetFloatList("Buff", Player.Instance.Buff); // Assuming a utility method for saving lists
        PlayerPrefsX.SetFloatList("Debuff", Player.Instance.Debuff); // Assuming a utility method for saving lists
        PlayerPrefsX.SetFloatList("ItemSlot", Player.Instance.ItemSlot); // Assuming a utility method for saving lists*/
        PlayerPrefs.SetInt("Weight", Player.Instance.Weight);
        PlayerPrefs.SetInt("InventorySlot", Player.Instance.InventorySlot);
    }

    private void LoadPlayerStat()
    {
        Player.Instance.SetHP(PlayerPrefs.GetFloat("HP"));
        Player.Instance.SetMaxHP(PlayerPrefs.GetFloat("MaxHP"));
        Player.Instance.SetMinHP(PlayerPrefs.GetFloat("MinHP"));
        Player.Instance.SetStamina(PlayerPrefs.GetFloat("Stamina"));
        Player.Instance.SetMaxStamina(PlayerPrefs.GetFloat("MaxStamina"));
        Player.Instance.SetMinStamina(PlayerPrefs.GetFloat("MinStamina"));
        Player.Instance.SetStaminaRegenRate(PlayerPrefs.GetFloat("StaminaRegenRate"));
        Player.Instance.SetBaseActionCost(PlayerPrefs.GetFloat("BaseActionCost"));
        Player.Instance.SetSatiety(PlayerPrefs.GetFloat("Satiety"));
        Player.Instance.SetSatietyBleeding(PlayerPrefs.GetFloat("SatietyBleeding"));
        Player.Instance.SetSatietyConsumePoint(PlayerPrefs.GetFloat("SatietyConsumePoint"));
        Player.Instance.SetSatietyConsumeRate(PlayerPrefs.GetFloat("SatietyConsumeRate"));
        Player.Instance.SetMaxSatiety(PlayerPrefs.GetFloat("MaxSatiety"));
        Player.Instance.SetMinSatiety(PlayerPrefs.GetFloat("MinSatiety"));
        Player.Instance.SetDefense(PlayerPrefs.GetFloat("Defense"));
        Player.Instance.SetResistant(PlayerPrefs.GetFloat("Resistant"));
        Player.Instance.SetAttack(PlayerPrefs.GetFloat("Attack"));
        Player.Instance.SetElement(PlayerPrefs.GetFloat("Element"));
        //Player.Instance.SetEXP(PlayerPrefsX.GetFloatArray("EXP"));
        Player.Instance.SetSpeed(PlayerPrefs.GetFloat("Speed"));
        Player.Instance.SetBaseSpeed(PlayerPrefs.GetFloat("BaseSpeed"));
        Player.Instance.SetMaxSpeed(PlayerPrefs.GetFloat("MaxSpeed"));
        Player.Instance.SetMinSpeed(PlayerPrefs.GetFloat("MinSpeed"));
        /*Player.Instance.SetBuff(PlayerPrefsX.GetFloatList("Buff"));
        Player.Instance.SetDebuff(PlayerPrefsX.GetFloatList("Debuff"));
        Player.Instance.SetItemSlot(PlayerPrefsX.GetFloatList("ItemSlot"));*/
        Player.Instance.SetWeight(PlayerPrefs.GetInt("Weight"));
        Player.Instance.SetInventorySlot(PlayerPrefs.GetInt("InventorySlot"));
    }
    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            Vector3 playerPosition = new Vector3(x, y, z);
            Player.Instance.GetComponent<NavMeshAgent>().Warp(playerPosition);
        }
        else
        {
            Debug.LogWarning("No saved player position found.");
        }
    }
    private void LoadDayAndTime()
    {
        TimeManager.Instance.SetDayCount(PlayerPrefs.GetInt("Day"));
        TimeManager.Instance.CurrentTimeOfDay=(PlayerPrefs.GetFloat("Time"));
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
