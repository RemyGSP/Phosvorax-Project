using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerDecisions/Ability1Decision")]
public class Ability1Decision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsUsingAbility1() && PlayerTimers.Instance.abilityTimers[0] > PlayerTimers.Instance.abilityCD[0])
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 1)
            {
                HUDManager.instance.AbilityStartCooldown(0);
                aux = true;
            }

        }
        return aux;
    }
}
