using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Inventory;
using Inventory.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject craftingSystem;
    private UICraftingPage craftingUI;
    
    [SerializeField] private GameObject gameCanvas;
    private UIInventoryPage uiInventoryPage;
    private Health playerHealth;
    private Stamina playerStamina;
    private Satiety playerSatiety;
    
    private UIHealthBar uiHealthBar;
    private UIStaminaBar uiStaminaBar;
    private UISatietyBar uiSatietyBar;

    private CraftButtonHandler craftButtonHandler;
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
        InitializeGameCanvas();
        InitializeCraftingSystem();
        PersistentObject();
    }

    private void InitializeGameCanvas()
    {
        gameCanvas = Instantiate(gameCanvas);
        uiInventoryPage = gameCanvas.GetComponentInChildren<UIInventoryPage>(true);
        
        player.GetComponent<InventoryController>().inventoryUI = uiInventoryPage;
        
        List<UIHealthBar> uiHealthBarList = FindSpecificComponentsInChildrenBFS<UIHealthBar>(gameCanvas.transform, component => component.name.Contains("HealthBar"));
        if (uiHealthBarList.Count > 0)
        {
            uiHealthBar = uiHealthBarList[0]; // Directly assigning the component
        }
        List<UIStaminaBar> uiStaminaBarList = FindSpecificComponentsInChildrenBFS<UIStaminaBar>(gameCanvas.transform, component => component.name.Contains("StaminaBar"));
        if (uiStaminaBarList.Count > 0)
        {
            uiStaminaBar = uiStaminaBarList[0]; // Directly assigning the component
        }
        List<UISatietyBar> uiSatietyBarList = FindSpecificComponentsInChildrenBFS<UISatietyBar>(gameCanvas.transform, component => component.name.Contains("SatietyBar"));
        if (uiSatietyBarList.Count > 0)
        {
            uiSatietyBar = uiSatietyBarList[0]; // Directly assigning the component
        }
        
        
        
        uiHealthBar.healthComponent = playerHealth;
        uiStaminaBar.staminaComponent = playerStamina;
        uiSatietyBar.satietyComponent = playerSatiety;
        
    }
    private void InitializeCraftingSystem()
    {
        craftingSystem = Instantiate(craftingSystem);
        List<CraftButtonHandler> craftButtonList = FindSpecificComponentsInChildrenBFS<CraftButtonHandler>(gameCanvas.transform, component => component.name.Contains("CraftBtn"));
        if (craftButtonList.Count > 0)
        {
            craftButtonHandler = craftButtonList[0]; // Directly assigning the component
        }
        List<UICraftingPage> craftingUIList = FindSpecificComponentsInChildrenBFS<UICraftingPage>(gameCanvas.transform, component => component.name.Contains("PlayerCrafting"));
        if (craftingUIList.Count > 0)
        {
            craftingUI = craftingUIList[0]; // Directly assigning the component
        }
        craftButtonHandler.craftingSystem = craftingSystem.GetComponent<CraftingSystem>();
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
        playerHealth = player.GetComponent<Health>();
        playerStamina = player.GetComponent<Stamina>();
        playerSatiety = player.GetComponent<Satiety>();
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
        DontDestroyOnLoad(craftingSystem);
        
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(playerFollowCamera);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameCanvas);
    }
    
    public static List<T> FindSpecificComponentsInChildrenBFS<T>(Transform parent, System.Func<T, bool> criteria) where T : Component
    {
        Queue<Transform> queue = new Queue<Transform>();
        List<T> components = new List<T>();
        queue.Enqueue(parent);
        
        while (queue.Count > 0)
        {
            Transform current = queue.Dequeue();
            T component = current.GetComponent<T>();
            if (component != null && criteria(component))
            {
                components.Add(component);
            }
            foreach (Transform child in current)
            {
                queue.Enqueue(child);
            }
        }
        return components;
    }
}

