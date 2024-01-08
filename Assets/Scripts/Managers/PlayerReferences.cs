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

    #endregion


    #region Methods

    public Vector3 GetPlayerCoordinates()
    {
        playerCoordinates = player.transform.position;
        return playerCoordinates;
    }

    #endregion
}
