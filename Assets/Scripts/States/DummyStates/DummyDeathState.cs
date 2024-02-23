using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DummyStates/DeathState")]
public class DummyDeathState : States
{
    private Animator animator;
    private float animationClipDuration;
    private float animationTimer;
    public DummyDeathState(GameObject stateGameObject) : base(stateGameObject)
    {
    }


    public override void OnExitState()
    {
        animator.SetBool("dead", false);
    }

    public override void Update()
    {
        animationTimer = Time.deltaTime;
        Revive();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        animator = stateGameObject.GetComponent<Animator>();
        animator.SetBool("dead", true);
        animationClipDuration = animator.GetCurrentAnimatorClipInfo(0).Length;
        Debug.Log(animationClipDuration);
    }

    private void Revive()
    {
        MonoInstance.instance.StartCoroutine(_Revive());
    }

    private IEnumerator _Revive()
    {
        yield return new WaitForSeconds(5f);
        stateGameObject.GetComponent<HealthBehaviour>().Revive();
    }

}
