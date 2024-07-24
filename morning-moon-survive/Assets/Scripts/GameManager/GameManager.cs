using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Inventory;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject player;

    private Player playerComponent;
    private AgentTool playerAgentTool;
    private float playerATKBaseDMG;
    private float weaponATKBaseDMG;
    private float sharpnessOfWeapon;
    private float enemyWeaponWeaknessDMG;
    private float enemyDEF;
    private float weaponElementATKBaseDMG;
    private float enemyElementWeaknessDMG;
    private float bonusATK;
    
    
    [SerializeField] private GameObject craftingSystem;
    private UICraftingPage craftingUI;
    private CraftButtonHandler craftButtonHandler;

    [SerializeField] private GameObject gameCanvas;
    private UIInventoryPage uiInventoryPage;
    private Health playerHealth;
    private Stamina playerStamina;
    private Satiety playerSatiety;
    
    private UIHealthBar uiHealthBar;
    private UIStaminaBar uiStaminaBar;
    private UISatietyBar uiSatietyBar;

    
    
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
        
        

        uiHealthBar = gameCanvas.GetComponent<GameCanvasRef>().healthBar;
        uiStaminaBar = gameCanvas.GetComponent<GameCanvasRef>().staminaBar;
        uiSatietyBar = gameCanvas.GetComponent<GameCanvasRef>().satietyBar;
        
        uiHealthBar.healthComponent = playerHealth;
        uiStaminaBar.staminaComponent = playerStamina;
        uiSatietyBar.satietyComponent = playerSatiety;
        
    }
    private void InitializeCraftingSystem()
    {
        craftingSystem = Instantiate(craftingSystem);
        
        craftButtonHandler = gameCanvas.GetComponent<GameCanvasRef>().craftButtonHandler;
        craftingUI = gameCanvas.GetComponent<GameCanvasRef>().craftingPage;
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
        playerAgentTool = player.GetComponent<AgentTool>();
        playerComponent = player.GetComponent<Player>();
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
    
    
    public void PlayerDealDamage(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        AttackType playerATKType = playerAgentTool.currentTool.AttackType;
        Element playerElementType = playerAgentTool.currentTool.Element;
        playerATKBaseDMG = playerComponent.Attack;
        weaponATKBaseDMG = playerAgentTool.currentTool.AttackDamage;
        sharpnessOfWeapon = playerAgentTool.currentTool.Sharpness;
        enemyDEF = enemy.Defense;

        enemyWeaponWeaknessDMG = GetAttackTypeWeaknessMultiplier(playerATKType,enemy.GetAttackTypeWeaknessRank(playerATKType));
        weaponElementATKBaseDMG = playerAgentTool.currentTool.ElementAttackDamage;
        enemyElementWeaknessDMG =
            GetElementTypeWeaknessMultiplier(playerElementType, enemy.GetElementTypeWeaknessRank(playerElementType));
        bonusATK = 0f;
        
        Debug.Log($"ATK Type {enemyWeaponWeaknessDMG}");
        Debug.Log($"Element {enemyElementWeaknessDMG}");

        float damage = (playerATKBaseDMG + (weaponATKBaseDMG * sharpnessOfWeapon * enemyWeaponWeaknessDMG) - enemyDEF) +
                 (weaponElementATKBaseDMG * enemyElementWeaknessDMG) + (bonusATK);
        
        if (enemy!=null)
        {
            enemy.TakeDamage(damage);
        }

    }

    public float GetAttackTypeWeaknessMultiplier(AttackType attackType, int weaknessRank)
    {
        var rankToMultiplier = new Dictionary<int, float>
        {
            { 3, 1.6f },
            { 2, 1.4f },
            { 1, 1.2f },
            { 0, 1f },
            { -1, 0f }
        };
        return rankToMultiplier.TryGetValue(weaknessRank, out float multiplier) ? multiplier : 0f;
    }
    public float GetElementTypeWeaknessMultiplier(Element element, int weaknessRank)
    {
        var rankToMultiplier = new Dictionary<int, float>
        {
            { 3, 1.5f },
            { 2, 1.2f },
            { 1, 1f },
            { 0, 0.5f },
            { -1, 0f }
        };
        return rankToMultiplier.TryGetValue(weaknessRank, out float multiplier) ? multiplier : 0f;
    }
   
}

