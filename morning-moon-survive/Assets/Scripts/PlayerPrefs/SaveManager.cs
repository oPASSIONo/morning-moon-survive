using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public PlayerStats playerdefault;
    
    public void SavePlayer(Player player) 
    {
        PlayerPrefs.SetFloat("HP",player.HP);
        PlayerPrefs.SetFloat("MaxHp", player.MaxHP);
        PlayerPrefs.SetFloat("MinHp", player.MinHP);
        PlayerPrefs.SetFloat("Stamina", player.Stamina);
        PlayerPrefs.SetFloat("MaxStamina", player.MaxStamina);
        PlayerPrefs.SetFloat("MinStamina", player.MinStamina);
        PlayerPrefs.SetFloat("BaseActionCost", player.BaseActionCost);
        PlayerPrefs.SetFloat("Satiety", player.Satiety);
        PlayerPrefs.SetFloat("SatietyBleeding", player.SatietyBleeding);
        PlayerPrefs.SetFloat("SatietyConsumePoint", player.SatietyConsumePoint);
        PlayerPrefs.SetFloat("SatietyConsumeRate", player.SatietyConsumeRate);
        PlayerPrefs.SetFloat("MaxSatiety", player.MaxSatiety);
        PlayerPrefs.SetFloat("MinSatiety", player.MinSatiety);
        /*PlayerPrefs.SetFloat("Defense", player.Defense);
        PlayerPrefs.SetFloat("Resistant", player.Resistant);
        PlayerPrefs.SetFloat("Attack", player.Attack);
        PlayerPrefs.SetFloat("Element", player.Element);
        PlayerPrefs.SetFloat("EXP", player.EXP);*/

        /*for(int i = 0;i< EXP.count;i++)
        {
            string savekey = "EXP"+i ;
            Playerperf.setfloat( savekey, EXP[i]);

        }*/
        Debug.Log("HP: " + player.HP);
        Debug.Log(player.gameObject.name);
        //Debug.Log(playerdefault.HealthStat.MaxHP);
        //Debug.Log(playerdefault.HealthStat.MinHP);
    }

    public void LoadPlayer()
    {
        PlayerPrefs.GetFloat("HP",playerdefault.HealthStat.HP);
        PlayerPrefs.GetFloat("MaxHP",playerdefault.HealthStat.MaxHP);
        PlayerPrefs.GetFloat("MinHP",playerdefault.HealthStat.MinHP);

        /*for(int i = 0;i< EXP.count;i++)
        {
            string savekey = "EXP"+i ;
            Playerperf.setfloat( savekey, EXP[i]);
        }*/
        Debug.Log(playerdefault);
        Debug.Log(playerdefault);
        Debug.Log(playerdefault);
    }

    public void Delete()
    {
        PlayerPrefs.DeleteKey("HP");
        PlayerPrefs.DeleteKey("MaxHP");
        PlayerPrefs.DeleteKey("MinHP");
        Debug.Log("Delete");
    }
}
