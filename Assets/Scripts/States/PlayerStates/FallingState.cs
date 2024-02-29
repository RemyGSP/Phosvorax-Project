using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/FallingState")]
public class FallingState : States
{
    public FallingState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override States CheckTransitions()
    {

        if (PlayerReferences.instance.CheckIfGrounded())
        {
            return base.CheckTransitions();
        }
        else
        {
            return null;
        }
    }

    public override void OnExitState()
    {
        //PlayerReferences.instance.GetPlayerAnimator().SetBool("idle", true);
    }

    public override void Update()
    {
    }

}
