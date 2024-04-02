using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
    }

    public override void CheckSwitchSubStates()
    {
    }

    public override void EnterState()
    {
        stateMachine.velocity.y = stateMachine.jumpVelocity;
    }

    public override void ExitState()
    {
    }


    public override void UpdateState()
    {
    }
}
