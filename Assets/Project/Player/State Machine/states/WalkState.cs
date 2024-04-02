using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{
    public WalkState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
        InitializeSubState(stateFactory.Move(stateMachine.walkSpeed));
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
    }
}
