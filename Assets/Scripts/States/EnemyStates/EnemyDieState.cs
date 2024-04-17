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


    #region Methods

   
    public override void Start()
    {
        
        Destroy(stateGameObject);
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
        stateGameObject.transform.parent.GetComponent<RooomController>().CheckToOpenDoors();
    }

    #endregion
}

