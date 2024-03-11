using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/BichoPinchoAttack")]
public class BichoPinchoAttackState : States
{
    [SerializeField] private float force;
    [SerializeField] private float stopTime = 2f; 
    private Rigidbody rb;
    private bool reached;
    private Vector3 targetPosition;
    private float stopTimer = 0f;

    public BichoPinchoAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States aux = null;
        if (stopTimer > stopTime)
        {
            aux = base.CheckTransitions();
        }
        return aux;
    }
    public override void OnExitState()
    {
        return;
    }

    public override void Start()
    {
        rb = stateGameObject.GetComponent<Rigidbody>();
        reached = false;
        targetPosition = PlayerReferences.instance.GetPlayerCoordinates();
        rb.AddForce((targetPosition - stateGameObject.transform.position) * force, ForceMode.Impulse);
        MonoInstance.instance.StartCoroutine(_StopMoving(1));
    }

    public override void Update()
    {
        Debug.Log(stopTimer);
        if (reached)
        {
            stopTimer += Time.deltaTime;
        }
        else
        {
            rb.AddForce((targetPosition - stateGameObject.transform.position) * force, ForceMode.Impulse);

        }
        if (Vector3.Distance(stateGameObject.transform.position, targetPosition) < 0.3f)
        {
            reached = true;
        }
    }

    private IEnumerator _StopMoving(float time)
    {
        yield return new WaitForSeconds(time);
        rb.velocity = Vector3.zero;
    }


}
