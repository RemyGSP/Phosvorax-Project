using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerDecisions/FallingDecision")]

public class IsFallingDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (!PlayerReferences.instance.CheckIfGrounded())
        {
            aux = true;
        }
        return aux;
    }
}
