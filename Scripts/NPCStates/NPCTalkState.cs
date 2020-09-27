using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkState : State
{
    Transform destination;
    float stopTalkingRange = 2.5f;
    public NPCTalkState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (!stateController.CheckIfInRange(stopTalkingRange))
        {
            stateController.SetState(new NPCIdleState(stateController));
        }
    }

    public override void Act()
    {
        
    }

    public override void OnStateEnter()
    {
        stateController.SeeHelperText();
    }
}
