using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaVisualizer : MonoBehaviour
{

    public void DrawAttackArea(float attackOffset, float sphereSize)
    {
        Gizmos.color = Color.red;
        Vector3 atacPosition = transform.position + transform.forward * attackOffset;
        Gizmos.DrawWireSphere(atacPosition, sphereSize / 2);
    }

    public void DrawAttackArea(Vector3 attackDirection, float sphereSize)
    {
        Gizmos.color = Color.red;
        Vector3 attackPostion = attackDirection;
        Gizmos.DrawWireSphere(attackPostion, sphereSize / 2);
    }
}
