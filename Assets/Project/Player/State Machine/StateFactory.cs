using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    For a unified interface for state generation
*/
public class StateFactory
{
    StateMachine stateMachine;

    public StateFactory(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public BaseState Crouch()
    {
        return new CrouchState(stateMachine, this);
    }

    public BaseState Jump()
    {
        return new JumpState(stateMachine, this);
    }

    public BaseState Sprint()
    {
        return new SprintState(stateMachine, this);
    }

    public BaseState Walk()
    {
        return new WalkState(stateMachine, this);
    }

    public BaseState Move(float speed)
    {
        return new MoveState(stateMachine, this, speed);
    }

    public BaseState Idle()
    {
        return new IdleState(stateMachine, this);
    }

    public BaseState Slide()
    {
        return new SlideState(stateMachine, this);
    }

    public BaseState IsGrounded(bool hasLanded)
    {
        return new IsGroundedState(stateMachine, this, hasLanded);
    }

    public BaseState InAir()
    {
        return new InAirState(stateMachine, this);
    }

    public BaseState Root()
    {
        return new RootState(stateMachine, this);
    }

    public BaseState AirStrafe()
    {
        return new AirStrafeState(stateMachine, this);
    }

    public BaseState Land()
    {
        return new LandState(stateMachine, this);
    }
}
