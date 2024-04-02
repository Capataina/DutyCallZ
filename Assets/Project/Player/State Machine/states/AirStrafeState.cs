using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrafeState : BaseState
{
    public AirStrafeState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
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
        //stateMachine.velocity += stateMachine.movementDirection.normalized * stateMachine.airStrafeSpeed * Time.deltaTime;
        stateMachine.playerController.Move(stateMachine.movementDirection.normalized * stateMachine.lastGroundSpeed * Time.deltaTime);
    }
}
