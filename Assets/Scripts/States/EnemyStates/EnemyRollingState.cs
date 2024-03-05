using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "EnemyStates/EnemyRollingState")]
public class TESTSCRIPT : States
{

    #region Constructor
    public TESTSCRIPT(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables

    [Header("MoveValues")]
    private float currentRollingSpeed;
    [SerializeField] private float maxRollingSpeed;
    [SerializeField] AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeToSpendRolling;


    [Header("Raycast Values")]
    [SerializeField] private float maxRaycastDistance;

    private float elapsedTime = 0f;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    private NavMeshAgent enemy;
    #endregion

    #region Methods
    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = 0; //poner la velocidad del navmesh a 0 para que no se siga moviendo al entrar a este estado
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();

    }

    public override void Update()
    {
        RaycastHit hit;
        Vector3 direction = stateGameObject.transform.forward;
        if (Physics.Linecast(stateGameObject.transform.position, stateGameObject.transform.position + direction * maxRaycastDistance, out hit))
        {
            Debug.DrawLine(stateGameObject.transform.position, stateGameObject.transform.position + direction * maxRaycastDistance, Color.green, 0.2f);

            if (hit.collider.CompareTag("Player"))
            {
              
            }
            else 
            {
                direction = Vector3.ProjectOnPlane(direction, hit.normal).normalized;
                rotateCharacter.Rotate(stateGameObject.transform.rotation, direction);
            }
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime < timeToSpendRolling)
        {
            float t = Mathf.Clamp01(elapsedTime / timeToSpendRolling);

            float curveValue = curveToMaxAcceleration.Evaluate(t);

            float targetSpeed = curveValue * maxRollingSpeed;

            rigidBody.velocity = rigidBody.velocity.normalized * targetSpeed;
        }
    }



    public override void OnExitState()
    {
    }


    #endregion



}


