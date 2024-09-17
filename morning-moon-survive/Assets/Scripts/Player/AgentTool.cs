using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public class AgentTool : MonoBehaviour
{
    [SerializeField] private Transform playerHandTransform; // Transform of the player's hand

    public static event Action OnDrawWeapon;

    public ToolItemSO currentTool;
    private GameObject currentToolInstance; // Instance of the current tool

    public SeedItemSO CurrentSeed { get; private set; }
    private GameObject currentSeedInstance;
    
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
        DeactivateAllHolding();
        currentTool = toolItemSo;
        CurrentSeed = null;
        
        OnDrawWeapon?.Invoke();
        InstantiateTool(toolItemSo);
    }

    public void ActivateSeed(SeedItemSO seedItemSo,List<ItemParameter> itemState)
    {
        DeactivateAllHolding();
        CurrentSeed = seedItemSo;
        currentTool = null;
        InstantiateSeed(seedItemSo);
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

    private void InstantiateSeed(SeedItemSO seed)
    {
        if (seed!=null)
        {
            currentSeedInstance = Instantiate(seed.ItemPrefab, playerHandTransform);
            currentSeedInstance.transform.localPosition = seed.SeedPositionInHand;
            currentSeedInstance.transform.localRotation=Quaternion.Euler(seed.SeedRotationInHand);
            currentSeedInstance.transform.localScale = seed.SeedScale;
        }
    }

    /// <summary>333
    /// Helper method to deactivate all tool instances.
    /// </summary>
    private void DeactivateAllHolding()
    {
        DeactivateAllTools();
        DeactivateAllSeeds();
    }
    public void DeactivateAllTools()
    {
        if (currentToolInstance != null)
        {
            Destroy(currentToolInstance);
        }
    }
    public void DeactivateAllSeeds()
    {
        if (currentSeedInstance != null)
        {
            Destroy(currentSeedInstance);
        }
    }
}