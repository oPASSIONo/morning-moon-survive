using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

public class AgentTool : MonoBehaviour
{
    [SerializeField] private GameObject pickaxe;
    //[SerializeField] private GameObject shovel;
    // Add other tool GameObject references as needed

<<<<<<< HEAD
    [SerializeField] private Animator animator;

    public bool onWeapon = false;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

=======
>>>>>>> parent of e20685a (Fix animation but client cant pick up item)
    // Method to activate the appropriate tool based on parameters
    public void ActivateTool(ToolItemSO toolItemSo,List<ItemParameter> itemState)
    {
        
        // Deactivate all tool game objects first
        DeactivateAllTools();
        switch (toolItemSo.Name)
        {
            case "Pickaxe":
                ActivateTool(pickaxe);
                Debug.Log(pickaxe);
                break;
            case "Shovel":
                //ActivateTool(shovel);
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
            onWeapon = true;
        }
    }

    // Helper method to deactivate all tool game objects
    private void DeactivateAllTools()
    {
        pickaxe.SetActive(false);
        //shovel.SetActive(false);
        // Deactivate other tool game objects as needed
    }
}