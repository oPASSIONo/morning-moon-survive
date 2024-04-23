using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public StateMachine StateMachine;

    private PlayerInput playerInput;
    public InputAction MoveAction;

    public State(PlayerInput playerInput , StateMachine stateMachine)
    {
        playerInput = playerInput;
        StateMachine = stateMachine;

        MoveAction = playerInput.PlayerControls.Move;
    }

    public virtual void Enter()
    {
        Debug.Log("Enter state : "+ this.ToString());
    }

    public virtual void Exit()
    {
        
    }
}
