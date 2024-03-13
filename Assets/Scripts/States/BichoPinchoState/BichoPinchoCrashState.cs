using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyStates/BichoPinchoCrash")]
public class BichoPinchoCrashState : States
{
    [SerializeField] private float crashForce;
    [SerializeField] private float stunTime;
    public BichoPinchoCrashState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {
        
    }

    public override void Update()
    {

    }

    public override void Start()
    {
        base.Start();
        Vector3 backwardDirection = -stateGameObject.transform.forward;
        stateGameObject.GetComponent<BichoPinchoReferences>().Stun(stunTime);
        // Apply the backward force to the object
        stateGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        stateGameObject.GetComponent<Rigidbody>().AddForce(backwardDirection * crashForce, ForceMode.Impulse);

    }
}

