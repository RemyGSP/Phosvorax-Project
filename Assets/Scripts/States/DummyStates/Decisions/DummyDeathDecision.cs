using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DummyStates/DeathDecision")]
public class DummyDeathDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        return stateMachine.gameObject.GetComponent<HealthBehaviour>().CheckIfDeath();

    }
}
