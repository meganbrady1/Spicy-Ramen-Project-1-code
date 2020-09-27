using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowState : State
{
    float talkingRange = 2.5f;
    public NPCFollowState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange(talkingRange))
        {
            stateController.SetState(new NPCTalkState(stateController));
        }
    }

    public override void Act()
    {
        Debug.Log("moving with player");
        stateController.LookAt();
    }

    public override void OnStateEnter()
    {
        stateController.RemoveHelperText();
    }
}

