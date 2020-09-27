using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateController stateController;

    public abstract void CheckTransitions();

    public abstract void Act();

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

   //constructor
   public State(StateController stateController)
    {
        this.stateController = stateController;
    }
}
