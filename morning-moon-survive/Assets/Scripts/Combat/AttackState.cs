using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    private float timePassed;

    private float clipLength;

    private float clipSpeed;

    private bool attack;

    public AttackState(AnimationStateController _stateControl, StateMachine _stateMachine) : base(_stateControl, _stateMachine)
    {
        stateControl = _stateControl;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        
        attack = false;
        stateControl.animator.applyRootMotion = true;
        timePassed = 0f;
        stateControl.animator.SetTrigger("attack");
        stateControl.animator.SetFloat("speed" , 0f);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (attackAction.triggered)
        {
            attack = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;
        clipLength = stateControl.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = stateControl.animator.GetCurrentAnimatorStateInfo(1).speed;

        if (stateControl.draw == true)
        {
            if (timePassed >= clipLength / clipSpeed && attack) 
            {
                stateControl.animator.SetFloat("speed", 0f);
                stateMachine.ChangeState(stateControl.attacking);
            }
        
            if (timePassed >= clipLength / clipSpeed ) 
            {
                stateControl.animator.SetTrigger("move");
                stateMachine.ChangeState(stateControl.combatting);
            }
        }
       
    }

    public override void Exit()
    {
        base.Exit();

        stateControl.animator.applyRootMotion = false;
    }
   
}
