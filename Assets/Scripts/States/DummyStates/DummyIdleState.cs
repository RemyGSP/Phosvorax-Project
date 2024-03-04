using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DummyStates/IdleState")]
public class DummyIdleState : States
{

    private Animator animator;
    public DummyIdleState(GameObject stateGameObject) : base(stateGameObject)
    {
    }



    // Start is called before the first frame update
    public override void Start()
    {
        Debug.Log("Dummy Idle");
        animator = stateGameObject.GetComponent<Animator>();
        animator.SetBool("Idle",true);
    }
    public override void OnExitState()
    {
        animator.SetBool("Idle", false);

    }

    public override void Update()
    {
        return;
    }


}
