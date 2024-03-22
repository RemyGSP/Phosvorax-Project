using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/RollingEnemyRollingState")]
public class RollingEnemyRollingState : States
{
    #region Variables

    [Header("MoveValues")]
    [SerializeField] private float startSpeed;
    [SerializeField] private float maxRollingSpeed;
    [SerializeField] private AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeToSpendRolling;
    [SerializeField] private float damage;
    [Header("Raycast Values")]
    [SerializeField] private float maxRaycastDistance;

    private float elapsedTime = 0f;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private NavMeshAgent enemy;
    private Vector3 direction;

    Vector3 targetSpeed;

    StateMachine machine;

    #endregion

    #region Constructor
    public RollingEnemyRollingState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Methods

    public override void Start()
    {
        stateGameObject.GetComponent<Animator>().SetBool("walking",true);
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = 0;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        direction = stateGameObject.transform.forward;
        machine = stateGameObject.GetComponent<StateMachine>();
    }

    public override void Update()
    {
        RaycastHit hit;

        float sphereCastRadius = stateGameObject.GetComponent<BoxCollider>().bounds.extents.x;

        if (Physics.SphereCast(stateGameObject.transform.position, sphereCastRadius, direction, out hit, maxRaycastDistance))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(stateGameObject.transform.position, direction * maxRaycastDistance, Color.red);

                Vector3 normal = hit.normal;

                direction = Vector3.Reflect(direction, normal).normalized;
                direction.y = 0.5f;
                float angle = Vector3.SignedAngle(stateGameObject.transform.forward, direction, Vector3.up);
                Quaternion quat = Quaternion.Euler(0, angle, 0);
                stateGameObject.transform.rotation *= quat;
                targetSpeed = quat * targetSpeed;
            }
            else
            {
                //hacer que mate al player. preferiblemente oneshot
                Vector3 normal = hit.normal;
                if (hit.collider.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
                {
                    healthBehaviour.Damage(damage);
                }
                direction = Vector3.Reflect(direction, normal).normalized;
                direction.y = 0.5f;
                float angle = Vector3.SignedAngle(stateGameObject.transform.forward, direction, Vector3.up);
                Quaternion quat = Quaternion.Euler(0, angle, 0);
                stateGameObject.transform.rotation *= quat;
                targetSpeed = quat * targetSpeed;
            }
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime < timeToSpendRolling)
        {
            float t = Mathf.Clamp01(elapsedTime / timeToSpendRolling);
            float curveValue = curveToMaxAcceleration.Evaluate(t);
            float targetAcceleration = curveValue * maxRollingSpeed;
            targetSpeed += stateGameObject.transform.forward * targetAcceleration * Time.deltaTime;

            // Aplicar la velocidad mínima
            if (targetSpeed.magnitude < startSpeed)
            {
                targetSpeed = targetSpeed.normalized * startSpeed;
            }
            targetSpeed.y = 0.5f;

            rigidBody.velocity = targetSpeed;
        }
        else
        {
            stateGameObject.GetComponent<RollingEnemyReferences>().SetIsStunned(true);
        }
    }

    public override void OnExitState()
    {
        elapsedTime = 0;
        stateGameObject.GetComponent<Animator>().SetBool("walking", false);
    }

    #endregion
}
