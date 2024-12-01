using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapNavMesh : MonoBehaviour
{
    [SerializeField] private string navMeshName;

    private void Start()
    {
        if (NavmeshManager.Instance != null)
        {
            NavmeshManager.Instance.RegisterNavMeshSurfaces(navMeshName , GetComponent<NavMeshSurface>());
        }
    }
}
