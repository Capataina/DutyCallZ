using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RootState : BaseState
{
    public RootState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
        InitializeSubState(stateFactory.IsGrounded(false));
    }

    public override void CheckSwitchSubStates()
    {
        switch (subState)
        {
            case IsGroundedState:
                if (!stateMachine.isGrounded)
                {
                    SwitchSubState(stateFactory.InAir());
                }
                break;
            case InAirState:
                if (stateMachine.isGrounded)
                {
                    SwitchSubState(stateFactory.IsGrounded(true));
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
        stateMachine.playerController.Move(Vector3.up * stateMachine.velocity.y);
    }
}
