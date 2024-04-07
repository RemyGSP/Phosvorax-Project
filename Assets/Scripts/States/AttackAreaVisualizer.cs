using UnityEngine;

public class AttackAreaVisualizer : MonoBehaviour
{
    [SerializeField] private float attackOffset; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float heightOffset; // Desplazamiento en la altura desde el suelo
    [SerializeField] private float sphereSize; // Tamaño del área de detección

    // Método para establecer los parámetros del gizmo
    public void SetParameters(float attackOffset, float heightOffset, float sphereSize)
    {
        this.attackOffset = attackOffset;
        this.heightOffset = heightOffset;
        this.sphereSize = sphereSize;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        // Obtener la dirección hacia adelante del jugador
        Vector3 playerForward = transform.forward;

        // Calcular la posición del centro del área de ataque
        Vector3 attackPosition = transform.position + playerForward * attackOffset + Vector3.up * heightOffset;

        // Dibujar la esfera de ataque usando los parámetros definidos
        Gizmos.DrawWireSphere(attackPosition, sphereSize / 2);
    }
}




