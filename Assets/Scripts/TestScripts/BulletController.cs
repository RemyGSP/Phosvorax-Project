using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float distancia = 10f; // Distancia que quieres que recorra la bala
    public float tiempo = 2f; // Tiempo en el que quieres que la bala recorra la distancia

    private float velocidad; // Velocidad de la bala

    private void Start()
    {
        // Calcular la velocidad necesaria usando la f�rmula de MRU: velocidad = distancia / tiempo
        velocidad = distancia / tiempo;

        // Obtener la direcci�n hacia adelante del objeto
        Vector3 direccion = transform.forward;

        // Aplicar la fuerza en la direcci�n hacia adelante
        GetComponent<Rigidbody>().velocity = direccion * velocidad;

        // Iniciar la destrucci�n despu�s del tiempo especificado
        Destroy(gameObject, tiempo);
    }
}
