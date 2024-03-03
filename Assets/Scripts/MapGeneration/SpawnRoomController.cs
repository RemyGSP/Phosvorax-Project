using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomController : MonoBehaviour
{
     // Start is called before the first frame update
    void Start()
    {
        // Encuentra el objeto con el nombre "jugador"
        GameObject jugador = GameObject.Find("MainCharacter");

        // Verifica si se encontró el jugador
        if (jugador != null)
        {
            // Establece la posición del objeto actual (SpawnRoomController) a la posición del jugador
            
            jugador.transform.position = transform.position;
        }
        else
        {
            Debug.LogError("No se encontró el objeto con el nombre 'jugador'. Asegúrate de que el objeto existe y tiene el nombre correcto.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Puedes agregar lógica adicional de actualización si es necesario
    }
}
