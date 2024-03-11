using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Decision/BichoPinchoAttack")]
public class AttackDecision : Decision
{
    [SerializeField] private float detectionRange = 5f; 
    public override bool Decide(StateMachine stateMachine)
    {
        bool aux = false;
        if (Vector3.Distance(stateMachine.gameObject.transform.position, PlayerReferences.instance.GetPlayerCoordinates()) <= detectionRange)
        {
            Debug.Log("Working");
            aux = true;
        }
        return aux;
    }


}
