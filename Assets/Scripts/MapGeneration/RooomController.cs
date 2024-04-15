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
    private bool isEmptyMessageSent = false;

    private void Awake()
    {
        _doorList = new List<GameObject> { door4, door2, door8, door1 };
    }

    private void FixedUpdate()
    {
        // Verificar y eliminar referencias nulas de la lista de enemigos
        for (int i = inRoomEnemyList.Count - 1; i >= 0; i--)
        {
            if (inRoomEnemyList[i] == null)
                inRoomEnemyList.RemoveAt(i);
        }

        // Si la lista de enemigos está vacía y no se ha enviado el mensaje aún, mostrar un mensaje de depuración
        if (inRoomEnemyList.Count == 0 && !isEmptyMessageSent)
        {
            OpenDoors();
            isEmptyMessageSent = true;
        }else if (!isEmptyMessageSent)
        {
            CloseDoors();
        }
            
        
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
}




 
