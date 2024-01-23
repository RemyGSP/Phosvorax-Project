using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/AttackingDecision")]
public class IsAttackingDecision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsAttacking() && !PlayerInputController.Instance.IsUsingAbility())
        {
            aux = true;
        }
        return aux;
    }
}
