using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnPointName;

    private void Start()
    {
        if (SpawnPointManager.Instance != null)
        {
            SpawnPointManager.Instance.RegisterSpawnPoint(spawnPointName, transform);
        }
    }
}
