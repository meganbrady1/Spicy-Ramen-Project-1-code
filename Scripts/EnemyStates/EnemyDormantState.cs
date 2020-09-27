using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyDormantState : State
{
    Transform destination;

    public EnemyDormantState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new EnemyChaseState(stateController));
        }
    }

    public override void Act()
    {
        //do nothing (dormant)
    }

    public override void OnStateEnter()
    {
        UnityEngine.Debug.Log("Dormant");
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 0f;
        }
    }
}
