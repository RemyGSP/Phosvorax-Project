using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/Ability2Decision")]
public class Ability2Decision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (AbilityManager.instance.throwAbility && PlayerTimers.Instance.abilityTimers[1] > PlayerTimers.Instance.abilityCD[1])
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 2)
            {
                HUDManager.instance.AbilityStartCooldown(1);
                aux = true;
            }

        }
        return aux;
    }
}