using System;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public EnemyPartHitDetector[] attackParts; // Array of attack part hit detectors
    private int currentMovesetIndex; // Index of the current moveset
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        //DisableAllAttackParts();
        InitializeAttackParts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetMoveset(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetMoveset(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetMoveset(3);
        }
    }

    // Set the moveset and activate the corresponding collider
    public void SetMoveset(int movesetIndex)
    {
        // Calculate the index of the collider to activate
        int colliderIndex = movesetIndex - 1; 

        // Validate the calculated index
        if (colliderIndex < 0 || colliderIndex >= attackParts.Length)
        {
            Debug.LogWarning("Invalid moveset index.");
            return;
        }

        // Enable the appropriate attack part and disable others
        //SetActiveAttackParts(colliderIndex);
    }

    /*private void SetActiveAttackParts(int activeIndex)
    {
        for (int i = 0; i < attackParts.Length; i++)
        {
            // Enable the collider at the active index and disable all others
            bool isActive = (i == activeIndex);
            Collider collider = attackParts[i].GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = isActive;
            }
        }
    }

    private void DisableAllAttackParts()
    {
        foreach (EnemyPartHitDetector detector in attackParts)
        {
            Collider collider = detector.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }*/
    private void InitializeAttackParts()
    {
        foreach (EnemyPartHitDetector detector in attackParts)
        {
            detector.OnPlayerHit.AddListener(HandlePlayerHit);
        }
    }

    private void HandlePlayerHit(Collider playerCollider)
    {
        GameManager.Instance.EnemyDealDamage(enemy,currentMovesetIndex);
       
        // Implement your logic to handle the player being hit
        Debug.Log("Player hit by enemy attack part!");
    }
}