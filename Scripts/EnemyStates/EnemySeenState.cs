using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySeenState : State
{
    Transform destination;

    public EnemySeenState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        //If not seen, transition back to EnemyChaseState
        if (stateController.CheckIfVisible() == false)
        {
            stateController.SetState(new EnemyChaseState(stateController));
        }
    }

    public override void Act()
    {
        //Do nothing, frozen when seen
    }

    public override void OnStateEnter()
    {
        UnityEngine.Debug.Log("Seen");
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 0f;
        }
    }
}
