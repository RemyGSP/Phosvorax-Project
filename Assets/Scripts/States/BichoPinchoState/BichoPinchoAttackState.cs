using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEngine.GridBrushBase;

[CreateAssetMenu(menuName = "EnemyStates/BichoPinchoAttack")]
public class BichoPinchoAttackState : States
{
    [SerializeField] private float force;
    [SerializeField] private float stopTime = 2f; 
    private Rigidbody rb;
    private bool reached;
    private Vector3 targetPosition;
    private float stopTimer = 0f;
    private float currentTime = 0f;
    [SerializeField] private  AnimationCurve accelerationCurve;
    [SerializeField] private AnimationCurve deccelerationCurve;
    [SerializeField] GameObject feedback;
    Vector3 rotationPoint;
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
        stateGameObject.GetComponent<Animator>().SetTrigger("attack");
        rb = stateGameObject.GetComponent<Rigidbody>();
        reached = false;
        targetPosition = PlayerReferences.instance.GetPlayerCoordinates();
        targetPosition.y = stateGameObject.transform.position.y;
        stateGameObject.transform.LookAt(targetPosition);
        rotationPoint = stateGameObject.GetComponent<RotateFromPosition>().rotationPoint.position;
        Instantiate(feedback, stateGameObject.GetComponent<FeedbackPosition>().GetFeedbackSpawnPos(),Quaternion.identity);

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;

        float acceleration = accelerationCurve.Evaluate(currentTime);
        Debug.Log("Reached " + reached + " Timer: " + stopTimer + " Distance: " + Vector3.Distance(stateGameObject.transform.position, targetPosition));
        if (reached)
        {
            Vector3 lastPos = rb.velocity;
            stopTimer += Time.deltaTime;
            float decceleration = deccelerationCurve.Evaluate(stopTimer);
            rb.velocity = (lastPos / force / acceleration).normalized * force * decceleration;

        }
        else
        {
            rb.velocity = (targetPosition - stateGameObject.transform.position).normalized * force * acceleration;

        }
        if (Vector3.Distance(stateGameObject.transform.position, targetPosition) < 0.5f)
        {
            reached = true;
        }
    }



}
 