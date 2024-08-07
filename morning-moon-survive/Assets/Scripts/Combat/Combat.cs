using UnityEngine;
using System.Collections;
using System;

public class Combat : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;
    private PlayerAnimation playerAnimation;
    private bool hasHit = false;
    public bool isPerformingAction { get; private set; } = false;
    public void SetIsPerformingAction(bool isPerform) => isPerformingAction = isPerform;

    private Stamina staminaComponent;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        GameInput.Instance.OnAction += PerformAction;
        staminaComponent = GetComponent<Stamina>();
    }

    private void PerformAction(object sender, EventArgs e)
    {
        if (GetComponent<AgentTool>().currentTool != null && !isPerformingAction)
        {
            staminaComponent.TakeAction();

            // Reset the hasHit flag
            hasHit = false;
            
            playerAnimation.PlayerAttackAnim();
            
        }
    }

    public void SetActionPerformOnCombat(bool isEnable)
    {
        switch (isEnable)
        {
            case true:
                GameInput.Instance.OnAction += PerformAction;
                break;
            case false:
                GameInput.Instance.OnAction -= PerformAction;
                break;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (attackCollider.enabled && !hasHit)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Collider triggered by: " + enemy.gameObject.name);
                // Mark as hit to ignore subsequent collisions
                hasHit = true;
                // Call GameManager to handle damage calculation and application
                GameManager.Instance.PlayerDealDamage(enemy.gameObject, other);
            }
        }
    }

    public void SetAttackCollider(bool isEnable)
    {
        switch (isEnable)
        {
            case true:
                attackCollider.enabled = true;
                break;
            case false:
                attackCollider.enabled = false;
                break;
        }
    }
}