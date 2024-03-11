using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/AttackingDecision")]
public class IsAttackingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsAttacking() && !PlayerInputController.Instance.IsUsingAbility1() && !PlayerInputController.Instance.IsUsingAbility2() && PlayerTimers.Instance.playerBasicAttackTimer > PlayerTimers.Instance.playerBasicAttackCD)
        {
            aux = true;
        }
        return aux;
    }
}
