using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EnemyStates/RollingEnemyStunned")]
public class RollingEnemyStunnedState : States
{
    [SerializeField] private float stunTime;
    private float startigMass;
    private Rigidbody rb;
    public RollingEnemyStunnedState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {

        base.OnExitState();
        stateGameObject.GetComponent<RollingEnemyReferences>().SetIsStunned(false);
        rb.mass = startigMass;
    }

    public override void Update()
    {
        
    }

    public override void Start()
    {
        startigMass = stateGameObject.GetComponent<Rigidbody>().mass;
        stateGameObject.GetComponent<RollingEnemyReferences>().Stun(stunTime);
        stateGameObject.GetComponent<RollingEnemyReferences>().SetIsStunned(true);
        stateGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        rb = stateGameObject.GetComponent<Rigidbody>();
        rb.mass = Mathf.Infinity;


    }
}

