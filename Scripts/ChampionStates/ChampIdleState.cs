using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampIdleState : State
{
    Transform destination;
    float trailDistance = 10f;

    public ChampIdleState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange(trailDistance))
        {
            stateController.SetState(new ChampTrailState(stateController));
        }
    }

    public override void Act()
    {
      
    }

    public override void OnStateEnter()
    {
        
    }
}
