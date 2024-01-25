using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/ChasingDecision")]

public class EnemyIsChasingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 enemiePos = stateMachine.GetComponent<EnemyReferences>().GetEnemyCoordinates();
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), enemiePos);

        float distanceToSeePlayer = stateMachine.GetComponent<EnemyReferences>().GetDistanceToSeePlayer();
        bool aux = false;

        if (distance < distanceToSeePlayer)
        {
            aux = true;
        }
        return aux;
    }
}
