using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

[CreateAssetMenu(menuName = "States/PlayerIdleState")]
public class IdleState : States
{

    #region Constructor
    public IdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Methods
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
                newPlayerState = Instantiate(newPlayerState);
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
    // Aquí hacer la lógica para cuando el jugador no haga nada, normalmente solo será que haga la animación de idle del objeto
    public override void Update()
    {
        return;
    }
    #endregion
}
