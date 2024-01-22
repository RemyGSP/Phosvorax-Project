using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/Ability2Decision")]
public class Ability2Decision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsAttacking())
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 2)
            {
                aux = true;
            }

        }
        return aux;
    }
}
