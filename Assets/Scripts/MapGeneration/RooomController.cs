using System.Collections.Generic;
using UnityEngine;

public class RooomController : MonoBehaviour
{
    [SerializeField] private GameObject door4;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door8;
    [SerializeField] private GameObject door1;
    private List<GameObject> _doorList;
    public List<GameObject> DoorList => _doorList;  // Propiedad pública solo de lectura para acceder a la lista desde otro script

    [SerializeField] private List<GameObject> inRoomEnemyList;
    [SerializeField] private bool isPlayerInRoom;
    private bool isEmptyMessageSent = false;

    private void Awake()
    {
        _doorList = new List<GameObject> { door4, door2, door8, door1 };
        isPlayerInRoom = false;
    }
    
    private void OpenDoors(){
        foreach (GameObject door in _doorList)
        {
            if (door != null)
            {
                 DoorTpController doorController = door.GetComponent<DoorTpController>();
                if (doorController != null)
                {
                    doorController.TpOpen();
                }
                else
                {
                    Debug.LogWarning("DoorTpController no encontrado en " + door.name);
                }
            }
        }
    }
    private void CloseDoors(){
        foreach (GameObject door in _doorList)
        {
            if (door != null)
            {
                 DoorTpController doorController = door.GetComponent<DoorTpController>();
                if (doorController != null)
                {
                    doorController.TpClose();
                }
                else
                {
                    Debug.LogWarning("DoorTpController no encontrado en " + door.name);
                }
            }
        }
    }

    private void CheckToCloseDoors(){
        if (isPlayerInRoom&&inRoomEnemyList.Count>0){
            CloseDoors();
        }
        
    }

    public void CheckToOpenDoors(GameObject gameObjectToRemove)
    {
        // Eliminar el GameObject especificado de la lista
        if (gameObjectToRemove != null && inRoomEnemyList.Contains(gameObjectToRemove))
        {
            inRoomEnemyList.Remove(gameObjectToRemove);
        }

        // Verificar si hay un jugador en la habitación y solo un enemigo presente
        if (isPlayerInRoom && inRoomEnemyList.Count < 1)
        {
            OpenDoors();
        }
    }



    public void SetIsPlayerInRoom(bool pir){
        isPlayerInRoom = pir;
        CheckToCloseDoors();
    }
}




 
