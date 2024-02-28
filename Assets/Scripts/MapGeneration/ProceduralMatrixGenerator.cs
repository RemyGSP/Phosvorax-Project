using UnityEngine;
using System.Collections.Generic;
using System;
public class ProceduralMatrixGenerator : MonoBehaviour
{
    public Vector2Int MapSize;
    private int[,] binariMatrix;
    private int[,] roomTypeMatrix;
    private int[,] roomHeatmapMatrix;
    public int RoomQuantity;
    private int roomsPlaced;
    private int deadEndsCount;
    public int minDeadEnds;

    private PathfindingCalculations pathfindingCalculations;
    private int maxDeadEndsIteration;
    private int DeadEndsIteration;



    void Start()
    {

        
        pathfindingCalculations = GetComponent<PathfindingCalculations>();
        StartRoomGeneration();
    }

    void StartRoomGeneration()
    {
        binariMatrix = new int[(int)MapSize.x, (int)MapSize.y];
        roomsPlaced = 0;
        deadEndsCount = 0;
        maxDeadEndsIteration = 1000;
        PrepareMatrixBeforeFilling();   
    }

    void PrepareMatrixBeforeFilling(){
        
        int casillaCentralX = MapSize.x / 2;
        int casillaCentralY = MapSize.y / 2;

        binariMatrix[casillaCentralX, casillaCentralY] = 1;

        roomsPlaced++;
        DeadEndsIteration++;
        FillOutTheMatrix();
    }

    void FillOutTheMatrix()
    {
       
        int maxIterations = 10000; // Ajusta según sea necesario
        int iterationCount = 0;

        while (roomsPlaced < RoomQuantity && iterationCount < maxIterations)
        {
            for (int i = 0; i < MapSize.x; i++)
            {
                for (int j = 0; j < MapSize.y; j++)
                {
                    if (roomsPlaced < RoomQuantity && binariMatrix[i, j] == 0)
                    {
                        int adjacentOnes = CountAdjacentOnes(i, j);

                        // Agrega las condiciones adicionales que deseas
                        if (adjacentOnes == 1)
                        {
                            float randomProbability = UnityEngine.Random.value;
                            float probabilityThreshold = 0.5f;

                            if (randomProbability <= probabilityThreshold)
                            {
                                binariMatrix[i, j] = 1;
                                roomsPlaced++;
                            }
                        }
                    }
                }
            }

            iterationCount++;
        }

        if (iterationCount >= maxIterations)
        {
            Debug.LogError("Generación de mapa fallida. Se alcanzó el límite de iteraciones.");
            // Puedes manejar la situación de generación fallida aquí.
        }
           MatrixRoomTypeReEnumeration(); 
           
    }

    void MatrixRoomTypeReEnumeration(){

        roomTypeMatrix = new int[MapSize.x, MapSize.y];

        for (int i = 0; i < MapSize.x; i++)
        {
            for (int j = 0; j < MapSize.y; j++)
            {
                int adjacentOnes = CountAdjacentOnes(i, j);

                // Modifica el valor en base a la cantidad de vecinos con valor 1
                if (binariMatrix[i, j] == 1)
                {
                    if (adjacentOnes == 4)
                        roomTypeMatrix[i, j] = 15;
                    else if (adjacentOnes == 3)
                        roomTypeMatrix[i, j] = GetAdjacentConfiguration(i, j);
                    else if (adjacentOnes == 2)
                        roomTypeMatrix[i, j] = GetAdjacentConfiguration(i, j);
                    else{
                        roomTypeMatrix[i, j] = GetAdjacentConfiguration(i, j);
                        deadEndsCount++;
                    }
                        
                }
                else
                {
                    roomTypeMatrix[i, j] = 0;
                }
            }
        }
        //!!!!como se ponga mal el tamaño de la aray o el de las habitaciones esto hace un bucle infinito!!!!!
        if (deadEndsCount >= minDeadEnds)
        {
            
            pathfindingCalculations.ReceiveMatrix2(roomTypeMatrix);
        }
        else if (DeadEndsIteration < maxDeadEndsIteration)
        {
            StartRoomGeneration();
            Debug.Log("a generar de nuevo");
        }
        else
        {
            Debug.Log("liada maxima datos de mapa mal configurados");
        }
    }

    int GetAdjacentConfiguration(int x, int y)
    {
        // Devuelve un valor dependiendo de la configuración de habitaciones adyacentes
        int configuration = 0;

        if (x > 0 && binariMatrix[x - 1, y] == 1) configuration += 2;
        if (x < MapSize.x - 1 && binariMatrix[x + 1, y] == 1) configuration += 1;
        if (y > 0 && binariMatrix[x, y - 1] == 1) configuration += 4;
        if (y < MapSize.y - 1 && binariMatrix[x, y + 1] == 1) configuration += 8;

        return configuration;
    }

    int CountAdjacentOnes(int x, int y)
    {
        int count = 0;

        // Verifica casillas a la izquierda y derecha
        if (x > 0 && binariMatrix[x - 1, y] == 1) count++;
        if (x < MapSize.x - 1 && binariMatrix[x + 1, y] == 1) count++;

        // Verifica casillas arriba y abajo
        if (y > 0 && binariMatrix[x, y - 1] == 1) count++;
        if (y < MapSize.y - 1 && binariMatrix[x, y + 1] == 1) count++;

        return count;
    }

   

   

}
