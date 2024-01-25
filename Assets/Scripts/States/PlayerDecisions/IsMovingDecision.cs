using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerDecisions/sMovingDecision")]
public class IsMovingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (PlayerInputController.Instance.GetPlayerInputDirection() != Vector3.zero)
        {
            aux = true;
        }
        return aux;
    }
}
