using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
     [SerializeField] public List<GameObject> placedGameObjects = new();
     [SerializeField] public EnviromentsJson enviromentsJson;

    public int PlaceObject(GameObject prefab, Vector3 position , int rotationAngle, string customName = null)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.Euler(0, rotationAngle, 0));
        //newObject.transform.position = position;
        
        if (!string.IsNullOrEmpty(customName))
        {
            newObject.name = customName;
        }
        
        placedGameObjects.Add(newObject);
        
        if (enviromentsJson != null)
        {
            enviromentsJson.AddBuildingData(newObject.name, position, newObject.transform.rotation);
        }

        return placedGameObjects.Count - 1;
    }

    public void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex || placedGameObjects[gameObjectIndex] == null)
        {
            return;
        }

        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
    }
    
    
    public void ClearAllObjects()
    {
        for (int i = 0; i < placedGameObjects.Count; i++)
        {
            if (placedGameObjects[i] != null)
            {
                Destroy(placedGameObjects[i]);
                placedGameObjects[i] = null;
            }
        }
        placedGameObjects.Clear();
    }
}
