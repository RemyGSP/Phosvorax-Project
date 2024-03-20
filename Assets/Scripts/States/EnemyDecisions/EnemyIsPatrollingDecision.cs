using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/PatrollingDecision")]
public class EnemyIsPatrollingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool canPatrol = false;

        bool aux = false;
        if (canPatrol)
        {
            aux = true;
        }
        return aux;
    }
}

