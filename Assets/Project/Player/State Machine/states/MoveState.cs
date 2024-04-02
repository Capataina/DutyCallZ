using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    float speed;

    public MoveState(StateMachine stateMachine, StateFactory stateFactory, float speed) : base(stateMachine, stateFactory)
    {
        this.speed = speed;
    }

    public override void CheckSwitchSubStates()
    {
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }


    public override void UpdateState()
    {
        stateMachine.velocity = stateMachine.movementDirection.normalized * speed;
        stateMachine.lastGroundSpeed = speed;
        stateMachine.playerController.Move(stateMachine.velocity * Time.deltaTime);
    }
}
