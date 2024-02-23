using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DummyStates/HitState")]
public class DummyHitState : States
{
    private Animator animator;

    public DummyHitState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {
        return;
    }

    public override void Update()
    {
    }

    public override void Start()
    {
        Debug.Log("Dummy Start");
        stateGameObject.GetComponent<HealthBehaviour>().UndoHit();
        animator = stateGameObject.GetComponent<Animator>();
        animator.SetTrigger("hit");
    }



}
