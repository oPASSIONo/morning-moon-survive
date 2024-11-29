using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Model;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowCamera;
    [SerializeField] private GameObject timeManager;
    [SerializeField] private GameObject gameInput;
    
    [SerializeField] private GameObject craftingSystem;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject buildingSystem;
    public PlayerManager PlayerManager { get; private set; }
    
    [SerializeField] private UIBuildingPage uiBuildingPage;
    [SerializeField] private PlacementSystem placementSystem;
    [SerializeField] private GameInput gameInputScript;
    [SerializeField] private UICraftingManager uiCraftingManager;
    [SerializeField] private UIBuildingManager uiBuildingManager;
    [SerializeField] private TimeManager timeManagerScript; 
    
    private float enemyWeaponWeaknessDMG;
    private float enemyElementWeaknessDMG;
    
    private bool isLoadScene = false;
    private bool isPlayerDie = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);    
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {            
        PlayerManager = new PlayerManager();
        StartGame();
    }

    private void StartGame()
    {
        PersistentObject();
    }
    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            TryAssignLocalPlayerComponents();
        
            // Apply DontDestroyOnLoad to the local player object (for the client)
            Player player = PlayerManager.Player;
            if (player != null)
            {
                Debug.Log("Applying DontDestroyOnLoad to the local player object.");
                DontDestroyOnLoad(player); // Apply to local player
            }
            else
            {
                Debug.LogError("Player object is null. Can't apply DontDestroyOnLoad.");
            }
        }

        // Ensure the host sees the client's player object in DontDestroyOnLoad after client connects
        if (NetworkManager.Singleton.IsHost)
        {
            // Check for all NetworkObjects and apply DontDestroyOnLoad for remote players
            foreach (var networkObject in FindObjectsOfType<NetworkObject>())
            {
                if (networkObject.IsOwner || networkObject.IsLocalPlayer) 
                    continue; // Skip if it's the host's own player object or local player

                // Apply DontDestroyOnLoad to remote players
                Debug.Log($"Host sees remote player. Applying DontDestroyOnLoad to {networkObject.gameObject.name}.");
                DontDestroyOnLoad(networkObject.gameObject);
            }
        }
           
        
    }
    
    public void TryAssignLocalPlayerComponents()
    {
        foreach (var networkObject in FindObjectsOfType<NetworkObject>())
        {
            if (networkObject.IsLocalPlayer)
            {
                PlayerManager.Initialize(networkObject);
                DontDestroyOnLoad(networkObject.gameObject);
                var inventory = PlayerManager.PlayerInventoryController;
                var stateManager = PlayerManager.PlayerStateManager;

                if (uiBuildingPage != null)
                {
                    uiBuildingPage.SetInventoryController(inventory);
                    uiBuildingPage.SetPlayerStateManager(stateManager);
                }
                if (placementSystem != null)
                {
                    placementSystem.SetInventoryController(inventory);
                }
                if (gameInputScript != null)
                {
                    gameInputScript.SetPlayerStateManager(stateManager);
                }
                if (uiCraftingManager != null)
                {
                    uiCraftingManager.SetPlayerStateManager(stateManager);
                } 
                if (uiBuildingManager != null)
                {
                    uiBuildingManager.SetPlayerStateManager(stateManager);
                }

                if (timeManagerScript != null)
                {
                    timeManagerScript.SetPlayerStateManager(stateManager);
                }
                
                break;
            }
         
        }

    }
    /*private void InitializePlayer()
    {
        playerHealth = player.GetComponent<Health>();
        playerHealth.OnEntityDie += OnPlayerDie;
        playerStamina = player.GetComponent<Stamina>();
        playerSatiety = player.GetComponent<Satiety>();
        playerAgentTool = player.GetComponent<AgentTool>();
        playerComponent = player.GetComponent<Player>();
    }*/

    private void InitializeCoreGameObj()
    {       
        //player.SetActive(true);
        
        gameInput.SetActive(true);
        playerFollowCamera.SetActive(true);
        gameCanvas.SetActive(true);
        timeManager.SetActive(true);
        buildingSystem.SetActive(true);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryAssignLocalPlayerComponents();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private Transform TargetSpawnPoint(string targetSpawnPoint)
    {
        return SpawnPointManager.Instance.GetSpawnPoint(targetSpawnPoint);
    }

    public void MoveTargetToPoint(string moveTarget, string spawnPointName)
    {
        if (PlayerManager.Player == null)
        {
            Debug.LogError("Player not assigned. Cannot move to point.");
            return;
        }
        StartCoroutine(WaitAndMove(moveTarget, spawnPointName));
    }

    private IEnumerator WaitAndMove(string moveTarget, string spawnPointName)
    {
        NavMeshAgent objectToMove = null;
        
        while (true)
        {
            Transform movePointTransform = SpawnPointManager.Instance.GetSpawnPoint(spawnPointName);

            if (movePointTransform != null)
            {
                if (moveTarget == "Player")
                {
                    objectToMove = PlayerManager.Player.GetComponent<NavMeshAgent>();
                    if (objectToMove != null)
                    {
                        objectToMove.Warp(movePointTransform.position);
                        Debug.Log($"Player moved to: {movePointTransform.position}");
                        yield break; // Exit the coroutine once the player is moved

                    }
                    else
                    {
                        Debug.LogError("NavMeshAgent not found on the player!");
                    }
                }
            }
            else
            {
                Debug.Log($"Waiting for spawn point '{spawnPointName}' to be registered...");
                yield return null; // Wait for the next frame and check again
            }
        }
    }


    public void MoveTargetToPoint(string moveTarget,GameObject movePointGameObject)
    {
        NavMeshAgent objectToMove=null;
        switch (moveTarget)
        {
            case "Player":
                objectToMove = PlayerManager.Player.GetComponent<NavMeshAgent>();
                if (movePointGameObject==null)
                {
                    Debug.Log("Move Point Null");
                }
                else
                {
                    objectToMove.Warp(movePointGameObject.transform.position);    
                }
                break;
            default:
                break;
        }
    }
    
    
    public void LoadScene(string sceneName)
    {
        isLoadScene = true;
        LevelManager.Instance.OnLoadComplete += OnLoadComplete;
        LevelManager.Instance.OnLoaderFadeOut += OnLoaderFadeOut;
        LevelManager.Instance.LoadScene(sceneName);
    }
    
    private void OnLoadComplete()
    {
        if (isLoadScene)
        {
            InitializeCoreGameObj();
            // Ensure the spawn points are cleared from the previous scene
            SpawnPointManager.Instance.ClearSpawnPoints();
        }
        TimeManager.Instance.SetStartTimer(false);
        GameInput.Instance.SetPlayerInput(false);
        if (isLoadScene)
        {
            if (isPlayerDie)
            {
                RespawnPlayer();
            }
        }
        
    }
    private void OnLoaderFadeOut()
    {
        TimeManager.Instance.SetStartTimer(true);
        GameInput.Instance.SetPlayerInput(true);

        if (isLoadScene)
        {
            
            MoveTargetToPoint("Player","PlayerSpawn");

            SaveManager.Instance.SavePlayer();
        }
        isLoadScene = false;
    }
    
   
    private void PersistentObject()
    {
        DontDestroyOnLoad(craftingSystem);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(playerFollowCamera);
        DontDestroyOnLoad(gameCanvas);
        DontDestroyOnLoad(buildingSystem);
    }

    public void EnemyDealDamage(Enemy enemy,int movesetIndex)
    {
        float damage = 0f;
        float playerDEF = PlayerManager.Player.Defense;
        float movesetDMG = enemy.MovesetStats[movesetIndex].PhysicalDamage;
        float movesetElementDMG = enemy.MovesetStats[movesetIndex].ElementDamage;
        
        Debug.Log($"{enemy.gameObject.name} move 1 DMG ATK : {movesetDMG}");
        Debug.Log($"{enemy.gameObject.name} move 1 DMG Element : {movesetElementDMG}");
        switch (movesetElementDMG)
        {
            case 0:
                damage = ((movesetDMG*enemy.BaseATK) - playerDEF);
                break;
            default:
                damage = ((movesetDMG*enemy.BaseATK) - playerDEF) + (movesetElementDMG - PlayerManager.Player.Resistant);
                break;
        }
        PlayerManager.PlayerHealth.TakeDamage(damage);
    }

    private void OnPlayerDie()
    {
        // Start the coroutine to handle the delay
        StartCoroutine(HandlePlayerDeath());
    }

    private IEnumerator HandlePlayerDeath()
    {
        // Perform actions before the delay
        GameInput.Instance.SetPlayerInput(false);
        PlayerManager.PlayerAnimation.PlayerDeadAnim();
        PlayerManager.Player.GetComponent<Collider>().enabled = false;
        isPlayerDie = true;
        
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Perform actions after the delay
        gameCanvas.GetComponent<GameCanvas>().notiBox.SetActive(true);
        Debug.Log($"is player die : {isPlayerDie}");
    }

    public void RespawnPlayer()
    {
        PlayerManager.Player.GetComponent<Collider>().enabled = true;
        gameCanvas.GetComponent<GameCanvas>().notiBox.SetActive(false);
        PlayerManager.PlayerAnimation.PlayerRespawnAnim();
        //GameInput.Instance.SetPlayerInput(true);
        
        PlayerStats playerStats = PlayerManager.Player.GetPlayerStatSO();
        if (playerStats != null)
        {
            PlayerManager.Player.SetHP(playerStats.HealthStat.HP);
            PlayerManager.Player.SetSatiety(playerStats.SatietyStat.Satiety);
            PlayerManager.PlayerHealth.Initialize(playerStats.HealthStat.MaxHP, playerStats.HealthStat.MinHP, playerStats.HealthStat.HP);
            PlayerManager.PlayerSatiety.Initialize(playerStats.SatietyStat.MaxSatiety, playerStats.SatietyStat.MinSatiety, 
                playerStats.SatietyStat.Satiety, playerStats.SatietyStat.SatietyBleeding,
                playerStats.SatietyStat.SatietyConsumePoint, playerStats.SatietyStat.SatietyConsumeRate);
        }

        /*playerComponent.SetHP(Player.Instance.GetPlayerStatSO().HealthStat.HP);
        playerComponent.SetSatiety(Player.Instance.GetPlayerStatSO().SatietyStat.Satiety);*/
        PlayerManager.PlayerSatiety.InitialSatietyConsumeOvertime();
        //SaveManager.Instance.SavePlayer();
        isPlayerDie = false;
        
        // Optional: Save Player State
        SaveManager.Instance?.SavePlayer();

        Debug.Log("Player respawned successfully.");
    }
    public void PlayerDealDamage(GameObject target, Collider hitCollider)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        AttackType playerATKType = PlayerManager.PlayerAgentTool.currentTool.AttackType;
        Element playerElementType = PlayerManager.PlayerAgentTool.currentTool.Element;
        float playerATKBaseDMG = PlayerManager.Player.Attack;
        float weaponATKBaseDMG = PlayerManager.PlayerAgentTool.currentTool.AttackDamage;
        float sharpnessOfWeapon = PlayerManager.PlayerAgentTool.currentTool.Sharpness;
        float enemyDEF = enemy.Defense;
        float weaponElementATKBaseDMG = PlayerManager.PlayerAgentTool.currentTool.ElementAttackDamage;
        float bonusATK = 0f;
        
        if (enemy!=null)
        {
            if (hitCollider == enemy.weakPoint)
            {
                enemyWeaponWeaknessDMG = GetAttackTypeWeaknessMultiplier(playerATKType,
                    enemy.GetWeakPointAttackTypeWeaknessRank(playerATKType));
                enemyElementWeaknessDMG = GetElementTypeWeaknessMultiplier(playerElementType,
                    enemy.GetWeakPointElementTypeWeaknessRank(playerElementType));
                Debug.Log("Hit WeakPoint");
            }
            else if(hitCollider == enemy.bodyPoint)
            {
                enemyWeaponWeaknessDMG = GetAttackTypeWeaknessMultiplier(playerATKType,enemy.GetBodyPointAttackTypeWeaknessRank(playerATKType));
                enemyElementWeaknessDMG =
                    GetElementTypeWeaknessMultiplier(playerElementType, enemy.GetBodyPointElementTypeWeaknessRank(playerElementType));
                Debug.Log("Hit BodyPoint");
            }
            float damage = (playerATKBaseDMG + (weaponATKBaseDMG * sharpnessOfWeapon * enemyWeaponWeaknessDMG) - enemyDEF) +
                           (weaponElementATKBaseDMG * enemyElementWeaknessDMG) + (bonusATK);
            enemy.healthComponent.TakeDamage(damage);
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

