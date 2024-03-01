using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EnemyChaseState")]
public class EnemyMeleeChaseState : States
{

    #region Constructor
    public EnemyMeleeChaseState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [Header("MoveValues")]
    [SerializeField] private float enemyNavMeshSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] float timeToMaxVelocity;
    [SerializeField] AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;

    float currentMovementStatusTimer;
    private bool playerSeen = false;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    private NavMeshAgent enemy;
    #endregion


    #region Methods

    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = enemyNavMeshSpeed;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
    }

    public override void Update()
    {
        RaycastHit hit;
        Vector3 playerVisiblePosition = PlayerReferences.instance.GetPlayerVisiblePoint(); //Visible point es un empty game object dentro del player que sirve para que el linedraw vaya hacia esa posicion porque con el pivote del player se va al suelo y nunca colisiona con el jugador
        Vector3 playerPosition = PlayerReferences.instance.GetPlayerCoordinates();
        if (Physics.Linecast(stateGameObject.transform.position, playerVisiblePosition, out hit))
        {
            Debug.DrawLine(stateGameObject.transform.position, playerPosition, Color.magenta, 0.2f);

            if (hit.transform != null && hit.transform.position.z == PlayerReferences.instance.GetPlayerCoordinates().z && hit.transform.position.x == PlayerReferences.instance.GetPlayerCoordinates().x)
            {
                playerSeen = true;
                stateGameObject.GetComponent<EnemyReferences>().SetPlayerSeen(playerSeen);
                stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, playerPosition - stateGameObject.transform.position, 0.5f);
                enemy.SetDestination(playerPosition);
            }
            else
            {
                playerSeen = false;
                stateGameObject.GetComponent<EnemyReferences>().SetPlayerSeen(playerSeen);
            }
        }
    }

    public override void OnExitState()
    {
        return;
    }
    #endregion
}
