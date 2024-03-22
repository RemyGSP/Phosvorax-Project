using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/RangedEnemyGetAwayDecision")]
public class GetAwayDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool canGetAway = stateMachine.GetComponent<RangedEnemyReferences>().GetCanMoveAway();
        bool aux = false;

        if (canGetAway)
        {
            aux = true;
        }
        return aux;
    }
}
