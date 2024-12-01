using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Serialization;

public class Enemy : NetworkBehaviour
{
    public EnemyStatsSO enemyStatsSO;
    
    public List<GameObject> PoolDrop { get; private set; }
    
    public GameObject[] dropItems; // Array of items to drop upon death
    
    public Health healthComponent;
    
    public bool IsMonster { get; private set; }
    
    public string Name { get; private set; }
    public float HP { get; private set; }
    public float MaxHP { get; private set; }
    public float MinHP { get; private set; }
    public float Defense { get; private set; }
    public float BaseATK { get; private set; }
    public Element ElementATK { get; private set; }
    public float ElementATKDMG { get; private set; }

    public List<EnemyStatsSO.MovesetStat> MovesetStats { get; private set; }
    
    private EnemyStatsSO.AttackTypeWeaknesses _bodyPointAttackWeaknesses;
    private EnemyStatsSO.AttackTypeWeaknesses _weakPointAttackWeaknesses;
    private EnemyStatsSO.ElementTypeWeaknesses _bodyPointElementWeaknesses;
    private EnemyStatsSO.ElementTypeWeaknesses _weakPointElementWeaknesses;
    
    public Collider bodyPoint;
    public Collider weakPoint;
    
    private NetworkObject networkObject;

    private void Awake()
    {
        Initialize();
        networkObject = GetComponentInChildren<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError($"No NetworkObject found in {name} or its children. Ensure a NetworkObject is attached.");
        }
    }
    
    private void Start()
    {
        if (NetworkManager.Singleton.IsServer && networkObject != null && !networkObject.IsSpawned)
        {
            networkObject.Spawn();
        }
    }
    
    /*
    private void Start()
    {
        if (healthComponent != null)
        {
            healthComponent.OnHealthChanged += UpdateEnemyHealth;
        }
    }
    */
    
    #region Initialize

     private void Initialize()
        {
            InitializeStat();
            InitializeHealthComponent();
        }
    
     private void InitializeStat()
     {
         HP = enemyStatsSO.HP;
         MinHP = enemyStatsSO.MinHP;
         MaxHP = enemyStatsSO.MaxHP;
         Defense = enemyStatsSO.Defense;
         BaseATK = enemyStatsSO.BaseATK;
         Name = enemyStatsSO.Name;
         IsMonster = enemyStatsSO.IsMonster;
         ElementATK = enemyStatsSO.ElementATK;
         ElementATKDMG = enemyStatsSO.ElementATKDMG;
    
         MovesetStats = enemyStatsSO.movesetDMG;
    
         _bodyPointAttackWeaknesses = enemyStatsSO.BodyPointWeaknesses;
         _weakPointAttackWeaknesses = enemyStatsSO.WeakPointWeaknesses;
         _bodyPointElementWeaknesses = enemyStatsSO.BodyPointElementWeaknesses;
         _weakPointElementWeaknesses = enemyStatsSO.WeakPointElementWeaknesses;
            
        }

    #endregion
   
    private void InitializeHealthComponent()
    {
        healthComponent = GetComponent<Health>(); // Ensure GetComponent is finding the correct component
        if (healthComponent != null)
        {
            healthComponent.Initialize(MaxHP, MinHP, HP);
            healthComponent.OnHealthChanged += UpdateEnemyHealth; // Subscribe to health changed event
        }
        else
        {
            Debug.LogError("Health component not found on Enemy GameObject.");
        }
    }
    
    private void UpdateEnemyHealth(float currentHealth, float maxHealth,float minHealth)
    {
        HP = currentHealth;
        Debug.Log($"{name} health updated to {currentHealth}");
        IsDead();
    }

    private void IsDead()
    {
         if (HP <= 0)
         {
             HandleDeathServerRPC();
         }
    }

    [ServerRpc(RequireOwnership = false)]
    private void HandleDeathServerRPC()
    {
        Debug.Log($"Server handling death for {name} with health {HP}");
        DropRandomItemServerRpc();
        NetworkObject.Despawn();
 
    }
    
    private void DropRandomItem()
    {
        if (dropItems != null && dropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, dropItems.Length);
            GameObject itemToDrop = dropItems[randomIndex];
            
            if (itemToDrop != null)
            {
                Instantiate(itemToDrop, transform.position, Quaternion.identity);
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void DropRandomItemServerRpc()
    {
        if (dropItems != null && dropItems.Length > 0)
        {
            int randomIndex = Random.Range(0, dropItems.Length);
            GameObject itemToDrop = dropItems[randomIndex].gameObject;

            if (itemToDrop != null)
            {

                GameObject droppedItem = Instantiate(itemToDrop, transform.position, Quaternion.identity);
                NetworkObject droppedItemNetworkObj = GetTopmostParentNetworkObject(droppedItem);

                if (droppedItemNetworkObj  != null)
                {
                    Debug.Log("The item to drop : " + droppedItemNetworkObj);
                    droppedItemNetworkObj.Spawn(); 
                }
                else
                {
                    Debug.LogError("The item to drop does not have a NetworkObject component.");
                }
            }
        }
    }
    
    private NetworkObject GetTopmostParentNetworkObject(GameObject obj)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            NetworkObject networkObject = current.GetComponent<NetworkObject>();
            if (networkObject != null)
            {
                return networkObject;
            }
            current = current.parent;
        }
        return null;
    }

    #region Weak Point

     public int GetWeakPointAttackTypeWeaknessRank(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.Chop: return _weakPointAttackWeaknesses.Chop;
                case AttackType.Blunt: return _weakPointAttackWeaknesses.Blunt;
                case AttackType.Pierce: return _weakPointAttackWeaknesses.Pierce;
                case AttackType.Slash: return _weakPointAttackWeaknesses.Slash;
                case AttackType.Ammo: return _weakPointAttackWeaknesses.Ammo;
                default: return 0;
            }
        }
    
        public int GetWeakPointElementTypeWeaknessRank(Element element)
        {
            switch (element)
            {
                case Element.Thunder: return _weakPointElementWeaknesses.Thunder;
                case Element.Fire: return _weakPointElementWeaknesses.Fire;
                case Element.Ice: return _weakPointElementWeaknesses.Ice;
                case Element.Toxic: return _weakPointElementWeaknesses.Toxic;
                case Element.Dark: return _weakPointElementWeaknesses.Dark;
                case Element.Unholy: return _weakPointElementWeaknesses.Unholy;
                case Element.None: return 1;
                default: return 0;
            }
        }
    
        public int GetBodyPointAttackTypeWeaknessRank(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.Chop: return _bodyPointAttackWeaknesses.Chop;
                case AttackType.Blunt: return _bodyPointAttackWeaknesses.Blunt;
                case AttackType.Pierce: return _bodyPointAttackWeaknesses.Pierce;
                case AttackType.Slash: return _bodyPointAttackWeaknesses.Slash;
                case AttackType.Ammo: return _bodyPointAttackWeaknesses.Ammo;
                default: return 0;
            }
        }
    
        public int GetBodyPointElementTypeWeaknessRank(Element element)
        {
            switch (element)
            {
                case Element.Thunder: return _bodyPointElementWeaknesses.Thunder;
                case Element.Fire: return _bodyPointElementWeaknesses.Fire;
                case Element.Ice: return _bodyPointElementWeaknesses.Ice;
                case Element.Toxic: return _bodyPointElementWeaknesses.Toxic;
                case Element.Dark: return _bodyPointElementWeaknesses.Dark;
                case Element.Unholy: return _bodyPointElementWeaknesses.Unholy;
                case Element.None: return 1;
                default: return 0;
            }
        }

    #endregion
   
}