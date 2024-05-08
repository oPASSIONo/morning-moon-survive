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

    private Vector3 cVelocity;
    
    public CombatState(AnimationStateController _stateControl, StateMachine _stateMachine) : base(_stateControl,_stateMachine)

    {
        stateControl = _stateControl;
        stateMachine = _stateMachine;
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateControl.draw = true;
        attack = false;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        
        stateControl.animator.SetTrigger("drawWeapon"); 
        stateControl.animator.SetFloat("speed" , 0f);

 
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

        if (stateControl.draw == false)
        {
            stateControl.animator.SetTrigger("sheathWeapon");
            stateMachine.ChangeState(stateControl.standing);
        }
        if (attack)
        {
            stateControl.animator.SetTrigger("attack");
            stateMachine.ChangeState(stateControl.attacking);
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
