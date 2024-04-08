using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "EnemyDecisions/BichoPinchoCrash")]

public class BichoPinchoCrashDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        if (stateMachine.gameObject.GetComponent<BichoPinchoReferences>().IsCrashing())
        {
            stateMachine.gameObject.GetComponent<BichoPinchoReferences>().StopCrashing();
            return true;
        }
        else
        {
            return false;
        }
    }
}
