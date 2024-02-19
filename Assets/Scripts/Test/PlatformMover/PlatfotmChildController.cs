using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChildController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto que colision� tiene un componente Rigidbody
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        // Si tiene un Rigidbody, hacerlo hijo de la plataforma
        if (rb != null)
        {
            rb.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verificar si el objeto que sali� de la colisi�n tiene un componente Rigidbody
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

        // Si tiene un Rigidbody, quitarlo como hijo de la plataforma
        if (rb != null && rb.transform.parent == transform)
        {
            rb.transform.parent = null;
        }
    }
}
