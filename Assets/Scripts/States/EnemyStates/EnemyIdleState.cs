using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyIdleState")]

public class EnemyIdleState : States
{
    [Header("States")]
    [SerializeField] private States EnemyChaseState;
    [SerializeField] private States EnemyAttackState;
    [SerializeField] private States EnemyDieState;


    [SerializeField] private float maxAttackDistance = 5f;
    [SerializeField] private float distanceToSeePlayer = 20f;
    #region Constructor
    public EnemyIdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region AbstractMethods
    public override States CheckTransitions()
    {
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), stateGameObject.transform.position);
        States newEnemyState = null;

        if (distance < distanceToSeePlayer)
        {
            newEnemyState = EnemyChaseState;
        }
        if (stateGameObject.GetComponent<HealthBehaviour>().CheckIfDeath())
        {
            newEnemyState = EnemyDieState;
        }

        if (distance <= maxAttackDistance)
        {
            newEnemyState = EnemyAttackState;
        }

        if (newEnemyState != null)
        {
            newEnemyState.InitializeState(stateGameObject);
            newEnemyState.Start();
        }
        return newEnemyState;

    }
    #endregion

    #region Methods
    void Start()
    {
    }

    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        //Esto es porque como de momento no hace nada que se cancele con el return
        return;
    }


    #endregion
}
