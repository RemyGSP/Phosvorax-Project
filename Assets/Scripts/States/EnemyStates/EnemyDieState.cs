using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyDieState")]
public class EnemyDieState : States
{

    #region Constructor
    public EnemyDieState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables

    private bool canRevive;
    #endregion

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

    #region Methods


    public override void Start()
    {
        stateGameObject.SetActive(false);
        canRevive = false;
        stateGameObject.GetComponent<CrystalDrop>().Drop();
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
    }

    public override void OnExitState()
    {
        return;
    }

    #endregion
}

