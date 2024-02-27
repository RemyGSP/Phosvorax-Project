using System.Collections.Generic;
using UnityEngine;

public class RooomController : MonoBehaviour
{
    [SerializeField]
    private GameObject door4;

    [SerializeField]
    private GameObject door2;

    [SerializeField]
    private GameObject door8;

    [SerializeField]
    private GameObject door1;

    private List<GameObject> _doorList;

    // Propiedad pública solo de lectura para acceder a la lista desde otro script
    public List<GameObject> DoorList => _doorList;

    // Enumeración de tipos de vegetales
    public enum RoomType
    {
        multipurpose,
        boss,
        hability
    }

    // Variable para almacenar la opción seleccionada
    public RoomType typeSelected;

    private void Awake()
    {
        _doorList = new List<GameObject> { door4, door2, door8, door1 };
    }


}


 
