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
    private Quaternion rotation;
    //Esto se utiliza para que no golpee multiples veces, este bool se necesita para poder golpear y una vez ha golpeado se pone a false
    private bool hasHit;
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
        hasHit = false;
        GameObject feedbackSpawnPos = stateGameObject.GetComponent<FeedbackPosition>().GetFeedbackGameObject();
        infoContainer = stateGameObject.GetComponent<BichoPinchoReferences>();
        infoContainer.StopAttackTimer();
        infoContainer.RestartAttackTimer();
        infoContainer.Attack();
        rb = stateGameObject.GetComponent<Rigidbody>();
        reached = false;
        targetPosition = PlayerReferences.instance.GetPlayerCoordinates();
        targetPosition.y = stateGameObject.transform.position.y;
        GetRotation(targetPosition);
        stateGameObject.transform.rotation = Quaternion.Lerp(stateGameObject.transform.rotation, rotation, 0.05f);
        // Instantiate the object
        GameObject instantiatedObject = Instantiate(feedback, feedbackSpawnPos.transform.position, Quaternion.identity);
        instantiatedObject.GetComponent<FollowPlayer>().SetPlayer(feedbackSpawnPos);
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
            if (decceleration < 0.2f)
            {
                GetRotation(targetPosition = PlayerReferences.instance.GetPlayerCoordinates());
                stateGameObject.transform.rotation = Quaternion.Lerp(stateGameObject.transform.rotation, rotation, 0.008f);
            }
        }
        else
        {
            rb.velocity = (targetPosition - stateGameObject.transform.position).normalized * force * acceleration;
            //Hace falta el Lookat aqui porque sino se rompe la rotacion y hago este if porque si se ejecuta todo el rato que esta acelerando a veces hace el lookAt una vez se ha pasado de la 
            //direccion y corrige su direccion hacia atras
            if (currentTime > 0.0f && currentTime < 0.7f)
            {
                //stateGameObject.transform.LookAt(targetPosition);
                stateGameObject.transform.rotation = Quaternion.Lerp(stateGameObject.transform.rotation, rotation, 0.05f);

            }
        }
        if (Vector3.Distance(stateGameObject.transform.position, targetPosition) < 0.5f)
        {
            reached = true;
        }

    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasHit)
        {
            other.gameObject.GetComponent<HealthBehaviour>().Damage(infoContainer.GetDamage());
            hasHit = true;
        }
    }
    
    public void GetRotation(Vector3 targetPos)
    {
        Quaternion previousRotation = stateGameObject.transform.rotation;
        stateGameObject.transform.LookAt(targetPos);
        rotation = stateGameObject.transform.rotation;
        stateGameObject.transform.rotation = previousRotation;
    }

}
 