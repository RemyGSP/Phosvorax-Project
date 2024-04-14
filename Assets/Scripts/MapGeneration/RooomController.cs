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

    [SerializeField] private GameObject[] inRoomEnemyList; 

    private void Awake()
    {
        _doorList = new List<GameObject> { door4, door2, door8, door1 };
    }


}


 
