using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DummyStates/HitDecision")]
public class OnHitDecisions : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (stateMachine.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour health))
        {
            if (health.HasBeenHit())
            {
                aux = true;
            }
        }
        return aux;
    }
}
