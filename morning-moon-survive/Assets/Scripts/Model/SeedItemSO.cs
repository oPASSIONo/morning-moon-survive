using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class SeedItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "USE";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        [field: SerializeField] public Vector3 SeedPositionInHand { get; set; }
        [field: SerializeField] public Vector3 SeedRotationInHand { get; set; }
        [field: SerializeField] public Vector3 SeedScale { get; set; } // Added field for scale
        // Watering days required for each growth stage
        [SerializeField] public int daysToMedium; // Days required to reach medium size
        [SerializeField] public int daysToLarge;  // Days required to reach large size
        [SerializeField] public int daysToFull;  // Days required to reach full size
        [SerializeField] public int rotTime; // Set your desired rot time

        /*[field: SerializeField] public GameObject SmallPlant { get; set; }
        [field: SerializeField] public GameObject MediumPlant { get; set; }
        [field: SerializeField] public GameObject LargePlant { get; set; }
        [field: SerializeField] public GameObject FullPlant { get; set; }*/
        public bool PerformAction(GameObject character, List<ItemParameter> itemState,int amount)
        {
            AgentTool agentTool = character.GetComponent<AgentTool>();

            if (agentTool!=null)
            {
                agentTool.ActivateSeed(this,itemState);
                return true;
            }
            else
            {
                Debug.LogError("AgentTool component not found on character.");
                return false;
            }
        }
    }
}
