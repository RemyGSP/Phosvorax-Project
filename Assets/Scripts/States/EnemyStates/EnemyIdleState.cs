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

    #region Constructor
    public EnemyIdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    private bool playerSeen;
    #endregion

    #region AbstractMethods
    public override States CheckTransitions()
    {
        States newEnemyState = null;

        if (playerSeen)
        {
            newEnemyState = EnemyChaseState;
        }
        /*if (PlayerInputController.IsRolling() && Timers.timer.rollTimer > Timers.timer.rollCD)
        {
            newEnemyState = EnemyDieState;
        }*/

        /*if (//HACER PARA QUE ATAQUE)
        {
            newEnemyState = EnemyAttackState;
        }*/
        if (newEnemyState != null)
        {
            newEnemyState.InitializeState(stateGameObject);
            newEnemyState.Start();
        }
        return newEnemyState;

    }
    #endregion

    #region Methods
    public override void Start()
    {
        playerSeen = false;
    }

    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        //Esto es porque como de momento no hace nada que se cancele con el return
        return;
    }


    //CAMBIAR ESTO

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = true;
        }
    }*/

    #endregion
}
