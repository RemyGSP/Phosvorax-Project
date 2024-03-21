using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/RangedEnemyGetAwayState")]
public class GetAwayState : States
{

    #region Constructor
    public GetAwayState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables

    private NavMeshAgent enemy;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;

    [Header("Values")]
    [SerializeField] private float speed;
    [SerializeField] private float timeToRunAway;
    [SerializeField] private float maxDistanceToPlayer;
 
    private float currentTime;
    #endregion

    #region AbstractMethods
    public override States CheckTransitions()
    {
        bool notChanged = true;
        int counter = 0;
        States newEnemyState = null;

        while (notChanged)
        {
            newEnemyState = stateTransitions[counter].GetExitState(stateGameObject.GetComponent<StateMachine>());
            if (newEnemyState != null)
            {
                notChanged = false;
                newEnemyState = Instantiate(newEnemyState);
                newEnemyState.InitializeState(stateGameObject);
                newEnemyState.Start();
            }
            if (counter < stateTransitions.Length - 1)
            {
                counter++;
            }
            else
            {
                notChanged = false;
            }
        }

        return newEnemyState;
    }

    public override void InitializeState(GameObject gameObject)
    {
        stateGameObject = gameObject;
    }
    #endregion

    #region Methods

    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = speed;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();

    }




    public override void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < timeToRunAway)
        {
            Vector3 playerPosition = PlayerReferences.instance.GetPlayerCoordinates();
            Vector3 directionToPlayer = playerPosition - stateGameObject.transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            if (distanceToPlayer < maxDistanceToPlayer)
            {
                directionToPlayer.Normalize();

                Vector3 oppositeDirectionLeft = Quaternion.Euler(0, -45f, 0) * -directionToPlayer;
                Vector3 oppositeDirectionRight = Quaternion.Euler(0, 45f, 0) * -directionToPlayer;

                NavMeshHit hitLeft, hitRight;
                bool canMoveLeft = NavMesh.SamplePosition(stateGameObject.transform.position + oppositeDirectionLeft * 10f, out hitLeft, 10f, NavMesh.AllAreas);
                bool canMoveRight = NavMesh.SamplePosition(stateGameObject.transform.position + oppositeDirectionRight * 10f, out hitRight, 10f, NavMesh.AllAreas);

                if (canMoveLeft && canMoveRight)
                {
                    if ((hitLeft.position - playerPosition).sqrMagnitude > (hitRight.position - playerPosition).sqrMagnitude)
                        enemy.SetDestination(hitLeft.position);
                    else
                        enemy.SetDestination(hitRight.position);
                }
                else if (canMoveLeft)
                {
                    enemy.SetDestination(hitLeft.position);
                }
                else if (canMoveRight)
                {
                    enemy.SetDestination(hitRight.position);
                }
                // Si no podemos movernos hacia ningún lado, el enemigo se quedará en su posición actual
            }
            else
            {
                stateGameObject.GetComponent<RangedEnemyReferences>().SetCanAttack(true);
            }
        }
    }

    public override void OnExitState()
    {
        stateGameObject.GetComponent<RangedEnemyReferences>().SetCanMoveAway(false);
    }

    #endregion
}

