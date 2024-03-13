using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Decision/BichoPinchoAttack")]
public class BichoPinchoAttackDecision : Decision
{
    [SerializeField] private float detectionRange = 5f;
    public override bool Decide(StateMachine stateMachine)
    {
        Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();
        playerPos.y = PlayerReferences.instance.GetPlayerVisiblePoint().y;
        Vector3 directionToPlayer = playerPos - stateMachine.gameObject.transform.position;

        BichoPinchoReferences infoContainer = stateMachine.gameObject.GetComponent<BichoPinchoReferences>();
        // Check if the player is within detection range and the enemy is not stunned
        if (Vector3.Distance(stateMachine.gameObject.transform.position, playerPos) <= detectionRange && !stateMachine.gameObject.GetComponent<BichoPinchoReferences>().CheckIfStunned() && infoContainer.CheckAttackTimer())
        {
            RaycastHit hit;
            // Cast a ray from the enemy towards the player
            if (Physics.Raycast(stateMachine.gameObject.transform.position, directionToPlayer, out hit, detectionRange))
            {
                // Check if the ray hits the player or not obstructed by other objects
                if (hit.collider.CompareTag("Player"))
                {
                    // Enemy has clear line of sight to the player
                    return true;
                }
            }
        }

        // Player is not within detection range or enemy's line of sight is obstructed
        return false;
    }


}
