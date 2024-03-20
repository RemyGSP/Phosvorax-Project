using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/RangedEnemyAttackState")]
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
        enemy.speed = 0;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();

    }

 


    public override void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime < timeToRunAway)
        {
            Vector3 playerPosition = PlayerReferences.instance.GetPlayerCoordinates();

            Vector3 directionToPlayer = playerPosition - stateGameObject.transform.position;
            directionToPlayer.Normalize();

            // Obtener la posición más alejada de la nav mesh en la dirección opuesta al jugador
            Vector3 oppositeDirection = -directionToPlayer;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(stateGameObject.transform.position + oppositeDirection * 10f, out hit, 10f, NavMesh.AllAreas))
            {
                // Mover al enemigo hacia el punto más alejado de la nav mesh
                enemy.SetDestination(hit.position);
                //enemy.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, hit.position - stateGameObject.transform.position, 0.5f);
                //Vector3 oppositeLookDirection = -(hit.position - stateGameObject.transform.position);
                //enemy.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, oppositeLookDirection - stateGameObject.transform.position, 0.5f); // si queremos que el enmigo esté andando hacia atras
            }
        }
    }

    public override void OnExitState()
    {
    }

    #endregion
}

