using UnityEngine;

public class SaveLoadTester : MonoBehaviour
{
    private void Update()
    {
        // Guardar datos al presionar la tecla 'S'
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Buscar el objeto llamado "player"
            GameObject playerObject = GameObject.Find("Player");

            if (playerObject != null)
            {
                PlayerData newPlayerData = new PlayerData
                {
                    playerPosition = playerObject.transform.position,
                    playerLives = 3
                };

                JsonManager.SaveData(newPlayerData); // Cambiado el nombre del administrador
                Debug.Log("Datos guardados");
                Debug.Log("Datos guardados en: " + JsonManager.GetSavePath());
            }
            else
            {
                Debug.Log("No se encontró el objeto 'player'");
            }
        }

        // Cargar datos al presionar la tecla 'L'
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData loadedPlayerData = JsonManager.LoadData<PlayerData>(); // Cambiado el nombre del administrador

            if (loadedPlayerData != null)
            {
                // Buscar el objeto llamado "player"
                GameObject playerObject = GameObject.Find("Player");

                if (playerObject != null)
                {
                    // Mover el objeto "player" a la posición cargada
                    playerObject.transform.position = loadedPlayerData.playerPosition;

                    // Hacer algo con los datos cargados
                    Debug.Log("Player position: " + loadedPlayerData.playerPosition);
                    //Debug.Log("Player lives: " + loadedPlayerData.playerLives);
                }
                else
                {
                    Debug.Log("No se encontró el objeto 'player' para cargar la posición");
                }
            }
            else
            {
                Debug.Log("No se encontraron datos para cargar");
            }
        }
    }
}

