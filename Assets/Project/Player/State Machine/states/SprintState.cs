using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SprintState : BaseState
{
    public SprintState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
        InitializeSubState(stateFactory.Move(stateMachine.sprintSpeed));
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
