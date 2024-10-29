using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
     [SerializeField] public List<GameObject> placedGameObjects = new();

    public int PlaceObject(GameObject prefab, Vector3 position , int rotationAngle)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.Euler(0, rotationAngle, 0));
        //newObject.transform.position = position;
        placedGameObjects.Add(newObject);
        
        EnviromentsJson environmentData = new EnviromentsJson();
        environmentData.SetEnvironmentData(newObject, newObject.transform.position, newObject.transform.rotation.eulerAngles);

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
}
