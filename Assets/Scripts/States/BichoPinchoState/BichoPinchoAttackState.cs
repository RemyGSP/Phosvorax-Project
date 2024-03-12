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
            Vector3 directionToLook = stateGameObject.GetComponent<RotateFromPosition>().rotationPoint.position - stateGameObject.transform.position;

            // Rotate towards the rotation point
            Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
            Vector3 rotationPoint = stateGameObject.GetComponent<RotateFromPosition>().rotationPoint.position;
            Vector3 rotationAxis = rotationPoint - stateGameObject.transform.position;
            stateGameObject.transform.RotateAround(rotationPoint,rotationAxis,targetRotation.y);

        }
        if (Vector3.Distance(stateGameObject.transform.position, targetPosition) < 0.3f)
        {
            reached = true;
        }
    }



}
 