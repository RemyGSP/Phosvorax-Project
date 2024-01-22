using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerDecisions/Ability1Decision")]
public class Ability1Decision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsAttacking())
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 1)
            {
                aux = true;
            }

        }
        return aux;
    }
}
