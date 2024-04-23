using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryState : State
{
    public HungryState(AnimationStateController _stateControl, StateMachine _stateMachine) : base(_stateControl,
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
    }
}
