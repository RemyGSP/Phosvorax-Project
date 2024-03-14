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
    //En este script es donde guardo informacion que transciende los estados para que lo puedan usar las decisiones etc
    private BichoPinchoReferences infoContainer;
    public BichoPinchoAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        return base.CheckTransitions();
    }
    public override void OnExitState()
    {
        infoContainer.StopAttack();
        infoContainer.StartAttackTimer();
        return;
    }

    public override void Start()
    {
        infoContainer = stateGameObject.GetComponent<BichoPinchoReferences>();
        infoContainer.StopAttackTimer();
        infoContainer.RestartAttackTimer();
        infoContainer.Attack();
        rb = stateGameObject.GetComponent<Rigidbody>();
        reached = false;
        targetPosition = PlayerReferences.instance.GetPlayerCoordinates();
        targetPosition.y = stateGameObject.transform.position.y;
        stateGameObject.transform.LookAt(targetPosition);
        Instantiate(feedback, stateGameObject.GetComponent<FeedbackPosition>().GetFeedbackSpawnPos(),Quaternion.identity);

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;

        float acceleration = accelerationCurve.Evaluate(currentTime);
        if (reached)
        {
            stopTimer += Time.deltaTime;
            infoContainer.StopAttack();
            infoContainer.StartAttackTimer();
            Vector3 lastPos = rb.velocity;
            float decceleration = deccelerationCurve.Evaluate(stopTimer);
            rb.velocity = (lastPos / force / acceleration).normalized * force * decceleration;

        }
        else
        {
            stateGameObject.transform.LookAt(targetPosition);
            rb.velocity = (targetPosition - stateGameObject.transform.position).normalized * force * acceleration;

        }
        if (Vector3.Distance(stateGameObject.transform.position, targetPosition) < 0.5f)
        {
            reached = true;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthBehaviour>().Damage(infoContainer.GetDamage());
        }
    }



}
 