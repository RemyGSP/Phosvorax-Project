using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyChaseState")]
public class EnemyMeleeChaseState : States
{
    [Header("States")]
    [SerializeField] private States EnemyAttackState;
    [SerializeField] private States EnemyDieState;
    [SerializeField] private States EnemyIdleState;

    #region Constructor
    public EnemyMeleeChaseState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [Header("MoveValues")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] float timeToMaxVelocity;
    [SerializeField] AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;


    [SerializeField] private float maxAttackDistance = 5f;
    [SerializeField] private float distanceToSeePlayer = 20f;
    float currentMovementStatusTimer;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;

    #endregion

    public override States CheckTransitions()
    {
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), stateGameObject.transform.position);
        States newGameState = null;
       
         if (distance <= maxAttackDistance) 
         {
             newGameState = Instantiate(EnemyAttackState);
         }
         if (stateGameObject.GetComponent<HealthBehaviour>().CheckIfDeath()) //si el enemigo muere
         {
            newGameState = Instantiate(EnemyDieState);
         }
        if (distance > distanceToSeePlayer)
        {
            newGameState = Instantiate(EnemyIdleState);
        }
        if (newGameState != null)
        {
            newGameState.InitializeState(stateGameObject);
            newGameState.Start();
            rigidBody.velocity = Vector3.zero;
            //animator.SetBool("running",false);
        }
        return newGameState;


    }

    #region Methods

    public override void Start()
    {
        currentSpeed = 0;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
    }

    public override void Update()
    {
        Vector3 playerPosition = PlayerReferences.instance.GetPlayerCoordinates();
        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(playerPosition);
        rigidBody.velocity = Move(playerPosition);

    }

    private Vector3 Move(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - stateGameObject.transform.position).normalized;

        if (currentSpeed < maxSpeed)
        {
            currentSpeed = maxSpeed * curveToMaxAcceleration.Evaluate(currentMovementStatusTimer / timeToMaxVelocity);
        }
        currentMovementStatusTimer += Time.deltaTime;

        return direction * currentSpeed;
    }

    #endregion
}
