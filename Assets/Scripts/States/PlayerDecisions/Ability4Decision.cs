using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/Ability4Decision")]
public class Ability4Decision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsAttacking())
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 4)
            {
                aux = true;
            }

        }
        return aux;
    }
}
