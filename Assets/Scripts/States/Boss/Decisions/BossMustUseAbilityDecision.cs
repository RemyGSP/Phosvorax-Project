using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossDecisions/UsingAbility1")]
public class BossMustUseAbilityDecision : Decision
{
    [SerializeField] int index = 0;
    public override bool Decide(StateMachine stateMachine)
    {
        float[] abilitiesCD = stateMachine.GetComponent<BossTimers>().abilityCD;
        float[] abilityTimers = stateMachine.GetComponent<BossTimers>().abilityTimers;

        return stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && abilityTimers[index] >= abilitiesCD[index];
    }
}
