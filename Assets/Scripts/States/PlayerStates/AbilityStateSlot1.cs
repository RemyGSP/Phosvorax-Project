using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "States/AbilityState1")]
public class AbilityStateSlot1 : States
{
    public AbilityList abilityListObj;
    public AbilityStateSlot1(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override States CheckTransitions()
    {
        States newGameState = null;
        if (0 == 0)
        {
            newGameState = base.CheckTransitions();
        }

        return newGameState;
    }
    public override void Start()
    {

        Debug.Log("lakingsli");

    }

    public override void FixedUpdate()
    {

    }
    public override void Update()
    {
        return;
    }

    public override void OnExitState()
    {
        return;
    }
}