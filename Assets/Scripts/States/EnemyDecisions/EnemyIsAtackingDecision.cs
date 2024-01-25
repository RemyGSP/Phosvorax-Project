using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/AttackingDecision")]
public class EnemyIsAtackingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 enemiePos = stateMachine.GetComponent<EnemyReferences>().GetEnemyCoordinates();
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), enemiePos);
        float maxAttackDistance = stateMachine.GetComponent<EnemyReferences>().GetMaxAttackPosition();
        bool aux = false;

        if (distance < maxAttackDistance)
        {
            aux = true;
        }
        return aux;
    }
}
