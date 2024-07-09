using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayModeGameController : MonoBehaviour
{
    public static SinglePlayModeGameController Instance { get; private set; }

    public GameObject playerPrefab;
    private GameObject playerInstance;

    private Transform spawnPoint; // Reference to the spawn point in the scene

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the spawn point in the scene
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;

        // Check if playerInstance already exists in the scene
        if (playerInstance == null)
        {
            // Check if there is already a player in the scene
            GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
            if (existingPlayer != null)
            {
                playerInstance = existingPlayer;
            }
            else
            {
                playerInstance = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            }

            DontDestroyOnLoad(playerInstance);
        }
        else
        {
            // Move the playerInstance to the spawn point position
            playerInstance.transform.position = spawnPoint.position;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}