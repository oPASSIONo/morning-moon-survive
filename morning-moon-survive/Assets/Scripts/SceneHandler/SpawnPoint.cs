using UnityEngine;


public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnPointTag;

    private void Awake()
    {
        if (SpawnPointManager.Instance != null)
        {
            SpawnPointManager.Instance.RegisterSpawnPoint(spawnPointTag, gameObject);
        }
        
    }
}