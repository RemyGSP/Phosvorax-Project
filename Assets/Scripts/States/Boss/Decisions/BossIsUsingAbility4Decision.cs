using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BossDecisions/UsingAbility4")]
public class BossUsingAbility4Decision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        //Abilities[] abilities = stateMachine.GetComponent
        bool aux = false;
        if (stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && stateMachine.GetComponent<BossTimers>().abilityTimers[3] >= stateMachine.GetComponent<BossTimers>().abilityCD[3])
        {
            aux = true;
        }
        return aux;
    }
}
