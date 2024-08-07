using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Inventory;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowCamera;
    
    [SerializeField] private GameObject player;
    private Health playerHealth;
    private Stamina playerStamina;
    private Satiety playerSatiety;
    private Player playerComponent;
    private AgentTool playerAgentTool;
    
    private float enemyWeaponWeaknessDMG;
    private float enemyElementWeaknessDMG;
    
    
    [SerializeField] private GameObject craftingSystem;
    [SerializeField] private GameObject gameCanvas;
    public LoadingScreenManager loadingScreenManager;

    
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
        PersistentObject();
    }
    private void InitializePlayer()
    {
        playerHealth = player.GetComponent<Health>();
        playerHealth.OnEntityDie += OnPlayerDie;
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

    public void MoveTargetToPoint(string moveTarget,GameObject movePoint)
    {
        GameObject objectToMove=null;
        switch (moveTarget)
        {
            case "Player":
                objectToMove = player;
                player.GetComponent<NavMeshAgent>().Warp(movePoint.transform.position);
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
        /*// Load the loading scene first
        SceneManager.LoadScene("LoadingScene");

        // Ensure the loading screen manager is initialized and ready
        if (loadingScreenManager != null)
        {
            loadingScreenManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("LoadingScreenManager not assigned!");
        }*/
    }

    private void PersistentObject()
    {
        DontDestroyOnLoad(craftingSystem);
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(playerFollowCamera);
        DontDestroyOnLoad(gameCanvas);
    }

    public void EnemyDealDamage(Enemy enemy,int movesetIndex)
    {
        float damage = 0f;
        float playerDEF = playerComponent.Defense;
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
                damage = ((movesetDMG*enemy.BaseATK) - playerDEF) + (movesetElementDMG -playerComponent.Resistant);
                break;
        }
        playerHealth.TakeDamage(damage);
    }

    private void OnPlayerDie()
    {
        gameCanvas.GetComponent<GameCanvasRef>().notiBox.SetActive(true);
        GameInput.Instance.SetPlayerInput(false);
    }

    public void RespawnPlayer()
    {
        GameInput.Instance.SetPlayerInput(true);
        playerComponent.SetHP(Player.Instance.GetPlayerStatSO().HealthStat.HP);
        playerComponent.SetSatiety(Player.Instance.GetPlayerStatSO().SatietyStat.Satiety);
        playerSatiety.InitialSatietyConsumeOvertime();
    }
    public void PlayerDealDamage(GameObject target, Collider hitCollider)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        AttackType playerATKType = playerAgentTool.currentTool.AttackType;
        Element playerElementType = playerAgentTool.currentTool.Element;
        float playerATKBaseDMG = playerComponent.Attack;
        float weaponATKBaseDMG = playerAgentTool.currentTool.AttackDamage;
        float sharpnessOfWeapon = playerAgentTool.currentTool.Sharpness;
        float enemyDEF = enemy.Defense;
        float weaponElementATKBaseDMG = playerAgentTool.currentTool.ElementAttackDamage;
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
            else if(hitCollider==enemy.boydyPoint)
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

