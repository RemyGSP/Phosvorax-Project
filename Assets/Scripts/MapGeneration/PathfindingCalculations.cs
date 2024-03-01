using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingCalculations : MonoBehaviour
{
    public static List<Vector2Int> FindFarthestRooms(int[,] matrix)
    {
        Vector2Int center = CalculateCenter(matrix);

        // Encuentra la habitación más alejada del centro
        Vector2Int firstFarthestRoom = FindFarthestRoom(matrix, center);

        // Encuentra la habitación más alejada de la habitación más alejada del centro
        Vector2Int secondFarthestRoom = FindFarthestRoom(matrix, center, firstFarthestRoom);

        // Retorna las habitaciones encontradas
        return new List<Vector2Int> { firstFarthestRoom, secondFarthestRoom };
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
