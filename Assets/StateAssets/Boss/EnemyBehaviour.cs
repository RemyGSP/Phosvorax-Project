using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemyMeleeAttackState meleeAttackState; // Asigna esta referencia desde el editor

    public float attackOffset;
    public float sphereSize;

    public void SetAttackParams(float offset, float size)
    {
        attackOffset = offset;
        sphereSize = size;
    }

    private void OnDrawGizmosSelected()
    {
        if (meleeAttackState == null)
            return;

        // Dibujar una esfera en el área de ataque del enemigo
        Vector3 attackPosition = transform.position + transform.forward * attackOffset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition, sphereSize / 2);
    }
}
