using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    #region Variables
    public static PlayerReferences instance;
    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject footPos;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject playerVisuals;
    public bool canMove;
    [SerializeField] private GameObject playerVisiblePoint;
    [Header("Position")]
    static Vector3 playerCoordinates;
    [SerializeField] LayerMask groundMask;
    #endregion


    #region Methods
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Manager PlayerReferences already exists");
        }
    }
    public Vector3 GetPlayerCoordinates()
    {
        playerCoordinates = player.transform.position;
        return playerCoordinates;
    }

    public bool CheckIfGrounded()
    {
        // Set the maximum distance for the ray
        float maxRaycastDistance = 10f; // Adjust this value based on your needs
        Debug.DrawRay(footPos.transform.position, Vector3.down,Color.red);
        // Check if a ray from footPos.position downward hits anything within the specified distance
        return Physics.Raycast(footPos.transform.position, Vector3.down, maxRaycastDistance);
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

    public Animator GetPlayerAnimator()
    {
        return playerAnimator;
    }

    public Vector3 GetPlayerVisiblePoint()
    {
        return playerVisiblePoint.transform.position;
    }
    #endregion
}
