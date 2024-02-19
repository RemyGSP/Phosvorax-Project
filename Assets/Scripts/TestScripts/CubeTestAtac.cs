using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTestAtac : MonoBehaviour
{
    [SerializeField] private float attackDamage; // Da�o que realizar� al jugador

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n es con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Intenta obtener el componente HealthBehaviour del jugador
            if (collision.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour playerHealth))
            {
                // Aplica da�o al jugador
                playerHealth.Damage(attackDamage);
                Debug.Log("Cubo atac� al jugador. Da�o: " + attackDamage);
            }
        }
    }
}
