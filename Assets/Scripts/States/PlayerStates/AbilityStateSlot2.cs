using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/AbilityState2")]
public class AbilityStateSlot2 : Ability
{

    public AbilityList abilityListObj;
    public AbilityStateSlot2(GameObject stateGameObject) : base(stateGameObject)
    {

    }
    public override States CheckTransitions()
    {
        States newGameState = null;
        if (0==0)
        {    
            newGameState = base.CheckTransitions();
        }

        return newGameState;
    }

    public override void Start()
    {
      
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
