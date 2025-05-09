using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public bool isPerformingAction { get; private set; } = false;
    private Stamina staminaComponent;
    [SerializeField] private PlayerFarmingInteractor farmingInteractor;


    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.OnAction += PerformAction;
        staminaComponent = GetComponent<Stamina>();
    }

    private void PerformAction(object sender, EventArgs e)
    {
        AgentTool agentTool = GetComponent<AgentTool>();
        if (agentTool.currentTool!=null && !isPerformingAction)
        {
            if (agentTool.currentTool.ItemCategory==ItemCategory.Farming)
            {
                if (farmingInteractor.SelectedLand!=null)
                {
                    staminaComponent.TakeAction();
                    if (staminaComponent.isAction)
                    {
                        farmingInteractor.ToolInteract();
                        //playerAnimation.
                    }
                }
            }
            
        }
        else if (agentTool.CurrentSeed!=null&& !isPerformingAction)
        {
            if (farmingInteractor.SelectedLand!=null)
            {
                staminaComponent.TakeAction();
                if (staminaComponent.isAction)
                {
                    farmingInteractor.SeedInteract();
                    //playerAnimation.
                }
            }
        }
    }
    
}
