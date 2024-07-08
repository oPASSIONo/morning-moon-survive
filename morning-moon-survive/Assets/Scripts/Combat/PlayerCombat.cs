using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform weaponTransform; // Reference to the weapon's transform
    [SerializeField] private LayerMask enemyLayer; // Layer mask for enemy detection
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float headshotMultiplier = 2f;

    private bool isAttacking = false; // Flag to track if player is currently attacking

    private void OnEnable()
    {
        var playerInput = new PlayerInput();
        playerInput.PlayerControls.Enable();

        playerInput.PlayerControls.Attack.performed += OnAttackPerformed;
    }

    private void OnDisable()
    {
        var playerInput = new PlayerInput();
        playerInput.PlayerControls.Disable();

        playerInput.PlayerControls.Attack.performed -= OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        if (!isAttacking)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        isAttacking = true;
        Debug.Log("Attack");
        // Perform collider-based hit detection
        Collider[] hitColliders = Physics.OverlapBox(weaponTransform.position, weaponTransform.localScale / 2, weaponTransform.rotation, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Head"))
            {
                float damage = baseDamage * headshotMultiplier;
                DealDamage(hitCollider.gameObject, damage);
                Debug.Log("Attack Head");

            }
            else if (hitCollider.CompareTag("Body"))
            {
                float damage = baseDamage;
                DealDamage(hitCollider.gameObject, damage);
                Debug.Log("Attack Body");

            }
        }

        isAttacking = false;
    }

    private void DealDamage(GameObject enemy, float damage)
    {
        // Example: Apply damage to enemy health or call enemy script to take damage
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }
}
