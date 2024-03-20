using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EnemyIdleState")]

public class EnemyIdleState : States
{
    #region Constructor
    public EnemyIdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    private NavMeshAgent enemy;

    #endregion
    #region AbstractMethods

    #endregion

    #region Methods
    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = 0;
    }

    // Aqu� hacer la l�gica para cuando el jugador no haga nada, normalmente solo ser� que haga la animaci�n de idle del objeto
    public override void Update()
    {
        //Esto es porque como de momento no hace nada que se cancele con el return
        return;
    }

    public override void OnExitState()
    {
        return;
    }


    #endregion
}
