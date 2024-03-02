using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[System.Serializable]
public class Roomlist
{
    public List<GameObject> prefabRooms;
    public List<GameObject> bossRooms;
    public List<GameObject> spawnRoom;
    public List<GameObject> shopRoom;

    
}

public class PrefabRoomInstancier : MonoBehaviour
{
    public List<Roomlist> prefabRooms;

    private int[,] roomLayoutTypeMatrix;
    private int[,] roomTypeMatrix;
    public GameObject[,] roomInstancesMatrix;

    public Vector2Int endRoom;
    public Vector2Int spawnRoom;

    public void ReceiveMatrix(int[,] matrix1, int[,] matrix2)
    {
        roomLayoutTypeMatrix = matrix1;
        roomTypeMatrix  = matrix2;
        roomInstancesMatrix = new GameObject[matrix1.GetLength(0), matrix1.GetLength(1)];
        PrintMatrix(roomLayoutTypeMatrix);
        PrintMatrix(roomTypeMatrix);
        InstantiateRooms();
    }

  public void InstantiateRooms()
{
    if (roomLayoutTypeMatrix == null || prefabRooms == null || prefabRooms.Count == 0)
    {
        Debug.LogError("Matrix or prefabRooms not set properly.");
        return;
    }

    int numRows = roomLayoutTypeMatrix.GetLength(0);
    int numCols = roomLayoutTypeMatrix.GetLength(1);
    float spacing = 50f;

    for (int row = 0; row < numRows; row++)
    {
        for (int col = 0; col < numCols; col++)
        {
            int layoutTypeIndex = roomLayoutTypeMatrix[row, col];
            int roomTypeIndex = roomTypeMatrix[row, col];

            List<GameObject> selectedRoomList;
            if (layoutTypeIndex >= 0 && layoutTypeIndex < prefabRooms.Count)
                {
                    switch (roomTypeIndex)
                {
                    case 1:
                       selectedRoomList = prefabRooms[layoutTypeIndex].bossRooms;
                       if (selectedRoomList.Count > 0)
                        {
                            GameObject selectedPrefab = selectedRoomList[Random.Range(0, selectedRoomList.Count)];
                            Vector3 spawnPosition = new Vector3(col * spacing, 0f, -row * spacing);

                            // Instanciar la habitación y asignarla a prefabRoomsMatrix
                            GameObject instantiatedRoom = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

                            // Establecer el objeto instanciado como hijo de otro objeto (por ejemplo, el objeto que tiene el script)
                            instantiatedRoom.transform.parent = transform;

                            roomInstancesMatrix[row, col] = instantiatedRoom;
                        }
                        break;
                    case 2:
                        selectedRoomList = prefabRooms[layoutTypeIndex].spawnRoom;
                        if (selectedRoomList.Count > 0)
                        {
                            GameObject selectedPrefab = selectedRoomList[Random.Range(0, selectedRoomList.Count)];
                            Vector3 spawnPosition = new Vector3(col * spacing, 0f, -row * spacing);

                            // Instanciar la habitación y asignarla a prefabRoomsMatrix
                            GameObject instantiatedRoom = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

                            // Establecer el objeto instanciado como hijo de otro objeto (por ejemplo, el objeto que tiene el script)
                            instantiatedRoom.transform.parent = transform;

                            roomInstancesMatrix[row, col] = instantiatedRoom;
                        }
                        break;
                    case 3:
                        selectedRoomList = prefabRooms[layoutTypeIndex].shopRoom;
                        if (selectedRoomList.Count > 0)
                        {
                            GameObject selectedPrefab = selectedRoomList[Random.Range(0, selectedRoomList.Count)];
                            Vector3 spawnPosition = new Vector3(col * spacing, 0f, -row * spacing);

                            // Instanciar la habitación y asignarla a prefabRoomsMatrix
                            GameObject instantiatedRoom = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

                            // Establecer el objeto instanciado como hijo de otro objeto (por ejemplo, el objeto que tiene el script)
                            instantiatedRoom.transform.parent = transform;

                            roomInstancesMatrix[row, col] = instantiatedRoom;
                        }
                        break;
                    default:
                        selectedRoomList = prefabRooms[layoutTypeIndex].prefabRooms;
                        if (selectedRoomList.Count > 0)
                        {
                            GameObject selectedPrefab = selectedRoomList[Random.Range(0, selectedRoomList.Count)];
                            Vector3 spawnPosition = new Vector3(col * spacing, 0f, -row * spacing);

                            // Instanciar la habitación y asignarla a prefabRoomsMatrix
                            GameObject instantiatedRoom = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

                            // Establecer el objeto instanciado como hijo de otro objeto (por ejemplo, el objeto que tiene el script)
                            instantiatedRoom.transform.parent = transform;

                            roomInstancesMatrix[row, col] = instantiatedRoom;
                        }
                        break;
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
                RooomController doorManager = currentRoom.GetComponent<RooomController>();

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
                                        RooomController leftDoorManager = leftRoom.GetComponent<RooomController>();
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
                                        RooomController aboveDoorManager = aboveRoom.GetComponent<RooomController>();
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
                                        RooomController rightDoorManager = rightRoom.GetComponent<RooomController>();
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
                                        RooomController belowDoorManager = belowRoom.GetComponent<RooomController>();
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
                if (roomInstance != null && roomInstance.name == "0(Clone)")
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
        DestroyRooms();
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
