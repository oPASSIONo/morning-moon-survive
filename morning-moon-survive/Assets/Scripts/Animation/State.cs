using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public AnimationStateController stateControl;
    public StateMachine stateMachine;

    public InputAction attackAction;
    public InputAction drawAction;
    public InputAction sheathAction;
    

    
    protected Vector3 velocity;
    protected Vector2 input;

    public State(AnimationStateController _stateControl, StateMachine _stateMachine)
    {
        stateControl = _stateControl;
        stateMachine = _stateMachine;

        //moveAction = stateControl.playerInput.PlayerControls.Move;

        stateControl.playerInput = new PlayerInput();
        stateControl.playerInput.PlayerControls.Enable();
        
        attackAction = stateControl.playerInput.PlayerControls.Action;
        drawAction = stateControl.playerInput.PlayerControls.DrawWeapon;
        sheathAction = stateControl.playerInput.PlayerControls.SheathWeapon;
        
    }
    
    public virtual void Enter()
    {
        Debug.Log("Enter state : " + this.ToString());
    }
    
    
    public virtual void HandleInput()
    {
        
    } 
    
    public virtual void LogicUpdate()
    {

    }
    
    public virtual void PhysicsUpdate()
    {
        
    }
    public virtual void Exit()
    {
        //Debug.Log("Exit");
    }
}
