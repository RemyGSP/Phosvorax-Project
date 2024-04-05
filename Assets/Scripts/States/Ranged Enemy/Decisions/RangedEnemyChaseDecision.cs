using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/RangedEnemyChasingDecision")]

public class RangedEnemyChaseDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool stopGetAway = stateMachine.GetComponent<RangedEnemyReferences>().GetCanAttack();
        bool canStart = stateMachine.GetComponent<EnemyReferences>().GetCanBeStarted();
        bool aux = false;
        if (stopGetAway && canStart)
        {
            aux = true;
        }
        return aux;
    }
}
