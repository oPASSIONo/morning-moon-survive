using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
     [SerializeField] private List<GameObject> placedGameObjects = new();
     
    public int PlaceObject(GameObject prefab, Vector3 position , int rotationAngle)
    {
        GameObject newObject = Instantiate(prefab, position, Quaternion.Euler(0, rotationAngle, 0));
        //newObject.transform.position = position;
        placedGameObjects.Add(newObject);
        
        /*NavMeshSurface surface = NavmeshManager.Instance.GetNavMeshSurfaces("NavMesh_HomeScene");
        if (surface != null)
        {
            surface.BuildNavMesh();
            Debug.Log("NavMesh built successfully.");
        }
        else
        {
            Debug.LogError("NavMeshSurface 'NavMesh_HomeScene' not found.");
        }*/
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
