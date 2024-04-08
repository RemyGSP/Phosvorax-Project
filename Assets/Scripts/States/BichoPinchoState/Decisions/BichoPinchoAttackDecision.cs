using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyDecisions/BichoPinchoAttack")]
public class BichoPinchoAttackDecision : Decision
{
    [SerializeField]LayerMask playerLayer;
    [SerializeField] private float detectionRange = 5f;
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();
        playerPos.y = PlayerReferences.instance.GetPlayerVisiblePoint().y;
        Vector3 directionToPlayer = playerPos - stateMachine.gameObject.GetComponent<RotateFromPosition>().rotationPoint.position;

        BichoPinchoReferences infoContainer = stateMachine.gameObject.GetComponent<BichoPinchoReferences>();
        if (Vector3.Distance(stateMachine.gameObject.transform.position, playerPos) <= detectionRange && !stateMachine.gameObject.GetComponent<BichoPinchoReferences>().CheckIfStunned() && infoContainer.CheckAttackTimer())
        {
            Debug.DrawRay(stateMachine.gameObject.GetComponent<RotateFromPosition>().rotationPoint.position, directionToPlayer, Color.red, detectionRange);
            RaycastHit hit;
            if (Physics.Raycast(stateMachine.gameObject.GetComponent<RotateFromPosition>().rotationPoint.position, directionToPlayer, out hit, detectionRange,playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }


}
