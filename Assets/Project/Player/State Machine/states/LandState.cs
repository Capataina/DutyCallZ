using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : BaseState
{
    public LandState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
    }

    public override void CheckSwitchSubStates()
    {
    }

    public override void EnterState()
    {
        stateMachine.playerAnimationController.Play("PlayerLand", 0);
    }

    public override void ExitState()
    {
    }


    public override void UpdateState()
    {
    }
}
