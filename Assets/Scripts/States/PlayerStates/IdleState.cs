using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerIdleState")]
public class IdleStateDummy : States
{
    Animator animator;
    #region Constructor
    public IdleStateDummy(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public new void OnEnterState()
    {
        base.OnEnterState();
    }
    public override void OnExitState()
    {
        PlayerReferences.instance.GetPlayerAnimator().SetBool("idle", false);
    }
    #endregion

    #region Methods
    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        PlayerReferences.instance.GetPlayerAnimator().SetBool("idle", true);
    }
    #endregion
}
