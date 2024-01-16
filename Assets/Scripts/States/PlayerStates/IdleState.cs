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
    [SerializeField] private States firstAbilityState;
    [SerializeField] private States secondAbilityState;
    [SerializeField] private States thirdAbilityState;
    [SerializeField] private States fourthAbilityState;


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
            switch (AbilityManager.instance.GetCurrentAbility())
            {
                case 0: newPlayerState = basicAttackState; break;
                case 1: newPlayerState = firstAbilityState; break;
                case 2: newPlayerState = secondAbilityState; break;
                case 3: newPlayerState = thirdAbilityState; break;
                case 4: newPlayerState = fourthAbilityState; break;
            }
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
        return;
    }
    #endregion
}
