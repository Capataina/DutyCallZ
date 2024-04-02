using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseState
{
    protected BaseState subState;
    protected BaseState superState;
    protected StateMachine stateMachine;
    protected StateFactory stateFactory;

    public BaseState(StateMachine stateMachine, StateFactory stateFactory)
    {
        this.stateMachine = stateMachine;
        this.stateFactory = stateFactory;

    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void CheckSwitchSubStates();
    public abstract void ExitState();

    public void InitializeSubState(BaseState initialSubState)
    {
        if (initialSubState != null)
        {
            subState = initialSubState;
            subState.EnterState();
        }
    }

    public void BaseUpdate()
    {
        UpdateState();
        if (subState != null)
        {
            subState.BaseUpdate();
        }
        CheckSwitchSubStates();
    }

    public void SwitchSubState(BaseState newState)
    {
        if (subState.GetType() != newState.GetType())
        {
            subState.ExitState();
            subState = newState;
            subState.EnterState();
        }
    }

}
