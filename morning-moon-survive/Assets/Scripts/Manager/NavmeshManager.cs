using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class NavmeshManager : MonoBehaviour
{
    public static NavmeshManager Instance;

    private Dictionary<string, NavMeshSurface> navMeshSurfaces = new Dictionary<string, NavMeshSurface>();

    public event Action OnNavMeshRegisterd;

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

    public void RegisterNavMeshSurfaces(string navMeshSurfaceName, NavMeshSurface navMeshSurface)
    {
        if (!this.navMeshSurfaces.ContainsKey(navMeshSurfaceName))
        {
            this.navMeshSurfaces.Add(navMeshSurfaceName, navMeshSurface);
            Debug.Log($"NavMesh {navMeshSurfaceName} registered.");
        }

        // If all spawn points are registered, notify listeners
        OnNavMeshRegisterd?.Invoke();
    }

    public NavMeshSurface GetNavMeshSurfaces(string navMeshSurfaceName)
    {
        if (navMeshSurfaces.TryGetValue(navMeshSurfaceName, out NavMeshSurface navMeshSurface))
        {
            return navMeshSurface;
        }
        return null;
    }

    public void ClearNavMeshSurfaces()
    {
        navMeshSurfaces.Clear(); // Clear spawn points when switching scenes
    }
}
