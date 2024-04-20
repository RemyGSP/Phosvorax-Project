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
        if (stateGameObject.CompareTag("Boss"))
        {
            Instantiate(stateGameObject.GetComponent<BossReferences>().onWinPrefab);
        }
        Destroy(stateGameObject);
        canRevive = false;
        stateGameObject.GetComponent<CrystalDrop>().Drop();
        Debug.Log("memori");
        stateGameObject.transform.parent.GetComponent<RooomController>().CheckToOpenDoors(stateGameObject);
        
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

