using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BossDecisions/UsingAbility4")]
public class BossUsingAbility4Decision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        Abilities[] abilities = stateMachine.GetComponent<GetBestAbilityToUse>().getAbilityArrayWithPoints();
        float[] abilitiesCD = stateMachine.GetComponent<BossTimers>().abilityCD;
        float[] abilityTimers = stateMachine.GetComponent<BossTimers>().abilityTimers;
        int index = 0;
        bool found = false;
        bool stopAbility = false;
        do
        {
            if (stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && stateMachine.GetComponent<BossTimers>().abilityTimers[index] >= stateMachine.GetComponent<BossTimers>().abilityCD[index] && abilities[index].index != 3)
            {
                stopAbility = true;
            }

            if (stateMachine.GetComponent<BossReferences>().GetCanUseAbility() && stateMachine.GetComponent<BossTimers>().abilityTimers[index] >= stateMachine.GetComponent<BossTimers>().abilityCD[index] && abilities[index].index == 3)
            {
                aux = true;
                found = true;
            }
            index++;
        } while (!found && index < abilities.Length);
        if (stopAbility)
        {
            aux = false;
        }
        return aux;
    }
}
