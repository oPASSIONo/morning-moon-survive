using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayModeGameController : MonoBehaviour
{
    public static SinglePlayModeGameController Instance { get; private set; }

    //public GameObject playerPrefab;
    [SerializeField] private GameObject player;

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
            return;
        }

        player = player;
        //player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the spawn point in the scene
        GameObject spawnPointObj = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPointObj != null)
        {
            Transform spawnPoint = spawnPointObj.transform;

        }
        player.transform.position = spawnPointObj.transform.position;
                        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
