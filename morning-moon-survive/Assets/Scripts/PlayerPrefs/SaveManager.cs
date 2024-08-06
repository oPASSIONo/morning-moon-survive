using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SavePlayer() 
    {
        PlayerPrefs.SetFloat("HP", Player.Instance.HP);
        PlayerPrefs.SetFloat("MaxHP", Player.Instance.MaxHP);
        PlayerPrefs.SetFloat("MinHP", Player.Instance.MinHP);
        PlayerPrefs.SetFloat("Stamina", Player.Instance.Stamina);
        PlayerPrefs.SetFloat("MaxStamina", Player.Instance.MaxStamina);
        PlayerPrefs.SetFloat("MinStamina", Player.Instance.MinStamina);
        PlayerPrefs.SetFloat("BaseActionCost", Player.Instance.BaseActionCost);
        PlayerPrefs.SetFloat("Satiety", Player.Instance.Satiety);
        PlayerPrefs.SetFloat("SatietyBleeding", Player.Instance.SatietyBleeding);
        PlayerPrefs.SetFloat("SatietyConsumePoint", Player.Instance.SatietyConsumePoint);
        PlayerPrefs.SetFloat("SatietyConsumeRate", Player.Instance.SatietyConsumeRate);
        PlayerPrefs.SetFloat("MaxSatiety", Player.Instance.MaxSatiety);
        PlayerPrefs.SetFloat("MinSatiety", Player.Instance.MinSatiety);
        
        PlayerPrefs.SetInt("Day", TimeManager.Instance.dayCount);
        
        PlayerPrefs.Save(); // Ensure PlayerPrefs data is saved to disk
        
        Debug.Log($"Save HP: {PlayerPrefs.GetFloat("HP")}");
        Debug.Log($"Save MaxHP: {PlayerPrefs.GetFloat("MaxHP")}");
        Debug.Log($"Save MinHP: {PlayerPrefs.GetFloat("MinHP")}");
        Debug.Log($"Save Stamina: {PlayerPrefs.GetFloat("Stamina")}");
        Debug.Log($"Save MaxStamina: {PlayerPrefs.GetFloat("MaxStamina")}");
        Debug.Log($"Save MinStamina: {PlayerPrefs.GetFloat("MinStamina")}");
        Debug.Log($"Save Satiety: {PlayerPrefs.GetFloat("Satiety")}");
        Debug.Log($"Save Day: {PlayerPrefs.GetInt("Day")}");
    }

    public void LoadPlayer()
    {
        if (PlayerPrefs.HasKey("HP"))
        {
            Player.Instance.SetHP(PlayerPrefs.GetFloat("HP"));
            Player.Instance.SetMaxHP(PlayerPrefs.GetFloat("MaxHP"));
            Player.Instance.SetMinHP(PlayerPrefs.GetFloat("MinHP"));
            Player.Instance.SetStamina(PlayerPrefs.GetFloat("Stamina"));
            Player.Instance.SetMaxStamina(PlayerPrefs.GetFloat("MaxStamina"));
            Player.Instance.SetMinStamina(PlayerPrefs.GetFloat("MinStamina"));
            Player.Instance.SetSatiety(PlayerPrefs.GetFloat("Satiety"));
        
            TimeManager.Instance.SetDayCount(PlayerPrefs.GetInt("Day"));
        
            Debug.Log($"Load HP: {PlayerPrefs.GetFloat("HP")}");
            Debug.Log($"Load MaxHP: {PlayerPrefs.GetFloat("MaxHP")}");
            Debug.Log($"Load MinHP: {PlayerPrefs.GetFloat("MinHP")}");
            Debug.Log($"Load Stamina: {PlayerPrefs.GetFloat("Stamina")}");
            Debug.Log($"Load MaxStamina: {PlayerPrefs.GetFloat("MaxStamina")}");
            Debug.Log($"Load MinStamina: {PlayerPrefs.GetFloat("MinStamina")}");
            Debug.Log($"Load Satiety: {PlayerPrefs.GetFloat("Satiety")}");
            Debug.Log($"Load Day: {PlayerPrefs.GetInt("Day")}");
        }
        else
        {
            Debug.LogError("No saved data found!");
        }
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted all PlayerPrefs data.");
    }
}
