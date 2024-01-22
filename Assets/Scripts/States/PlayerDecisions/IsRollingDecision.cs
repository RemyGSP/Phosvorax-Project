using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/RollingDecision")]
public class IsRollingDecision : Decision
{
    public override bool Decide()
    {
        bool aux = false;
        if (PlayerInputController.Instance.IsRolling())
        {
            aux = true;
        }
        return aux;
    }
}
