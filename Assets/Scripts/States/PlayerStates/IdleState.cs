using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerIdleState")]
public class IdleState : States
{
    [Header("States")]
    [SerializeField] private States moveState;
    [SerializeField] private States rollState;
    [SerializeField] private States basicAttackState;

    #region Constructor
    public IdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Methods
    public override States CheckTransitions()
    {
        States newPlayerState = null;
        if (PlayerInputController.GetPlayerInputDirection() != Vector3.zero)
        {
            newPlayerState = moveState;
        }
        if (PlayerInputController.IsRolling() && PlayerTimers.timer.rollTimer > PlayerTimers.timer.rollCD)
        {
            newPlayerState = rollState;
        }
        if (PlayerInputController.IsAttacking() && PlayerTimers.timer.playerBasicAttackTimer > PlayerTimers.timer.playerBasicAttackCD)
        {
            newPlayerState = basicAttackState;
        }
        if (newPlayerState != null)
        {
            newPlayerState.InitializeState(stateGameObject);
            newPlayerState.Start();
        }
        return newPlayerState;
    }
    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        //Esto es porque como de momento no hace nada que se cancele con el return
        return;
    }
    #endregion
}
