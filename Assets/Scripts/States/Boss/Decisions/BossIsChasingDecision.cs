using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "BossDecisions/IsChasingDecision")]
public class BossIsChasingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 enemiePos = stateMachine.GetComponent<EnemyReferences>().GetEnemyCoordinates();
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), enemiePos);
        float distanceToSeePlayer = stateMachine.GetComponent<EnemyReferences>().GetDistanceToSeePlayer();
        float maxAttackDistance = stateMachine.GetComponent<EnemyReferences>().GetMaxAttackPosition();
        bool canStart = stateMachine.GetComponent<EnemyReferences>().GetCanBeStarted();
        bool aux = false;
        bool isUsingAbility = stateMachine.GetComponent<BossReferences>().GetIsUsingAbiliy();
        if (distance <= distanceToSeePlayer && distance > maxAttackDistance && canStart && !isUsingAbility)
        {
            aux = true;
        }
        return aux;
    }
}
