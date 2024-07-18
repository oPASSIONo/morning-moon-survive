using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private CraftingSystem craftingSystem;
    
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        InitializePlayer();
        InitializeCamera();
        InitializeCraftingSystem();
        PersistentObject();
    }

    private void InitializeCraftingSystem()
    {
        //craftingSystem = Instantiate(craftingSystem);
    }

    private void InitializeCamera()
    {
        mainCamera = Instantiate(mainCamera);
        playerFollowCamera = Instantiate(playerFollowCamera);
        playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.GetComponent<Player>().RootTransform;
    }

    private void InitializePlayer()
    {
        player = Instantiate(player);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandlePlayerOnTransitionScene();
    }

    private GameObject TargetSpawnPoint(string targetSpawnPoint)
    {
        GameObject spawnPointObj;
        switch (targetSpawnPoint)
        {
            case "SpawnPlayer":
                spawnPointObj = GameObject.FindGameObjectWithTag("PlayerSpawnPoint");
                return spawnPointObj;
            case "SpawnEnemy":
                spawnPointObj = GameObject.FindGameObjectWithTag("EnemySpawnPoint");
                return spawnPointObj;
            case "SpawnResource":
                spawnPointObj = GameObject.FindGameObjectWithTag("ResourceSpawnPoint");
                return spawnPointObj;
            default:
                return null;
        }
    }

    private void MoveTargetToPoint(string moveTarget,GameObject movePoint)
    {
        GameObject objectToMove=null;
        switch (moveTarget)
        {
            case "Player":
                objectToMove = player;
                break;
            default:
                break;
        }
        objectToMove.transform.position = movePoint.transform.position;
    }

    private void HandlePlayerOnTransitionScene()
    {
        MoveTargetToPoint("Player",TargetSpawnPoint("SpawnPlayer"));
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void PersistentObject()
    {
        DontDestroyOnLoad(craftingSystem.gameObject);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(playerFollowCamera);
        DontDestroyOnLoad(player);
    }
}

