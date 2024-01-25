using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingPlatform
{
    [SerializeField] private GameObject platformObject;
    [SerializeField] private Vector3[] positionPoints;
    [SerializeField] private float moveTime;
    [SerializeField] private bool isMoving = false;

    private int platformPosition;


    public void InitializeStartingPosition()
    {
        // Calcular la posici�n inicial bas�ndonos en el objeto de la plataforma
        positionPoints[0] = platformObject.transform.localPosition;
    }

    private void NextPlatformPosition()
    {
        
        if ((platformPosition+1)< positionPoints.Length)
        {
            platformPosition++;
        }
        else
        {
            platformPosition = 0;
        }
    }

    public void PlatfomrMotionStarter( Vector3 newDirection)
    {
        if(newDirection == Vector3.zero)
        {
            CalculateNextdirection();
        }
        else
        {
            if (newDirection != platformObject.transform.position)
            {
                PlatformsController.Instance.StartCoroutine(MovePlatform(newDirection));
            }
        }
        
        
    }

    public void CalculateNextdirection()
    {
        NextPlatformPosition();
        Vector3 destination = positionPoints[platformPosition];
        PlatformsController.Instance.StartCoroutine(MovePlatform(destination));
    }

    IEnumerator MovePlatform(Vector3 destination)
    {
        isMoving = true;

        float elapsedTime = 0f;
        Vector3 startingPos = platformObject.transform.localPosition;

        while (elapsedTime < moveTime)
        {
            platformObject.transform.localPosition = Vector3.Lerp(startingPos, destination, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la plataforma est� exactamente en la posici�n de destino al final
        platformObject.transform.localPosition = destination;

        isMoving = false;
    }
    public bool ObtenerEstadoBooleano()
    {
        return isMoving;
    }
}
