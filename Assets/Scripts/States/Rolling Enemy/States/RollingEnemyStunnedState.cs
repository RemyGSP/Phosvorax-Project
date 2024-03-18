using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyStates/RollingEnemyStunned")]
public class RollingEnemyStunnedState : States
{
    [SerializeField] private float stunTime;
    public RollingEnemyStunnedState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {
        stateGameObject.GetComponent<RollingEnemyReferences>().SetIsStunned(false);
    }

    public override void Update()
    {

    }

    public override void Start()
    {
        stateGameObject.GetComponent<RollingEnemyReferences>().Stun(stunTime);
        stateGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
       
    }
}

