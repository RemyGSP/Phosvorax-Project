using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyDecisions/BichoPinchoDeath")]
public class BichoPinchoDeathDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        return stateMachine.gameObject.GetComponent<HealthBehaviour>().CheckIfDeath();
    }


}
