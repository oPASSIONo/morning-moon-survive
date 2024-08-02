using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public PlayerStats playerdefault;
    
    public void SavePlayer() 
    {
        PlayerPrefs.SetFloat("HP",Player.Instance.HP);
        PlayerPrefs.SetFloat("MaxHp",Player.Instance.MaxHP);
        PlayerPrefs.SetFloat("MinHp", Player.Instance.MinHP);
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
        /*PlayerPrefs.SetFloat("Defense", player.Defense);
        PlayerPrefs.SetFloat("Resistant", player.Resistant);
        PlayerPrefs.SetFloat("Attack", player.Attack);
        PlayerPrefs.SetFloat("Element", player.Element);
        PlayerPrefs.SetFloat("EXP", player.EXP);*/
        
        //PlayerPrefs.SetFloat("Time", TimeManager.Instance.);
        PlayerPrefs.SetFloat("Day", TimeManager.Instance.dayCount);

        /*for(int i = 0;i< EXP.count;i++)
        {
            string savekey = "EXP"+i ;
            Playerperf.setfloat( savekey, EXP[i]);

        }*/
        
        Debug.Log("Save HP: " + Player.Instance.HP);
        Debug.Log("Save MaxHP: " + Player.Instance.MaxHP);
        Debug.Log("Save MinHP: " + Player.Instance.MinHP);
        Debug.Log("Save Stamina: " + Player.Instance.Stamina);
        Debug.Log("Save MaxStamina: " + Player.Instance.MaxStamina);
        Debug.Log("Save MinStamina: " + Player.Instance.MinStamina);
        Debug.Log("Save Satiety: " + Player.Instance.Satiety);
        
        //Debug.Log("Save Time: " + TimeManager.Instance.);
        Debug.Log("Save Day: " + TimeManager.Instance.dayCount);
        
    }

    public void LoadPlayer()
    {
        Player.Instance.SetHP(Player.Instance.HP); PlayerPrefs.GetFloat("HP",playerdefault.HealthStat.HP);
        Player.Instance.SetMaxHP(Player.Instance.MaxHP); PlayerPrefs.GetFloat("MaxHP",playerdefault.HealthStat.MaxHP);
        Player.Instance.SetMinHP(Player.Instance.MinHP); PlayerPrefs.GetFloat("MinHP",playerdefault.HealthStat.MinHP);
        Player.Instance.SetStamina(Player.Instance.Stamina); PlayerPrefs.GetFloat("Stamina",playerdefault.StaminaStat.Stamina);
        Player.Instance.SetMaxStamina(Player.Instance.MaxStamina); PlayerPrefs.GetFloat("MaxStamina",playerdefault.StaminaStat.MaxStamina);
        Player.Instance.SetMinStamina(Player.Instance.MinStamina); PlayerPrefs.GetFloat("MinStamina",playerdefault.StaminaStat.MinStamina);
        Player.Instance.SetSatiety(Player.Instance.Satiety); PlayerPrefs.GetFloat("Satiety",playerdefault.SatietyStat.Satiety);

        //float time = PlayerPrefs.GetFloat("Time");
        float day = PlayerPrefs.GetFloat("Day");

        /*for(int i = 0;i< EXP.count;i++)
        {
            string savekey = "EXP"+i ;
            Playerperf.setfloat( savekey, EXP[i]);
        }*/
        Debug.Log("Load HP: ");
        Debug.Log("Load MaxHP: ");
        Debug.Log("Load MinHP: ");
        Debug.Log("Load Stamina: ");
        Debug.Log("Load MaxStamina: ");
        Debug.Log("Load minStamina: ");
        Debug.Log("Load Satiety: ");
        
        //Debug.Log("Load Time:" + time);
        Debug.Log("Load Day:");
        //Debug.Log(playerdefault.HealthStat.MaxHP);
        //Debug.Log(playerdefault.HealthStat.MinHP);
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete");
    }
}
