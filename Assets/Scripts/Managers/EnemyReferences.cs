using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReferences : MonoBehaviour
{
    #region Variables
    [Header("Enemy")]
    private GameObject enemy;
    public static EnemyReferences instance;

    [Header("Position")]
    static Vector3 enemyCoordinates;

    [Header("VisualContact")]
    [SerializeField] float distanceToSeePlayer;
    [SerializeField] bool playerSeen;

    [Header("Attack")]
    [SerializeField] float maxAttackDistance;

    #endregion


    #region Methods
    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("Manager EnemyReferences already exists");
    }
    public Vector3 GetEnemyCoordinates()
    {
        enemyCoordinates = this.transform.position;
        return enemyCoordinates;
    }

    public float GetMaxAttackPosition()
    {
        return maxAttackDistance;
    }

    public float GetDistanceToSeePlayer()
    {
        return distanceToSeePlayer;
    }

    public bool GetPlayerSeen()
    {
        return playerSeen;
    }

    public void SetPlayerSeen(bool playerSeen)
    {
        this.playerSeen = playerSeen;
    }
    #endregion
}
