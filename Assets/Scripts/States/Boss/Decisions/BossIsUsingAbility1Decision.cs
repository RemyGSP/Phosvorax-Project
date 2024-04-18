using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossDecisions/UsingAbility1")]
public class BossIsUsingAbility1Decision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        //Abilities[] abilities = stateMachine.GetComponent
        bool aux = false;
        if (stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && stateMachine.GetComponent<BossTimers>().abilityTimers[0] >= stateMachine.GetComponent<BossTimers>().abilityCD[0]  )
        {
            aux = true;
        }
        return aux;
    }
}
