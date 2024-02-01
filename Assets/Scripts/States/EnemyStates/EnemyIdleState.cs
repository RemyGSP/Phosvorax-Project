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

    #region
    private NavMeshAgent enemy;

    #endregion
    #region AbstractMethods
    public override States CheckTransitions()
    {
        bool notChanged = true;
        int counter = 0;
        States newPlayerState = null;

        while (notChanged)
        {
            newPlayerState = stateTransitions[counter].GetExitState(stateGameObject.GetComponent<StateMachine>());
            if (newPlayerState != null)
            {
                notChanged = false;
                newPlayerState.InitializeState(stateGameObject);
                newPlayerState.Start();
            }
            if (counter < stateTransitions.Length - 1)
            {
                counter++;
            }
            else
            {
                notChanged = false;
            }
        }

        return newPlayerState;
    }
    #endregion

    #region Methods
    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = 0;
    }

    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        //Esto es porque como de momento no hace nada que se cancele con el return
        return;
    }


    #endregion
}
