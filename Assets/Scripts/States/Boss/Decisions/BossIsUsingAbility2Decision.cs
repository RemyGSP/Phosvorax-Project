using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossDecisions/UsingAbility2")]
public class BossIsUsingAbility2Decision : Decision
{
    // Start is called before the first frame update
    public override bool Decide(StateMachine stateMachine)
    {
        //Abilities[] abilities = stateMachine.GetComponent
        bool aux = false;
        if (stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && stateMachine.GetComponent<BossTimers>().abilityTimers[1] >= stateMachine.GetComponent<BossTimers>().abilityCD[1])
        {
            aux = true;
        }
        return aux;
    }
}
