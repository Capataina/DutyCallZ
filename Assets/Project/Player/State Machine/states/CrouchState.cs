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
        stateMachine.playerAnimationController.Play("PlayerCrouch", 0);
        stateMachine.playerCollider.height = 1;
        stateMachine.playerCollider.center = new Vector3(0, 0.5f, 0);
    }

    public override void ExitState()
    {
        stateMachine.playerCollider.height = 2;
        stateMachine.playerCollider.center = Vector3.zero;
        stateMachine.playerAnimationController.Play("PlayerUncrouch", 0);
    }
    public override void UpdateState()
    {
    }
}
