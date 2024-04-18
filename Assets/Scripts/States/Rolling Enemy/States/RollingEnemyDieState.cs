using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/RollingEnemyDeath")]
public class RollingEnemyDieState : States
{

    public RollingEnemyDieState(GameObject stateGameObject) : base(stateGameObject)
    {

    }
    
    
    public override void OnExitState()
    {
        return;
    }

    public override void Start()
    {
        stateGameObject.GetComponent<CrystalDrop>().Drop();
        Destroy(stateGameObject);
    }

    public override void Update()
    {
        return;
    }
}
