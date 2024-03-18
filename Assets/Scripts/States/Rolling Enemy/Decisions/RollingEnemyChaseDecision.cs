using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/RollingEnemyChasingDecision")]

public class RollingEnemyChaseDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool isStuned = stateMachine.GetComponent<RollingEnemyReferences>().GetIsStunned();
        bool aux = false;

        if (!isStuned)
        {
            aux = true;
        }
        return aux;
    }
}
