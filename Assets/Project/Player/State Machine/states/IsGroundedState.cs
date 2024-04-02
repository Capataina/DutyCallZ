using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedState : BaseState
{

    public IsGroundedState(StateMachine stateMachine, StateFactory stateFactory, bool hasLanded) : base(stateMachine, stateFactory)
    {
        if (hasLanded)
        {
            InitializeSubState(stateFactory.Land());
        }
        else
        {
            InitializeSubState(stateFactory.Idle());
        }
    }

    public override void CheckSwitchSubStates()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSubState(stateFactory.Jump());
        }
        switch (subState)
        {
            case IdleState:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    SwitchSubState(stateFactory.Crouch());
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchSubState(stateFactory.Jump());
                }
                else if (stateMachine.input.magnitude > 0)
                {
                    SwitchSubState(stateFactory.Walk());
                }
                break;
            case WalkState:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    SwitchSubState(stateFactory.Crouch());
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchSubState(stateFactory.Jump());
                }
                else if (stateMachine.input.magnitude == 0)
                {
                    SwitchSubState(stateFactory.Idle());
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    SwitchSubState(stateFactory.Sprint());
                }
                break;
            case SprintState:
                if (stateMachine.input.magnitude == 0)
                {
                    SwitchSubState(stateFactory.Idle());
                }
                else if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    SwitchSubState(stateFactory.Walk());
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    SwitchSubState(stateFactory.Crouch());
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchSubState(stateFactory.Jump());
                }
                break;
            case CrouchState:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    SwitchSubState(stateFactory.Idle());
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    SwitchSubState(stateFactory.Jump());
                }
                break;
            case LandState:
                SwitchSubState(stateFactory.Idle());
                break;
            default:
                break;
        }
    }

    public override void EnterState()
    {
        stateMachine.velocity.y = 0;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }
}
