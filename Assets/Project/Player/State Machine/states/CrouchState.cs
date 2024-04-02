using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    public CrouchState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
        InitializeSubState(stateFactory.Idle());
    }

    public override void CheckSwitchSubStates()
    {
        if (stateMachine.input.magnitude == 0)
        {
            SwitchSubState(stateFactory.Idle());
        }
        else if (stateMachine.input.magnitude > 0)
        {
            SwitchSubState(stateFactory.Move(stateMachine.crouchSpeed));
        }
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
