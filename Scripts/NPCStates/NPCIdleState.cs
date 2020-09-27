using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : State
{
    Transform destination;
    float playerDetectionRange = 4f;

    public NPCIdleState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange(playerDetectionRange))
        {
            stateController.SetState(new NPCFollowState(stateController));
        }
    }

    public override void Act()
    {
        //if (destination == null || stateController.ai.DestinationReached())
        //{
        //    destination = stateController.GetNextNavPoint();
        //    stateController.ai.SetTarget(destination);
        //}
    }

    public override void OnStateEnter()
    {
        //destination = stateController.GetNextNavPoint();
        //if (stateController.ai.agent != null)
        //{
        //    stateController.ai.agent.speed = 1f;
        //}

        //stateController.ai.SetTarget(destination);
        //stateController.ChangeColor(Color.blue);
    }
}
