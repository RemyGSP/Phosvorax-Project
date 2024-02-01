using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/IdleDecision")]
public class EnemyIsIdleDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 enemyPos = stateMachine.GetComponent<EnemyReferences>().GetEnemyCoordinates();
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), enemyPos);

        float distanceToSeePlayer = stateMachine.GetComponent<EnemyReferences>().GetDistanceToSeePlayer();
        bool aux = false;

        if (distance > distanceToSeePlayer)
        {
            aux = true;
        }
        return aux;
    }
}
