using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Android;

[CreateAssetMenu(menuName = "States/PlayerIdleState")]
public class IdleState : States
{

    #region Constructor
    public IdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public new void OnEnterState()
    {
        base.OnEnterState();
        PlayerReferences.instance.GetPlayerAnimator().SetBool("idle",true);
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
        return;
    }
    #endregion
}
