using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject playerFollowCamera;

    [SerializeField] private GameObject timeManager;

    [SerializeField] private GameObject gameInput;
    
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

    private bool isLoadScene = false;
    private bool isPlayerDie = false;
    
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

    private void InitializeCoreGameObj()
    {
        gameInput.SetActive(true);
        player.SetActive(true);
        playerFollowCamera.SetActive(true);
        gameCanvas.SetActive(true);
        timeManager.SetActive(true);
    }
    

    private GameObject TargetSpawnPoint(string targetSpawnPoint)
    {
        return SpawnPointManager.Instance.GetSpawnPoint(targetSpawnPoint);
    }


    public void MoveTargetToPoint(string moveTarget,GameObject movePoint)
    {
        NavMeshAgent objectToMove=null;
        switch (moveTarget)
        {
            case "Player":
                objectToMove = player.GetComponent<NavMeshAgent>();
                if (movePoint==null)
                {
                    Debug.Log("Move Point Null");
                }
                else
                {
                    Debug.Log(movePoint.name);
                    objectToMove.Warp(movePoint.transform.position);    
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
        }
        TimeManager.Instance.SetStartTimer(false);
        GameInput.Instance.SetPlayerInput(false);
        if (isLoadScene)
        {
            if (isPlayerDie)
            {
                RespawnPlayer();
            }
            //MoveTargetToPoint("Player", TargetSpawnPoint("PlayerSpawn"));
            player.GetComponent<NavMeshAgent>().Warp(new Vector3(0,0,0));
        }
    }
    private void OnLoaderFadeOut()
    {
        TimeManager.Instance.SetStartTimer(true);
        GameInput.Instance.SetPlayerInput(true);
        if (isLoadScene)
        {
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
        PlayerAnimation.Instance.PlayerDeadAnim();
        isPlayerDie = true;
        
    }

    public void RespawnPlayer()
    {
        PlayerAnimation.Instance.PlayerRespawnAnim();
        //GameInput.Instance.SetPlayerInput(true);
        playerComponent.SetHP(Player.Instance.GetPlayerStatSO().HealthStat.HP);
        playerComponent.SetSatiety(Player.Instance.GetPlayerStatSO().SatietyStat.Satiety);
        playerSatiety.InitialSatietyConsumeOvertime();
        //SaveManager.Instance.SavePlayer();
        isPlayerDie = false;
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

