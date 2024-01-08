using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaVisualizer : MonoBehaviour
{
   public float atacOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    public float sphereSize; // Tamaño del área de detección

    private void OnDrawGizmos()
    {
        DrawAttackArea();
    }

    private void DrawAttackArea()
    {
        Gizmos.color = Color.red;
        Vector3 atacPosition = transform.position + transform.forward * atacOffset;
        Gizmos.DrawWireSphere(atacPosition, sphereSize / 2);
    }
}
