using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public class AgentTool : MonoBehaviour
{
    [SerializeField] private List<ToolItemSO> toolsList; // List of tool prefabs
    [SerializeField] private Transform playerHandTransform; // Transform of the player's hand
    
    public static event Action OnDrawWeapon;

    public ToolItemSO currentTool;
    private GameObject currentToolInstance; // Instance of the current tool
    
    private void Start()
    {
        
        DeactivateAllTools();
        
    }

    /// <summary>
    /// Activates the appropriate tool based on parameters.
    /// </summary>
    /// <param name="toolItemSo">The tool item scriptable object.</param>
    /// <param name="itemState">The list of item parameters.</param>
    public void ActivateTool(ToolItemSO toolItemSo, List<ItemParameter> itemState)
    {
        // Deactivate the current tool instance first
        DeactivateAllTools();
        currentTool = toolItemSo;
        
        foreach (ToolItemSO tool in toolsList)
        {
            if (currentTool.Name == tool.Name)
            {
                OnDrawWeapon?.Invoke();
                InstantiateTool(tool);
                break;
            }
        }
    }

    /// <summary>
    /// Helper method to instantiate a specific tool.
    /// </summary>
    /// <param name="tool">The tool scriptable object.</param>
    private void InstantiateTool(ToolItemSO tool)
    {
        if (tool != null)
        {
            currentToolInstance = Instantiate(tool.ItemPrefab, playerHandTransform);
            currentToolInstance.transform.localPosition = tool.ToolPositionInHand;
            currentToolInstance.transform.localRotation = Quaternion.Euler(tool.ToolRotationInHand); // Set the rotation
            currentToolInstance.transform.localScale = tool.ToolScale; // Set the scale
        }
    }

    /// <summary>
    /// Helper method to deactivate all tool instances.
    /// </summary>
    public void DeactivateAllTools()
    {
        if (currentToolInstance != null)
        {
            Destroy(currentToolInstance);
        }
    }
}