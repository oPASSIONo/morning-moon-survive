using UnityEngine;
using System.Collections;
using System;

public class Combat : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;
    private float enableDuration = 0.5f;
    private float weaponDamage = 10f;
    private bool hasHit = false;

    private Stamina staminaComponent;
    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.OnAction += PerformAction;
        staminaComponent = GetComponent<Stamina>();
    }

    private void PerformAction(object sender, EventArgs e)
    {
        if (GetComponent<AgentTool>().currentTool != null)
        {
            Debug.Log("Action Performed");
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
        attackCollider.enabled = true;
        Debug.Log("ATK Collider enabled");

        // Wait for the specified duration
        yield return new WaitForSeconds(enableDuration);

        // Disable the collider
        attackCollider.enabled = false;
        Debug.Log("ATK Collider disabled");
        GameInput.Instance.OnAction += PerformAction;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (attackCollider.enabled && !hasHit)
        {
            Debug.Log("Collider triggered by: " + enemy.gameObject.name);
            // Mark as hit to ignore subsequent collisions
            hasHit = true;
            switch (enemy.enemyStatsSO.IsMonster)
            {
                case true:
                    // Call the GameManager's DealDamage method
                    //GameManager.Instance.PlayerDealDamage(enemy.gameObject);
                    break;
                case false:
                    GameManager.Instance.PlayerDealDamage(enemy.gameObject);
                    break;
            }
        }
    }
}