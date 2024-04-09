using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/RollingEnemyChasingDecision")]

public class RollingEnemyChaseDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool isStuned = stateMachine.GetComponent<RollingEnemyReferences>().GetIsStunned();
        bool canStart = stateMachine.GetComponent<EnemyReferences>().GetCanBeStarted();

        bool aux = false;

        if (!isStuned && canStart)
        {
            aux = true;
        }
        return aux;
    }
}
