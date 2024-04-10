using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ToolItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "USE";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState)
        {
            // Get the AgentTool component attached to the character
            AgentTool agentTool = character.GetComponent<AgentTool>();
    
            // Check if the AgentTool component exists
            if (agentTool != null)
            {
                // Call the ActivateTool method of the AgentTool component
                agentTool.ActivateTool(this,itemState);
                return true; // Return true if the action was performed successfully
            }
            else
            {
                // Handle the case where the AgentTool component is not found
                Debug.LogError("AgentTool component not found on character.");
                return false;
            }
        }
    }
}
