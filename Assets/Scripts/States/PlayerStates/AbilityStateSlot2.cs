using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/AbilityState2")]
public class AbilityStateSlot2 : States
{

    [SerializeField] private AbilityList abilityListObj;
    [SerializeField] private int usingAbilityNumber;
    private float statetime;
    private float statetimecount;
    public AbilityStateSlot2(GameObject stateGameObject) : base(stateGameObject)
    {

    }
    public override States CheckTransitions()
    {
        States newGameState = null;
        if (statetimecount > statetime)
        {
            newGameState = base.CheckTransitions();
        }

        return newGameState;
    }

    public override void Start()
    {
        statetimecount = 0;
        statetime = abilityListObj.abilityList[usingAbilityNumber].onststetime;
        abilityListObj.abilityList[usingAbilityNumber].OnEnterState(stateGameObject);
    }

    public override void FixedUpdate()
    {
        statetimecount += Time.deltaTime;
        abilityListObj.abilityList[usingAbilityNumber].AbilityUpdate();
    }
    public override void Update()
    {
        return;
    }

    public override void OnExitState()
    {
        abilityListObj.abilityList[usingAbilityNumber].OnExitState();
    }
}
