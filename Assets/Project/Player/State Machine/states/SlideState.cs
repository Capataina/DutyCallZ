using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : BaseState
{
    public SlideState(StateMachine stateMachine, StateFactory stateFactory) : base(stateMachine, stateFactory)
    {
    }

    public override void CheckSwitchSubStates()
    {
    }

    public override void EnterState()
    {
        stateMachine.playerCollider.height = 1;
        stateMachine.playerCollider.center = new Vector3(0, 0.5f, 0);
        stateMachine.slidingSpeed = stateMachine.slideSpeed;
        stateMachine.playerAnimationController.Play("PlayerEnterSlide", 0);
    }

    public override void ExitState()
    {
        stateMachine.playerCollider.height = 2;
        stateMachine.playerCollider.center = Vector3.zero;
        stateMachine.playerAnimationController.Play("PlayerExitSlide", 0);
    }


    public override void UpdateState()
    {
        stateMachine.slidingSpeed -= stateMachine.slidingDeceleration * Time.deltaTime;
        stateMachine.velocity = stateMachine.velocity.normalized * stateMachine.slidingSpeed;
        Vector3 horizontalInput = new Vector3(stateMachine.input.x, 0, 0);
        Vector3 rotatedInput = Quaternion.LookRotation(stateMachine.velocity.normalized, Vector3.up) * horizontalInput;
        stateMachine.playerController.Move(
            (stateMachine.velocity + rotatedInput.normalized * stateMachine.slideHorizonalSpeed) * Time.deltaTime);
    }
}
