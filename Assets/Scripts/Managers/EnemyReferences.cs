using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReferences : MonoBehaviour
{
    #region Variables
    [Header("Enemy")]
    private GameObject enemy;

    [Header("Position")]
    static Vector3 enemyCoordinates;

    [Header("VisualContact")]
    [SerializeField] float distanceToSeePlayer;
    [SerializeField] bool playerSeen;
    [SerializeField] private GameObject visuals;

    [Header("Attack")]
    [SerializeField] float maxAttackDistance;


    [Header("StartEnemyStuff")]
    [SerializeField] private float distanceToStartCountdown; //Area para que si el jugador entra empiece a pasar el tiempo para que el enemigo se active. El numero deberia de ocupar toda la sala y ser el mismo para todos los enemigos
    [SerializeField] bool canBeStarted = false;
    #endregion


    #region Methods
    private void Start()
    {
        enemy = this.gameObject;
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

    public float GetDistanceToStartCountdown()
    {
        return distanceToStartCountdown;
    }

    public void SetCanBeStarted(bool canBeStarted)
    {
        this.canBeStarted = canBeStarted;
    }

    public bool GetCanBeStarted()
    {
        return canBeStarted;
    }

    public GameObject GetVisuals()
    {
        return visuals;
    }
    public GameObject GetEnemy()
    {
        return enemy;
    }
    #endregion
}
