using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private float currentTime = 0f;
    [SerializeField] private  AnimationCurve accelerationCurve;
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

        if (reached)
        {
            stopTimer += Time.deltaTime;

        }
        else
        {
            rb.velocity = (targetPosition - stateGameObject.transform.position) * force * acceleration;
            stateGameObject.transform.LookAt(targetPosition);

            Vector3 direction = stateGameObject.transform.position - rotationPoint;

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            stateGameObject.transform.rotation = targetRotation;

        }
        if (Vector3.Distance(stateGameObject.transform.position, targetPosition) < 0.3f)
        {
            Debug.Log("Funca");
            reached = true;
        }
    }



}
 