using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StateManager
{
    public StateBase currentState;
    public NPCController2.States currentStateName;

    public void ChangeState(StateBase newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }
}
