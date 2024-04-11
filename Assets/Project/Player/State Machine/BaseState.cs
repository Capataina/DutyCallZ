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

    // Triggered once when entering state
    public abstract void EnterState();
    // Triggered every frame as long as state is active
    public abstract void UpdateState();
    // Triggered every frame, used to check if state switches are necessery
    public abstract void CheckSwitchSubStates();
    // Triggerd once when leaving state
    public abstract void ExitState();

    // Activat the initial substate if any
    public void InitializeSubState(BaseState initialSubState)
    {
        if (initialSubState != null)
        {
            subState = initialSubState;
            subState.EnterState();
        }
    }

    // Call the update and state switch checks down the chain
    // of active states
    public void BaseUpdate()
    {
        UpdateState();
        if (subState != null)
        {
            subState.BaseUpdate();
        }
        CheckSwitchSubStates();
    }

    // Switch the substate of this state
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
