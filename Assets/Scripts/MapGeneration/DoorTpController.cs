using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTpController : MonoBehaviour
{
    private GameObject destinationObject; // Asigna el objeto de destino desde el Inspector.

    void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (destinationObject != null)
            {
                other.transform.position = destinationObject.transform.position;

                // Activa el objeto al teleportar al jugador.
                gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("El objeto de destino no está asignado en el Inspector.");
            }
        }
    }

    public void SetDestination(GameObject newDestination)
    {
        destinationObject = newDestination;

        Debug.Log("funcion puerta");
        gameObject.SetActive(true);
    }
}

