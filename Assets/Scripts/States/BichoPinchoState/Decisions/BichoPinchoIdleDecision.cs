using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Decisions/BichoPinchoIdle")]
public class BichoPinchoIdleDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        return true;

    }
}
