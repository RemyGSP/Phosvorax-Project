using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingCalculations : MonoBehaviour
{
    public void ReceiveMatrix2(int[,] matrix)
    {
        Vector2Int center = CalculateCenter(matrix);

        // Ejemplo de llamada con 1 habitación
        Vector2Int firstFarthestRoom = FindFarthestRoom(matrix, center);

        // Ejemplo de llamada con 2 habitaciones
        Vector2Int secondFarthestRoom = FindFarthestRoom(matrix, center, firstFarthestRoom);

        // Ejemplo de llamada con 3 habitaciones
        Vector2Int thirdFarthestRoom = FindFarthestRoom(matrix, center, firstFarthestRoom, secondFarthestRoom);

        Debug.Log("First Farthest Room: " + firstFarthestRoom);
        Debug.Log("Second Farthest Room: " + secondFarthestRoom);
        Debug.Log("Third Farthest Room: " + thirdFarthestRoom);
        // Puedes continuar con más pasos según sea necesario
    }

    private Vector2Int CalculateCenter(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        return new Vector2Int(rows / 2, cols / 2);
    }

    private Vector2Int FindFarthestRoom(int[,] matrix, params Vector2Int[] targetRooms)
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

        // Encuentra la media de las distancias acumuladas a todas las habitaciones objetivo
        float averageDistance = 0f;
        foreach (var targetRoom in targetRooms)
        {
            averageDistance += distanceMap[targetRoom];
        }
        averageDistance /= targetRooms.Length;

        // Encuentra la habitación más alejada en promedio de las habitaciones objetivo
        float maxAverageDistance = 0f;
        foreach (var targetRoom in targetRooms)
        {
            float roomAverageDistance = distanceMap[targetRoom] - averageDistance;
            if (roomAverageDistance > maxAverageDistance)
            {
                maxAverageDistance = roomAverageDistance;
                farthestRoom = targetRoom;
            }
        }

        return farthestRoom;
    }

    private List<Vector2Int> GetAdjacentRooms(Vector2Int room, int[,] matrix)
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

    private bool HasDoor(Vector2Int room1, Vector2Int room2, int[,] matrix)
    {
        return matrix[room1.x, room1.y] != 0 && matrix[room2.x, room2.y] != 0;
    }

    private bool IsValidRoom(Vector2Int room, int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        return room.x >= 0 && room.x < rows && room.y >= 0 && room.y < cols;
    }
}
