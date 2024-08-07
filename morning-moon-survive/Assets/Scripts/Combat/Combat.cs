using UnityEngine;
using System.Collections;
using System;

public class Combat : MonoBehaviour
{
    [SerializeField] public Collider attackCollider;
     private PlayerAnimation playerAnimation;
     private float enableDuration = 0.5f;
    private bool hasHit = false;
    public bool isPerformingAction = false;

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
            StartCoroutine(ToggleCollider());
        }
    }
    
    private IEnumerator ToggleCollider()
    {
        GameInput.Instance.OnAction -= PerformAction;
        // Enable the collider
        //attackCollider.enabled = true;
        playerAnimation.PlayerAttackAnim();
        // Wait for the specified duration
       // yield return new WaitForSeconds(enableDuration);

        // Disable the collider
       // attackCollider.enabled = false;
        GameInput.Instance.OnAction += PerformAction;
        yield return null;
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
}