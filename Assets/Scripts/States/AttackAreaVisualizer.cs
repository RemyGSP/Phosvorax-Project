using UnityEngine;

public class AttackAreaVisualizer : MonoBehaviour
{
    [SerializeField] private float attackOffset; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float heightOffset; // Desplazamiento en la altura desde el suelo
    [SerializeField] private float capsuleRadius; // Radio de la cápsula
    [SerializeField] private float capsuleHeight; // Altura de la cápsula

    // Método para establecer los parámetros del gizmo
    public void SetParameters(float attackOffset, float heightOffset, float capsuleRadius, float capsuleHeight)
    {
        this.attackOffset = attackOffset;
        this.heightOffset = heightOffset;
        this.capsuleRadius = capsuleRadius;
        this.capsuleHeight = capsuleHeight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        // Obtener la dirección hacia adelante del jugador
        Vector3 playerForward = transform.forward;

        // Calcular la posición del centro del área de ataque
        Vector3 attackPosition = transform.position + playerForward * attackOffset + Vector3.up * heightOffset;

        // Dibujar la cápsula de ataque usando los parámetros definidos
        DrawWireCapsule(attackPosition, capsuleHeight, capsuleRadius);
    }

    // Función para dibujar una cápsula con cable en el Editor de Unity
    private void DrawWireCapsule(Vector3 position, float height, float radius)
    {
        float halfHeight = height * 0.5f;
        Vector3 up = Vector3.up * halfHeight;
        Vector3 down = Vector3.down * halfHeight;

        // Draw the top half
        Gizmos.DrawWireSphere(position + up, radius);
        Gizmos.DrawLine(position + up + Vector3.forward * radius, position + up + Vector3.back * radius);
        Gizmos.DrawLine(position + up + Vector3.left * radius, position + up + Vector3.right * radius);

        // Draw the bottom half
        Gizmos.DrawWireSphere(position + down, radius);
        Gizmos.DrawLine(position + down + Vector3.forward * radius, position + down + Vector3.back * radius);
        Gizmos.DrawLine(position + down + Vector3.left * radius, position + down + Vector3.right * radius);

        // Draw the connecting lines
        Gizmos.DrawLine(position + up + Vector3.forward * radius, position + down + Vector3.forward * radius);
        Gizmos.DrawLine(position + up + Vector3.back * radius, position + down + Vector3.back * radius);
        Gizmos.DrawLine(position + up + Vector3.left * radius, position + down + Vector3.left * radius);
        Gizmos.DrawLine(position + up + Vector3.right * radius, position + down + Vector3.right * radius);
    }
}






