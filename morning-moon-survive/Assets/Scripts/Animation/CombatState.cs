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

        //sheathWeapon = false;
        stateControl.draw = true;
        attack = false;
        //drawWeapon = true;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        
        stateControl.animator.SetTrigger("drawWeapon"); 
 
    }

    public override void HandleInput()
    {
        base.HandleInput();

        
        if (sheathAction.triggered)
        {
            //drawWeapon = false;
            stateControl.draw = false; // Set drawWeapon to false if sheathing the weapon
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
            Debug.Log("Here");
            stateMachine.ChangeState(stateControl.attacking);
        }
        
        if (stateControl.draw == false)
        {
            // Sheathe the weapon and transition to standing state
            stateControl.animator.SetTrigger("sheathWeapon");
            stateMachine.ChangeState(stateControl.standing);
        }
     
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentVelocity =
            Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, stateControl.velocityDampTime);
    }
    
}
