using UnityEngine;
using System;
using Unity.Netcode;

public class Health : NetworkBehaviour
{
 
    public float MaxHealth { get; private set; }
    public float MinHealth { get; private set; }
    public float CurrentHealth { get; private set; }


    public event Action<float, float,float> OnHealthChanged;
    public event Action OnEntityDie;
        
    private PlayerAnimation playerAnimation;

    
    private void Start()
    {
        // Only do this for the local player
        if (IsLocalPlayer)
        {
            // Get the PlayerAnimation component on this GameObject
            playerAnimation = GetComponent<PlayerAnimation>();
        }
      
    }
    
    public void Initialize(float maxHealth, float minHealth, float initialHealth)
    {
        MaxHealth = maxHealth;
        MinHealth = minHealth;
        CurrentHealth = initialHealth;
      
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);
    }
    
    public void TakePlayerDamage(float damageAmount)
    {
        CurrentHealth += damageAmount;
                    
        if (CurrentHealth <= MinHealth) 
        {
            SetCurrentHealth(MinHealth);
        }
       
        ShowDamagePopupClientRpc(transform.position, damageAmount);
        
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);
            
        IsDie();
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
                    
        if (CurrentHealth <= MinHealth) 
        {
            SetCurrentHealth(MinHealth);
        }
     
        ShowDamagePopupClientRpc(transform.position, damageAmount);
        
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);
            
        IsDie();
    }

    /*[ServerRpc(RequireOwnership = false)]
    private void ShowDamagePopupSerVerRPC(Vector3 position, float damageAmount)
    {
        NetworkObject networkObj = GetComponent<NetworkObject>();
        if (networkObj != null && networkObj.IsSpawned)
        {
            ShowDamagePopupClientRpc(position, damageAmount);
        }
        else
        {
            Debug.LogWarning("Attempted to call ShowDamagePopup on an unspawned NetworkObject.");
        }
    }
    */

    
    [ClientRpc]
    private void ShowDamagePopupClientRpc(Vector3 position, float damageAmount)
    {
        // Ensure the DamagePopup is available
        if (DamagePopup.current != null)
        {
            DamagePopup.current.CreatePopup(position, damageAmount.ToString());
        }
    }
    
  
    public void AddHealth(float amount)
    {
        if (!IsServer) return;
        
        SetCurrentHealth(CurrentHealth + amount);
        if (CurrentHealth>=MaxHealth)
        {
            SetCurrentHealth(MaxHealth);
        }
        // Trigger health changed event
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth,MinHealth);
        Debug.Log($"{name} add health");
    }

    private void IsDie()
    {
        if (CurrentHealth <= MinHealth)
        {
            Die();
        }
        /*if (CurrentHealth <= 0)
        {
            Die();
        }*/
        else
        {
            if (GetComponent<Player>() != null)
            {
                playerAnimation.PlayerHitAnim();
            }
        }
        /*switch (CurrentHealth)
        {
            case 0:
                Die();
                break;
        }*/
    }
    
    public void Die()
    {
        /*CurrentHealth = MinHealth;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);*/
        OnEntityDie?.Invoke();
        Debug.Log($"{name} has died.Current Health : {CurrentHealth}");
    }
    
  
    public void SetCurrentHealth(float hp)
    {
        CurrentHealth = hp;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
    }

    public void SetMaxHealth(float val)
    {
        MaxHealth = val;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
    }

    public void SetMinHealth(float val)
    {
        MinHealth = val;
        OnHealthChanged?.Invoke(CurrentHealth,MaxHealth,MinHealth);
    }
    
}