using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerDecisions/OnDamageDecision")]
public class OnDamageDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        return stateMachine.gameObject.GetComponent<HealthBehaviour>().CheckIfDeath();
    }
}
