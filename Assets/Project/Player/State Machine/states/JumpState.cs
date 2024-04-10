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
        AnimationTools.CrossFadeToAnimationFT(stateMachine.playerAnimationController, "Neutral", 0.2f, 0);
    }

    public override void ExitState()
    {
    }


    public override void UpdateState()
    {
    }
}
