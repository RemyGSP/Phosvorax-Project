using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyDieState")]
public class EnemyDieState : States
{
    [Header("States")]
    [SerializeField] private States EnemyIdleState;

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
        States newGameState = null;
        if (canRevive)
        {
            stateGameObject.SetActive(true);
            newGameState = Instantiate(EnemyIdleState);
        }
        if (newGameState != null)
        {
            newGameState.InitializeState(stateGameObject);
            newGameState.Start();
        }
        return newGameState;

    }

    #region Methods


    public override void Start()
    {
        stateGameObject.SetActive(false);
        canRevive = false;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
    }

    #endregion
}

