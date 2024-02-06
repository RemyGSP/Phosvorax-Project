using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/FallingState")]
public class FallingState : States
{
    public FallingState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override States CheckTransitions()
    {
        States newPlayerState = null;

        bool notChanged = true;
        int counter = 0;
        if (PlayerReferences.instance.CheckIfGrounded())
        {
            while (notChanged)
            {
                newPlayerState = stateTransitions[counter].GetExitState(stateGameObject.GetComponent<StateMachine>());
                if (newPlayerState != null)
                {
                    notChanged = false;
                    newPlayerState.InitializeState(stateGameObject);
                    newPlayerState.Start();
                }
                if (counter < stateTransitions.Length - 1)
                {
                    counter++;
                }
                else
                {
                    notChanged = false;
                }
            }
        }
        return newPlayerState;
    }

    public override void Update()
    {
    }

}
