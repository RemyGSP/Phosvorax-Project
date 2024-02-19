using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTpController : MonoBehaviour
{
   public GameObject destinationObject; // Asigna el objeto de destino desde el Inspector.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (destinationObject != null)
            {
                other.transform.position = destinationObject.transform.position;
            }
            else
            {
                Debug.LogWarning("El objeto de destino no está asignado en el Inspector.");
            }
        }
    }

}
