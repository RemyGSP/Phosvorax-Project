using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyDecisions/DeadDecision")]
public class EnemyIsDeadDecision : Decision
{
    public override bool Decide(StateMachine stateMachine)
    {
        bool isDead = stateMachine.GetComponent<HealthBehaviour>().CheckIdDead();
        bool aux = false;

        if (isDead)
        {
            aux = true;
        }
        return aux;
    }
}

