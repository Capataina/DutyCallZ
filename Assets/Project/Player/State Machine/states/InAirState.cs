using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : BaseState
{
    public InAirState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
        InitializeSubState(stateFactory.Idle());
    }

    public override void CheckSwitchSubStates()
    {
        switch (subState)
        {
            case IdleState:
                if (stateMachine.input.magnitude > 0)
                {
                    SwitchSubState(stateFactory.AirStrafe());
                }
                break;
            case MoveState:
                if (stateMachine.input.magnitude == 0)
                {
                    SwitchSubState(stateFactory.Idle());
                }
                break;
            default:
                break;
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
        if (stateMachine.velocity.y > -stateMachine.terminalVelocity)
        {
            stateMachine.velocity.y -= stateMachine.gravity;
        }
    }
}
