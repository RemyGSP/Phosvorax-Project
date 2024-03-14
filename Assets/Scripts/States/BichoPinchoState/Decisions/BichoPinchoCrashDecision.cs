using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(menuName = "Decision/BichoPinchoCrash")]

public class BichoPinchoCrashDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        RaycastHit hit;
        return stateMachine.gameObject.GetComponent<BichoPinchoReferences>().IsCrashing();
    }
}
