using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampTalkState : State
{
    Transform destination;
    float removeTextDistance = 2.5f;
    float surviveRange = 2f;

    public ChampTalkState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (!stateController.CheckIfInRange(removeTextDistance))
        {
            stateController.SetState(new ChampIdleState(stateController));
        }

        if (stateController.CheckIfInRange(surviveRange))
        {
            stateController.gameController.WinGame();
        }
    }

    public override void Act()
    {
        //TODO: Champ talks to player
        //if (stateController.targetToChase != null)
        //{
        //    destination = stateController.targetToChase.transform;
        //    stateController.ai.SetTarget(destination);
        //}
    }

    public override void OnStateEnter()
    {
        stateController.seeChampText();
    }
}
