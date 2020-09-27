using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChampTrailState : State
{
    Transform destination;
    float winDistance = 2.5f;

    public ChampTrailState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange(winDistance))
        {
            stateController.SetState(new ChampTalkState(stateController));
        }
    }

    public override void Act()
    {
        UnityEngine.Debug.Log("helping player find me");
        stateController.LookAt();
    }

    public override void OnStateEnter()
    {
        stateController.removeChampText();
    }
}
