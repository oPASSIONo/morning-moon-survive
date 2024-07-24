using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public class AgentTool : MonoBehaviour
{
    [SerializeField] private GameObject stoneAxe;
    [SerializeField] private GameObject boneAxe;
    
    public static event Action OnDrawWeapon;

    public ToolItemSO currentTool;

    
    private void Start()
    {
        
        DeactivateAllTools();
        
    }

    // Method to activate the appropriate tool based on parameters
    public void ActivateTool(ToolItemSO toolItemSo,List<ItemParameter> itemState)
    {
        // Deactivate all tool game objects first
        DeactivateAllTools();
        currentTool = toolItemSo;
        switch (toolItemSo.Name)
        {
            case "Stone Axe":
                OnDrawWeapon?.Invoke();
                ActivateTool(stoneAxe);
                break;
            case "Bone Axe":
                OnDrawWeapon?.Invoke();
                ActivateTool(boneAxe);
                break;
            // Add more cases for other tool types as needed
        }

    }

    // Helper method to activate a specific tool
    private void ActivateTool(GameObject tool)
    {
        if (tool != null)
        {
            tool.SetActive(true);
        }
    }

    // Helper method to deactivate all tool game objects
    public void DeactivateAllTools()
    {
        stoneAxe.SetActive(false);
        boneAxe.SetActive(false);
        // Deactivate other tool game objects as needed
    }
}