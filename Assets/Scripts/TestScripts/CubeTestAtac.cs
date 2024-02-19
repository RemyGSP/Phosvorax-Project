using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTestAtac : MonoBehaviour
{
    [SerializeField] private float attackDamage; // Daño que realizará al jugador

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión es con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Intenta obtener el componente HealthBehaviour del jugador
            if (collision.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour playerHealth))
            {
                // Aplica daño al jugador
                playerHealth.Damage(attackDamage);
                Debug.Log("Cubo atacó al jugador. Daño: " + attackDamage);
            }
        }
    }
}
