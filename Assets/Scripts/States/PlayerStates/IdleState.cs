using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerIdleState")]
public class IdleState : States
{
    [SerializeField] private States moveState;
    [SerializeField] private States rollState;
    [SerializeField] private States basicAttackState;

    public IdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newPlayerState = null;
        if (PlayerInputController.GetPlayerInputDirection() != Vector3.zero)
        {
            newPlayerState = moveState;
        }
        if (PlayerInputController.IsRolling() && Timers.timer.rollTimer > Timers.timer.rollCD)
        {
            newPlayerState = rollState;
        }
        if (PlayerInputController.IsAttacking() && Timers.timer.playerBasicAttackTimer > Timers.timer.playerBasicAttackCD)
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
    }
}
