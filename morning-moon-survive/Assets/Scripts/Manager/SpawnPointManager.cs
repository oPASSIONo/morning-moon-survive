using System.Collections.Generic;
using UnityEngine;


public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance { get; private set; }

    private Dictionary<string, GameObject> spawnPoints = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // Singleton Pattern Implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this manager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate manager
        }
    }

    public void RegisterSpawnPoint(string tag, GameObject spawnPoint)
    {
        if (!spawnPoints.ContainsKey(tag))
        {
            spawnPoints.Add(tag, spawnPoint);
            Debug.Log(tag);
            Debug.Log(spawnPoint.name);
        }
    }

    public GameObject GetSpawnPoint(string targetSpawnPoint)
    {
        spawnPoints.TryGetValue(targetSpawnPoint, out GameObject spawnPoint);
        return spawnPoint;
    }

    public void ClearSpawnPoints()
    {
        spawnPoints.Clear();
    }
}