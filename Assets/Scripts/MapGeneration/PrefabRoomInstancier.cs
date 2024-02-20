using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Roomlist
{
    public List<GameObject> prefabRooms;
}

public class PrefabRoomInstancier : MonoBehaviour
{
    public List<Roomlist> roomTypes;
    private int[,] roomTypeMatrix;
    public GameObject[,] roomInstancesMatrix;

    public void ReceiveMatrix(int[,] matrix)
    {
        roomTypeMatrix = matrix;
        roomInstancesMatrix = new GameObject[matrix.GetLength(0), matrix.GetLength(1)];
        PrintMatrix(roomTypeMatrix);
        InstantiateRooms();
    }

    public void InstantiateRooms()
    {
        if (roomTypeMatrix == null || roomTypes == null || roomTypes.Count == 0 || roomTypes[0].prefabRooms.Count == 0)
        {
            Debug.LogError("Matrix or prefabRooms not set properly.");
            return;
        }

        int numRows = roomTypeMatrix.GetLength(0);
        int numCols = roomTypeMatrix.GetLength(1);
        float spacing = 50f;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                int roomTypeIndex = roomTypeMatrix[row, col];
                if (roomTypeIndex >= 0 && roomTypeIndex < roomTypes.Count)
                {
                    List<GameObject> roomTypePrefabs = roomTypes[roomTypeIndex].prefabRooms;
                    if (roomTypePrefabs.Count > 0)
                    {
                        GameObject selectedPrefab = roomTypePrefabs[Random.Range(0, roomTypePrefabs.Count)];
                        Vector3 spawnPosition = new Vector3(col * spacing, 0f, -row * spacing);

                        // Instanciar la habitación y asignarla a prefabRoomsMatrix
                        GameObject instantiatedRoom = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

                        // Establecer el objeto instanciado como hijo de otro objeto (por ejemplo, el objeto que tiene el script)
                        instantiatedRoom.transform.parent = transform;

                        roomInstancesMatrix[row, col] = instantiatedRoom;
                    }
                }
            }
        }
        ConnectingRoomDoors();
        GenerateNavMeshSurfaces();
    }

    void ConnectingRoomDoors()
    {
        // Iterar sobre la matriz
        for (int i = 0; i < roomInstancesMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < roomInstancesMatrix.GetLength(1); j++)
            {
                // Obtener el GameObject en la posición actual de la matriz
                GameObject currentRoom = roomInstancesMatrix[i, j];

                // Verificar si el GameObject tiene el componente RoomDoorManager
                RoomDoorManager doorManager = currentRoom.GetComponent<RoomDoorManager>();

                // Si tiene el componente RoomDoorManager
                if (doorManager != null)
                {
                    // Iterar sobre la lista de puertas
                    for (int k = 0; k < doorManager.DoorList.Count; k++)
                    {
                        // Verificar si la puerta tiene referencia
                        if (doorManager.DoorList[k] != null)
                        {
                            // Conectar la puerta con las puertas adyacentes en la matriz

                            switch (k)
                            {
                                case 0: // Puerta 4
                                    if (j > 0)
                                    {
                                        GameObject leftRoom = roomInstancesMatrix[i, j - 1];
                                        RoomDoorManager leftDoorManager = leftRoom.GetComponent<RoomDoorManager>();
                                        if (leftDoorManager != null && leftDoorManager.DoorList.Count > 2 && leftDoorManager.DoorList[2] != null)
                                        {
                                            doorManager.DoorList[k].GetComponent<DoorTpController>().destinationObject = leftDoorManager.DoorList[2];
                                        }
                                    }
                                    break;

                                case 1: // Puerta 2
                                    if (i > 0)
                                    {
                                        GameObject aboveRoom = roomInstancesMatrix[i - 1, j];
                                        RoomDoorManager aboveDoorManager = aboveRoom.GetComponent<RoomDoorManager>();
                                        if (aboveDoorManager != null && aboveDoorManager.DoorList.Count > 3 && aboveDoorManager.DoorList[3] != null)
                                        {
                                            doorManager.DoorList[k].GetComponent<DoorTpController>().destinationObject = aboveDoorManager.DoorList[3];
                                        }
                                    }
                                    break;

                                case 2: // Puerta 8
                                    if (j < roomInstancesMatrix.GetLength(1) - 1)
                                    {
                                        GameObject rightRoom = roomInstancesMatrix[i, j + 1];
                                        RoomDoorManager rightDoorManager = rightRoom.GetComponent<RoomDoorManager>();
                                        if (rightDoorManager != null && rightDoorManager.DoorList.Count > 0 && rightDoorManager.DoorList[0] != null)
                                        {
                                            doorManager.DoorList[k].GetComponent<DoorTpController>().destinationObject = rightDoorManager.DoorList[0];
                                        }
                                    }
                                    break;

                                case 3: // Puerta 1
                                    if (i < roomInstancesMatrix.GetLength(0) - 1)
                                    {
                                        GameObject belowRoom = roomInstancesMatrix[i + 1, j];
                                        RoomDoorManager belowDoorManager = belowRoom.GetComponent<RoomDoorManager>();
                                        if (belowDoorManager != null && belowDoorManager.DoorList.Count > 1 && belowDoorManager.DoorList[1] != null)
                                        {
                                            doorManager.DoorList[k].GetComponent<DoorTpController>().destinationObject = belowDoorManager.DoorList[1];
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        DestroyRooms();
    }

    private void DestroyRooms()
    {
        int numRows = roomInstancesMatrix.GetLength(0);
        int numCols = roomInstancesMatrix.GetLength(1);

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                GameObject roomInstance = roomInstancesMatrix[row, col];
                if (roomInstance != null && roomInstance.name == "d0t0(Clone)")
                {
                    Destroy(roomInstance);
                    roomInstancesMatrix[row, col] = null; // Limpiar la referencia en la matriz
                }
            }
        }
    }

    void GenerateNavMeshSurfaces()
    {
        foreach (GameObject go in roomInstancesMatrix)
        {
            Unity.AI.Navigation.NavMeshSurface[] surfaces = go.GetComponentsInChildren<Unity.AI.Navigation.NavMeshSurface>();
            foreach (Unity.AI.Navigation.NavMeshSurface surface in surfaces)
            {
                surface.BuildNavMesh();
            }
        }
    }

    void PrintMatrix(int[,] matrix)
    {
        string matrixString = "Matriz recibida:\n";

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
