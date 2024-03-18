using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/RollingEnemyChasingDecision")]

public class RollingEnemyChaseDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 enemiePos = stateMachine.GetComponent<EnemyReferences>().GetEnemyCoordinates();
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), enemiePos);
        float distanceToSeePlayer = stateMachine.GetComponent<EnemyReferences>().GetDistanceToSeePlayer();
        float maxAttackDistance = stateMachine.GetComponent<EnemyReferences>().GetMaxAttackPosition();
        bool isStuned = stateMachine.GetComponent<RollingEnemyReferences>().GetIsStunned();
        bool aux = false;

        if (distance <= distanceToSeePlayer && distance > maxAttackDistance && !isStuned)
        {
            aux = true;
        }
        return aux;
    }
}
