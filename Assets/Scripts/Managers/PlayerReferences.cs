using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    #region Variables
    [Header("Player")]
    [SerializeField] private GameObject player;
    public static PlayerReferences instance;

    [Header("Position")]
    static Vector3 playerCoordinates;
    [SerializeField] LayerMask groundMask; 
    #endregion


    #region Methods
    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("Manager PLayerReferences already exists");
    }
    public Vector3 GetPlayerCoordinates()
    {
        playerCoordinates = player.transform.position;
        return playerCoordinates;
    }

    public Vector3 GetMouseTargetDir()
    {
        // Obtener la posición del ratón en la pantalla
        Vector3 mousePos = Input.mousePosition;

        // Calcular la dirección del ratón en el mundo
        Ray castPoint = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Vector3 targetDir = Vector3.zero;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, groundMask))
        {
            targetDir = hit.point; // Conseguir la direccion a la que esta apuntando el raton en el mundo
            targetDir.y = 0f; // Mantener en el plano XY
        }
        else
        {
            targetDir = castPoint.direction;
            targetDir.y = 0f; // Mantener en el plano XY
        }
        return targetDir;
    }
    #endregion
}
