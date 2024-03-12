using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EnemyRollingState")]
public class EnemyRollingState : States
{
    #region Variables

    [Header("MoveValues")]
    [SerializeField] private float maxRollingSpeed;
    [SerializeField] private AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeToSpendRolling;

    [Header("Raycast Values")]
    [SerializeField] private float maxRaycastDistance;

    private float elapsedTime = 0f;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private NavMeshAgent enemy;
    private Vector3 direction;

    Vector3 targetSpeed;

    #endregion

    #region Constructor
    public EnemyRollingState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Methods

    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        direction = stateGameObject.transform.forward;
    }


    public override void Update()
    {

        RaycastHit hit;

        if (Physics.Linecast(stateGameObject.transform.position, stateGameObject.transform.position + direction * 7f, out hit))
        {
            Debug.DrawRay(stateGameObject.transform.position, direction * 5f, Color.red);

            if (!hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(stateGameObject.transform.position, direction * 5f, Color.red);

                Vector3 normal = hit.normal;

                direction = Vector3.Reflect(direction, normal).normalized;

                float angle = Vector3.SignedAngle(stateGameObject.transform.forward, direction, Vector3.up);
                Quaternion quat = Quaternion.Euler(0, angle, 0);
                stateGameObject.transform.rotation *= quat;
                targetSpeed = quat * targetSpeed;
                Debug.Log("Hit object: " + hit.collider.name);

            }
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime < timeToSpendRolling)
        {
            float t = Mathf.Clamp01(elapsedTime / timeToSpendRolling);
            float curveValue = curveToMaxAcceleration.Evaluate(t);
            float targetAcceleration = curveValue * maxRollingSpeed;
            targetSpeed += stateGameObject.transform.forward * targetAcceleration * Time.deltaTime;
            rigidBody.velocity = targetSpeed;
        }

        Debug.Log(rigidBody.velocity);
    }



    public override void OnExitState()
    {
    }

    #endregion
}
