using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStateHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] plantObj = new GameObject[5];

    public void SwitchPlant(Plant.GrowthStage growthStage)
    {
        for (int i = 0; i < plantObj.Length; i++)
        {
            Debug.Log(plantObj[i]);
        }
        DeactivateAllPlant();
        
        switch (growthStage)
        {
            case Plant.GrowthStage.Small:
                plantObj[0].SetActive(true);
                Debug.Log(plantObj[0].gameObject.name);
                break;
            case Plant.GrowthStage.Medium:
                plantObj[1].SetActive(true);
                Debug.Log(plantObj[1].gameObject.name);
                break;
            case Plant.GrowthStage.Large:
                plantObj[2].SetActive(true);
                Debug.Log(plantObj[2].gameObject.name);
                break;
            case Plant.GrowthStage.Full:
                plantObj[3].SetActive(true);
                Debug.Log(plantObj[3].gameObject.name);
                break;
            case Plant.GrowthStage.Rot:
                plantObj[4].SetActive(true);
                Debug.Log(plantObj[4].gameObject.name);
                break;
            default:
                break;
        }
    }
    
    private void DeactivateAllPlant()
    {
        for (int i = 0; i < plantObj.Length; i++)
        {
            plantObj[i].SetActive(false);
        }
    }
}
