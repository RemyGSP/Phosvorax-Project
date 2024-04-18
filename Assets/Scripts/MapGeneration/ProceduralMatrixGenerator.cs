using UnityEngine;
using System.Collections.Generic;
using System;
public class ProceduralMatrixGenerator : MonoBehaviour
{

    public Vector2Int MapSize;
    public int RoomQuantity;
    public int minDeadEnds;
    public int mapSeed;

    private int maxDeadEndsIteration;
    private int DeadEndsIteration;
    private int roomsPlaced;
    private int deadEndsCount;

    private int[,] binariMatrix;
    private int[,] roomLayoutTypeMatrix;
    private int[,] roomTypeMatrix;

    

    private PathfindingCalculations pathfindingCalculations;
    private PrefabRoomInstancier prefabRoomInstancier;


    void Start()
    {
        prefabRoomInstancier = GetComponent<PrefabRoomInstancier>();
        pathfindingCalculations = GetComponent<PathfindingCalculations>();
        StartRoomGeneration();
    }

    void StartRoomGeneration()
    {
        UnityEngine.Random.InitState(mapSeed);
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
           MatrixRoomLayoutTypeReEnumeration(); 
           
    }

    void MatrixRoomLayoutTypeReEnumeration(){

        roomLayoutTypeMatrix = new int[MapSize.x, MapSize.y];

        for (int i = 0; i < MapSize.x; i++)
        {
            for (int j = 0; j < MapSize.y; j++)
            {
                int adjacentOnes = CountAdjacentOnes(i, j);

                // Modifica el valor en base a la cantidad de vecinos con valor 1
                if (binariMatrix[i, j] == 1)
                {
                    if (adjacentOnes == 4)
                        roomLayoutTypeMatrix[i, j] = 15;
                    else if (adjacentOnes == 3)
                        roomLayoutTypeMatrix[i, j] = GetAdjacentConfiguration(i, j);
                    else if (adjacentOnes == 2)
                        roomLayoutTypeMatrix[i, j] = GetAdjacentConfiguration(i, j);
                    else{
                        roomLayoutTypeMatrix[i, j] = GetAdjacentConfiguration(i, j);
                        deadEndsCount++;
                    }
                        
                }
                else
                {
                    roomLayoutTypeMatrix[i, j] = 0;
                }
            }
        }
        if (deadEndsCount >= minDeadEnds)
        {
 
            DecideRoomType();
            
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

    void DecideRoomType()
    {
        List<Vector2Int> farthestRooms = PathfindingCalculations.FindFarthestRooms(roomLayoutTypeMatrix, 1);

        roomTypeMatrix = new int[MapSize.x, MapSize.y];

        // Copia roomLayoutTypeMatrix en roomTypeMatrix
        int rows = MapSize.x;
        int cols = MapSize.y;

        for (int i = 0; i < rows; i++)
        {
            Array.Copy(roomLayoutTypeMatrix, i * cols, roomTypeMatrix, i * cols, cols);
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (roomTypeMatrix[i, j] != 0)
                {
                    double randomValue = UnityEngine.Random.value;
                    roomTypeMatrix[i, j] = randomValue < 0.9 ? 4 : 5;
                }
            }
        }

        int[] customValues = { 2, 1, 3,};

        for (int i = 0; i < farthestRooms.Count; i++)
        {
            Vector2Int position = farthestRooms[i];
            int row = position.x;
            int col = position.y;

            // Asegurarse de que la posición esté dentro de los límites de la matriz
            if (row >= 0 && row < roomTypeMatrix.GetLength(0) && col >= 0 && col < roomTypeMatrix.GetLength(1))
            {
                // Asignar el valor correspondiente desde el arreglo customValues
                roomTypeMatrix[row, col] = customValues[i];
            }
        }

        SendMatrix();
    }


    void SendMatrix()
    {
        prefabRoomInstancier.ReceiveMatrix(roomLayoutTypeMatrix, roomTypeMatrix);
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
