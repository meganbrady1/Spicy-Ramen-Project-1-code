using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : State
{
    Transform destination;
    private float killRange = 2;
    public EnemyChaseState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        //Lose game if too close
        if (stateController.CheckIfInRange(killRange))
        {
            stateController.gameController.LoseGame();
        }

        //If seen, change to seen state
        if (stateController.CheckIfVisible() == true)
        {
            stateController.SetState(new EnemySeenState(stateController));
        }
    }

    public override void Act()
    {
        if (stateController.targetToChase != null)
        {
            destination = stateController.targetToChase.transform;
            stateController.ai.SetTarget(destination);
        }
    }

    public override void OnStateEnter()
    {
        UnityEngine.Debug.Log("Chase");
        stateController.ai.agent.speed = 3f;
    }
}
