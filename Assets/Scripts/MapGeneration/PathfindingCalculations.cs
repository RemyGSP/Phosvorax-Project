using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PathfindingCalculations : MonoBehaviour
{
      private static List<Vector2Int> usedRooms = new List<Vector2Int>();

    public static List<Vector2Int> FindFarthestRooms(int[,] matrix, int additionalRoomCount)
    {
        Vector2Int center = CalculateCenter(matrix);

        // Encuentra la habitación más alejada del centro
        Vector2Int firstFarthestRoom = FindFarthestRoom(matrix, center);

        // Encuentra la habitación más alejada de la habitación más alejada del centro
        Vector2Int secondFarthestRoom = FindFarthestRoom(matrix, center, firstFarthestRoom);

        // Encuentra habitaciones adicionales que cumplen los criterios
        List<Vector2Int> additionalRooms = FindAdditionalRooms(matrix, additionalRoomCount, firstFarthestRoom, secondFarthestRoom);

        // Retorna las habitaciones encontradas
        List<Vector2Int> allRooms = new List<Vector2Int> { firstFarthestRoom, secondFarthestRoom };
        allRooms.AddRange(additionalRooms);

        // Limpiar la lista de habitaciones usadas después de cada llamada principal
        usedRooms.Clear();

        return allRooms;
    }

    private static List<Vector2Int> FindAdditionalRooms(int[,] matrix, int count, params Vector2Int[] excludeRooms)
{
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);

    List<Vector2Int> validRooms = new List<Vector2Int>();

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Vector2Int room = new Vector2Int(i, j);
            int roomValue = matrix[i, j];

            if ((roomValue == 1 || roomValue == 2 || roomValue == 4 || roomValue == 8) &&
                !excludeRooms.ToList().Contains(room) && !usedRooms.Contains(room))
            {
                validRooms.Add(room);
            }
        }
    }

    // Mezclar la lista de habitaciones válidas
    ShuffleList(validRooms);

    List<Vector2Int> additionalRooms = new List<Vector2Int>();

    for (int i = 0; i < Mathf.Min(count, validRooms.Count); i++)
    {
        // Seleccionar habitaciones aleatorias entre las válidas
        Vector2Int selectedRoom = validRooms[i];
        additionalRooms.Add(selectedRoom);

        // Añadir la habitación seleccionada a la lista de habitaciones usadas
        usedRooms.Add(selectedRoom);
    }

    return additionalRooms;
}

// Método para mezclar una lista utilizando el algoritmo de Fisher-Yates
private static void ShuffleList<T>(List<T> list)
{
    int n = list.Count;
    System.Random rng = new System.Random();

    while (n > 1)
    {
        n--;
        int k = rng.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
    }
}

    private static Vector2Int CalculateCenter(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        return new Vector2Int(rows / 2, cols / 2);
    }

    private static Vector2Int FindFarthestRoom(int[,] matrix, params Vector2Int[] targetRooms)
    {
        int maxDistance = 0;
        Vector2Int farthestRoom = Vector2Int.zero;

        Dictionary<Vector2Int, int> distanceMap = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        foreach (var targetRoom in targetRooms)
        {
            queue.Enqueue(targetRoom);
            distanceMap[targetRoom] = 0;
        }

        while (queue.Count > 0)
        {
            Vector2Int currentRoom = queue.Dequeue();
            int currentDistance = distanceMap[currentRoom];

            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                farthestRoom = currentRoom;
            }

            foreach (var neighbor in GetAdjacentRooms(currentRoom, matrix))
            {
                if (!distanceMap.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    distanceMap[neighbor] = currentDistance + 1;
                }
            }
        }

        return farthestRoom;
    }

    private static List<Vector2Int> GetAdjacentRooms(Vector2Int room, int[,] matrix)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        foreach (var direction in directions)
        {
            Vector2Int neighbor = room + direction;

            if (IsValidRoom(neighbor, matrix) && HasDoor(room, neighbor, matrix))
            {
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    private static bool HasDoor(Vector2Int room1, Vector2Int room2, int[,] matrix)
    {
        return matrix[room1.x, room1.y] != 0 && matrix[room2.x, room2.y] != 0;
    }

    private static bool IsValidRoom(Vector2Int room, int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        return room.x >= 0 && room.x < rows && room.y >= 0 && room.y < cols;
    }

}
