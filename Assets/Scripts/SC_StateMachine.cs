using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SC_StateMachine : MonoBehaviour
{
    private enum PlayerStates { Patrolling, Chasing }

    private Enum currentState;

    protected void SetState(Enum state)
    {
        if (currentState != null)
            CallMethod("ExitState");

        currentState = state;

        CallMethod("EnterState");
    }

    private void CallMethod(string suffix)
    {
        string methodName = currentState + "_" + suffix;

        Delegate method = Delegate.CreateDelegate(typeof(Action), this, methodName, false, false);

        ((Action)method)();
    }
}
