using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerIdleState")]
public class IdleState : States
{
    Animator animator;
    private Rigidbody rigidBody;
    #region Constructor
    public IdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void Start(){
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
    }
    public new void OnEnterState()
    {
        base.OnEnterState();
    }
    public override void OnExitState()
    {
        PlayerReferences.instance.GetPlayerAnimator().SetBool("idle", false);
        base.OnExitState();
    }
    #endregion

    #region Methods
    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        rigidBody.velocity = Vector3.zero;
        PlayerReferences.instance.GetPlayerAnimator().SetBool("idle", true);
        
    }
    #endregion
}
