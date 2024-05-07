using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : State
{
    private bool grounded;

    private Vector3 currentVeclocity;
    
    
    private Vector3 cVeclocity;
    public StandingState(AnimationStateController _stateControl, StateMachine _stateMachine) : base(_stateControl,
        _stateMachine)
    {
        stateControl = _stateControl;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVeclocity = Vector3.zero;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        stateControl.animator.SetFloat("speed" , input.magnitude, stateControl.speedDampTime , Time.deltaTime);
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        currentVeclocity =
            Vector3.SmoothDamp(currentVeclocity, velocity, ref cVeclocity, stateControl.velocityDampTime);
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
