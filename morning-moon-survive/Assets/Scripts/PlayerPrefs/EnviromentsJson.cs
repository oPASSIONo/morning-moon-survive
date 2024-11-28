using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class EnviromentsJson
{
    [JsonProperty] public float Time;
    [JsonProperty] public int Day;
    [JsonProperty] public List<BuildingData> buildings = new List<BuildingData>();
    
    public class BuildingData
    {
        [JsonProperty] public string buildingName;
        [JsonProperty] public float positionX;
        [JsonProperty] public float positionY;
        [JsonProperty] public float positionZ;
        [JsonProperty] public float rotationX;
        [JsonProperty] public float rotationY;
        [JsonProperty] public float rotationZ;
        
        public BuildingData(string name,float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            buildingName = name;
            positionX = posX;
            positionY = posY;
            positionZ = posZ;
            rotationX = rotX;
            rotationY = rotY;
            rotationZ = rotZ;
        } 
    }
    
    public void AddBuildingData(string name, Vector3 position, Quaternion rotation)
    {
        BuildingData newBuilding = new BuildingData(name, position.x, position.y, position.z, rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
        buildings.Add(newBuilding);
    }
    
    public void SetTime(TimeManager present)
    {
        Time = present.CurrentTimeOfDay;
        Day = present.DayCount;
    }
}