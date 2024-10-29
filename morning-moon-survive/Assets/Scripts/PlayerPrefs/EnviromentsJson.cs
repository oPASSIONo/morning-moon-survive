using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class EnviromentsJson
{
    [JsonProperty] public float Time;
    [JsonProperty] public int Day;

    [JsonProperty] public string buildingTypeName;
    [JsonProperty] public Vector3 position;
    [JsonProperty] public Vector3 rotation;

    public void SetTime(TimeManager present)
    {
        Time = present.CurrentTimeOfDay;
        Day = present.DayCount;
    }

    public void SetEnvironmentData(GameObject gameObject, Vector3 position, Vector3 rotation)
    {
        this.buildingTypeName = gameObject.name;
        this.position = position;
        this.rotation = rotation;
    }
    
    /*public GameObject InstantiateEnvironment(GameObject prefab, Transform parent = null)
    {
        GameObject newObject = GameObject.Instantiate(prefab, position, Quaternion.Euler(rotation), parent);
        newObject.name = buildingTypeName;
        return newObject;
    }*/
    
    /*public void BuildEnvironment(Dictionary<GameObject, GameObject> prefabDictionary)
    {
        if (prefabDictionary.TryGetValue(buildingType, out GameObject prefab))
        {
            GameObject newObject = Object.Instantiate(prefab, position, rotation);
            
        }
        else
        {
            Debug.LogError($"Prefab สำหรับ '{buildingType}' ไม่พบใน Dictionary");
        }
    }*/
}