using UnityEngine;
using System.Collections.Generic;

public class ProceduralMatrixGenerator : MonoBehaviour
{
    public Vector2Int MapSize;
    public Vector2Int roomNumberThreshold;
    private int[,] binariMatrix;
    private int[,] roomTypeMatrix;
    private int[,] roomHeatmapMatrix;
    private int finalRoomNumber;
    private int roomsPlaced;
    private int deadEndsCount;
    public int minDeadEnds;
    private PrefabRoomInstancier prefabRoomInstancier;
  

    void Start()
    {

        prefabRoomInstancier = GetComponent<PrefabRoomInstancier>();
        StartRoomGeneration();
    }

    void StartRoomGeneration()
    {
        binariMatrix = new int[(int)MapSize.x, (int)MapSize.y];
        roomsPlaced = 0;
        deadEndsCount = 0;
            PrepareMatrixBeforeFilling();   
    }

    void PrepareMatrixBeforeFilling(){
        finalRoomNumber = Random.Range((int)roomNumberThreshold.x, (int)roomNumberThreshold.y + 1);
        Debug.Log(finalRoomNumber);
        
        int casillaCentralX = MapSize.x / 2;
        int casillaCentralY = MapSize.y / 2;

        binariMatrix[casillaCentralX, casillaCentralY] = 1;

        roomsPlaced++;

        FillOutTheMatrix();
    }

    void FillOutTheMatrix()
    {
       
        int maxIterations = 10000; // Ajusta según sea necesario
        int iterationCount = 0;

        while (roomsPlaced < finalRoomNumber && iterationCount < maxIterations)
        {
            for (int i = 0; i < MapSize.x; i++)
            {
                for (int j = 0; j < MapSize.y; j++)
                {
                    if (roomsPlaced < finalRoomNumber && binariMatrix[i, j] == 0)
                    {
                        int adjacentOnes = CountAdjacentOnes(i, j);

                        // Agrega las condiciones adicionales que deseas
                        if (adjacentOnes == 1)
                        {
                            float randomProbability = Random.value;
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
           GenerateRoomHeatmapMatrix();
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
            if (deadEndsCount >= minDeadEnds){
                prefabRoomInstancier.ReceiveMatrix(roomTypeMatrix);
            }
            else{
                Debug.LogError("no tiene suficientes salas de 1 puerta xd");
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

    void GenerateRoomHeatmapMatrix()
    {
        
    }


    void PrintMatrix(int[,] matrix)
    {
        string matrixString = "Matriz de calor:\n";

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrixString += matrix[i, j] + " ";
            }
            matrixString += "\n";
        }

        Debug.Log(matrixString);
    }

}
