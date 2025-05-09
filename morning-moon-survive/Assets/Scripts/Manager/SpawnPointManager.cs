using System;
using UnityEngine;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    private Dictionary<string, Transform> spawnPoints = new Dictionary<string, Transform>();
    public event Action OnSpawnPointsRegistered;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSpawnPoint(string spawnPointName, Transform spawnPointTransform)
    {
        if (!spawnPoints.ContainsKey(spawnPointName))
        {
            spawnPoints.Add(spawnPointName, spawnPointTransform);
            Debug.Log($"Spawn Point {spawnPointName} registered.");
        }

        // If all spawn points are registered, notify listeners
        OnSpawnPointsRegistered?.Invoke();
    }

    public Transform GetSpawnPoint(string spawnPointName)
    {
        if (spawnPoints.TryGetValue(spawnPointName, out Transform spawnPointTransform))
        {
            return spawnPointTransform;
        }
        return null;
    }

    public void ClearSpawnPoints()
    {
        spawnPoints.Clear(); // Clear spawn points when switching scenes
    }
}

