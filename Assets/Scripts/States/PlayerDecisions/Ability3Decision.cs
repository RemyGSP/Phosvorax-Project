using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/Ability3Decision")]
public class Ability3Decision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (AbilityManager.instance.throwAbility && PlayerTimers.Instance.abilityTimers[2] > PlayerTimers.Instance.abilityCD[2])
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 3)
            {
                HUDManager.instance.AbilityStartCooldown(2);
                aux = true;
            }

        }
        return aux;
    }
}
