using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/BichoPinchoFromEntryToIdle")]

public class BichoPinchoFromEntryToIdle : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool canStart = stateMachine.GetComponent<EnemyReferences>().GetCanBeStarted();
        if (canStart)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
