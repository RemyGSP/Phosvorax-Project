using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/Ability3Decision")]
public class Ability3Decision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsAttacking())
        {
            int currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            if (currentAbility == 3)
            {
                aux = true;
            }

        }
        return aux;
    }
}