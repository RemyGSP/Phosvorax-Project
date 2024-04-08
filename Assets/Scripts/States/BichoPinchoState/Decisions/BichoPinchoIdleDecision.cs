using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/BichoPinchoIdle")]
public class BichoPinchoIdleDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        BichoPinchoReferences infoContainer = stateMachine.gameObject.GetComponent<BichoPinchoReferences>();
        if (infoContainer.CheckAttackTimer() && !infoContainer.CheckIfStunned() && !infoContainer.IsAttacking())
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
