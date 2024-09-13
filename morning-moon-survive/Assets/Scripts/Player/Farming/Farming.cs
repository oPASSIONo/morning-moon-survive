using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Farming : MonoBehaviour
{
    public bool isPerformingAction { get; private set; } = false;
    private Stamina staminaComponent;
    private bool hasHit = false;
    [SerializeField] private PlayerFarmingInteractor farmingInteractor;


    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.OnAction += PerformAction;
        staminaComponent = GetComponent<Stamina>();
    }

    private void PerformAction(object sender, EventArgs e)
    {
        if (GetComponent<AgentTool>().currentTool.ItemCategory==ItemCategory.Farming && !isPerformingAction)
        {
            if (farmingInteractor.SelectedLand!=null)
            {
                staminaComponent.TakeAction();
                if (staminaComponent.isAction)
                {
                    // Reset the hasHit flag
                    hasHit = false;
                    farmingInteractor.ToolInteract();
                    //playerAnimation.
                }
            }
            
            
        }
    }
}
