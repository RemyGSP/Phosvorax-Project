using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/AvellanoIsPatrolling")]

public class AvellanoIsPatrollingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool canStart = stateMachine.GetComponent<EnemyReferences>().GetCanBeStarted();
        bool aux = false;

        if (canStart)
        {
            aux = true;
        }
        return aux;
    }
}
