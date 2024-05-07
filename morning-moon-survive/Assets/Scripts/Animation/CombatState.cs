using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : State
{
    private Vector3 currentVelocity;
    private bool sheathWeapon;
    private float playerSpeed;
    private bool drawWeapon;

    private bool attack;
    private AgentTool agentTool;

    private Vector3 cVelocity;
    
    public CombatState(AnimationStateController _stateControl, StateMachine _stateMachine, AgentTool _agentTool) : base(_stateControl,_stateMachine)

    {
        stateControl = _stateControl;
        stateMachine = _stateMachine;
        agentTool = _agentTool;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateControl.draw = true;
        attack = false;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        
        stateControl.animator.SetTrigger("drawWeapon"); 
 
    }

    public override void HandleInput()
    {
        base.HandleInput();

        
        if (sheathAction.triggered)
        {
            stateControl.draw = false;
        }

        if (attackAction.triggered)
        {
            attack = true;
        }

        
        velocity = new Vector3(input.x, 0, input.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        stateControl.animator.SetFloat("speed" , input.magnitude, stateControl.speedDampTime, Time.deltaTime);

        if (attack == true)
        {
            stateMachine.ChangeState(stateControl.attacking);
        }
        
        if (stateControl.draw == false)
        {
            stateControl.animator.SetFloat("speed", 0f);
            stateControl.animator.SetTrigger("sheathWeapon");
            stateMachine.ChangeState(stateControl.standing);
            if (agentTool != null)
            {
                agentTool.DeactivateAllTools();
                Debug.Log("HERERERERERERRERER");
            }
        }
     
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentVelocity =
            Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, stateControl.velocityDampTime);
    }
    
    public override void Exit()
    {
        base.Exit();
        
    }
    
}
