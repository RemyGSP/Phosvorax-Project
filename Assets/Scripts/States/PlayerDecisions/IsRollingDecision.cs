using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/RollingDecision")]
public class IsRollingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsRolling() && PlayerTimers.timer.rollTimer > PlayerTimers.timer.rollCD)
        {
            aux = true;
        }
        return aux;
    }
}
