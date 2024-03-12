using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "EnemyStates/BichoPinchoIdle")]
public class BichoPinchoIdleState : States
{
    [SerializeField] private float speed = 2f; // Speed of rotation
    private Rigidbody rb;
    public BichoPinchoIdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {
        return;
    }

    public override void Update()
    {
        return;
    }




    public override void FixedUpdate()
    {

    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = stateGameObject.GetComponent<Rigidbody>();
    }


}
