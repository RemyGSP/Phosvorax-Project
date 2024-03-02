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
        Type0 = 0,
        Type1 = 1,
        Type2 = 2,
        Type3 = 3
    }

    // Variable para almacenar la opción seleccionada
    public RoomType typeSelected;

    private void Awake()
    {
        _doorList = new List<GameObject> { door4, door2, door8, door1 };
    }


}


 
