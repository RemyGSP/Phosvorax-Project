using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyDecisions/StunnedDecision")]
public class RollingEnemyStunnedDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {

        bool isStunned = stateMachine.GetComponent<RollingEnemyReferences>().GetIsStunned();
        bool aux = false;

        if (isStunned)
        {
            aux = true;
        }
        return aux;
    }
}
