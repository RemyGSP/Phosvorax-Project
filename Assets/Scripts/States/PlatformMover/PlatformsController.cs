using UnityEngine;
using System.Collections;
using System.Linq;


[System.Serializable]
public class ElevatorPlatform
{
    [SerializeField] private GameObject platformObject;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float moveTime;

    [SerializeField] private bool isMoving = false;
    

    public void InitializeStartingPosition()
    {
        // Calcular la posición inicial basándonos en el objeto de la plataforma
        startingPosition = platformObject.transform.position;
    }

    public void MoveToDestination()
    {
        if (!isMoving)
        {
            // Calcular la posición de destino sumando la dirección a la posición actual
            Vector3 destination = startingPosition + direction;

            // Iniciar la corrutina para mover la plataforma
            PlatformsController.Instance.StartCoroutine(MovePlatform(destination));
        }
    }

    public void MoveBackToOriginalPosition()
    {
        if (!isMoving)
        {
            // Calcular la posición de destino restando la dirección a la posición actual
            Vector3 destination = startingPosition;

            // Iniciar la corrutina para mover la plataforma
            PlatformsController.Instance.StartCoroutine(MovePlatform(destination));
        }
    }

    IEnumerator MovePlatform(Vector3 destination)
    {
        isMoving = true;

        float elapsedTime = 0f;
        Vector3 startingPos = platformObject.transform.position;

        while (elapsedTime < moveTime)
        {
            platformObject.transform.position = Vector3.Lerp(startingPos, destination, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la plataforma esté exactamente en la posición de destino al final
        platformObject.transform.position = destination;

        isMoving = false;
    }
    public bool ObtenerEstadoBooleano()
    {
        return isMoving;
    }
}

public class PlatformsController : MonoBehaviour
{
    [SerializeField] private ElevatorPlatform[] platformList;
    [SerializeField] private GameObject[] Switches;
    private static PlatformsController instance;
    private int platformdirection;
    private bool todasLasPlataformasEnMovimiento;

    public static PlatformsController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlatformsController>();
            }
            return instance;
        }
    }

    void Start()
    {
        platformdirection = 0;
        foreach (ElevatorPlatform platform in platformList)
        {
            platform.InitializeStartingPosition();
        }
    }

    void FixedUpdate()
    {
        TodasLasPlataformasEstanMoviendo();
        if (!todasLasPlataformasEnMovimiento)
        {
            SetSwitchColor(1);
        }
        else
        {
           SetSwitchColor(0);
        }
    }
    private void TodasLasPlataformasEstanMoviendo()
    {
        // Verifica si todas las plataformas están en movimiento
        todasLasPlataformasEnMovimiento = platformList.Any(plataforma => plataforma.ObtenerEstadoBooleano());
    }
    public void SetSwitchColor(int colorType)
    {
        foreach (GameObject switchObject in Switches)
        {
            // Asegúrate de que el objeto tenga un componente Renderer para cambiar su color
            Renderer switchRenderer = switchObject.GetComponent<Renderer>();

            if (switchRenderer != null)
            {
                // Selecciona el color según el valor del parámetro
                Color newColor = Color.white; // Color por defecto, blanco

                if (colorType == 0)
                {
                    // Rojo pastel
                    newColor = new Color(1.0f, 0.7f, 0.7f);
                }
                else if (colorType == 1)
                {
                    // Gris oscuro
                    newColor = new Color(0.2f, 0.2f, 0.2f);
                }

                // Cambia el color del material del Renderer al color seleccionado
                switchRenderer.material.color = newColor;
            }
            else
            {
                Debug.LogWarning("El objeto " + switchObject.name + " no tiene un componente Renderer.");
            }
        }
    }

    public void StartMovingPlatform()
    {
        //TodasLasPlataformasEstanMoviendo();
        if (!todasLasPlataformasEnMovimiento)
        {
           // SetSwitchColor(0);
            if (platformdirection == 0)
            {
                foreach (ElevatorPlatform platform in platformList)
                {
                    platform.MoveToDestination();
                }
                platformdirection = 1;
            }
            else
            {
                foreach (ElevatorPlatform platform in platformList)
                {
                    platform.MoveBackToOriginalPosition();
                }
                platformdirection = 0;
            }
        }
        else
        {
            
        }
      
       
    }
}
