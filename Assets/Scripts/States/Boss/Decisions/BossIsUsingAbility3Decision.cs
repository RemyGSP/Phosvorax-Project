using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BossDecisions/UsingAbility3")]
public class BossIsUsingAbility3Decision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        //Abilities[] abilities = stateMachine.GetComponent
        bool aux = false;
        if (stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && stateMachine.GetComponent<BossTimers>().abilityTimers[2] >= stateMachine.GetComponent<BossTimers>().abilityCD[2])
        {
            aux = true;
        }
        return aux;
    }
}
