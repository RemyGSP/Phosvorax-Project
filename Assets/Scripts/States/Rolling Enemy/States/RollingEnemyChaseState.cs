using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "EnemyStates/RollingEnemyChaseState")]
public class RollingEnemyChaseState : States
{

    #region Constructor
    public RollingEnemyChaseState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [Header("MoveValues")]
    [SerializeField] private float enemyNavMeshSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] float timeToMaxVelocity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask playerLayerMask;

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
        stateGameObject.GetComponent<Animator>().SetBool("walking", true);
    }

    public override void Update()
    {

        if (!enemy.isOnNavMesh)
        {
            enemy.enabled = false;
            enemy.enabled = true;
            Debug.Log(enemy.isOnNavMesh);
        }

        RaycastHit hit;
        Vector3 playerVisiblePosition = PlayerReferences.instance.GetPlayerVisiblePoint(); //Visible point es un empty game object dentro del player que sirve para que el linedraw vaya hacia esa posicion porque con el pivote del player se va al suelo y nunca colisiona con el jugador
        Vector3 playerPosition = PlayerReferences.instance.GetPlayerCoordinates();
        Vector3 vecToPlayer = playerVisiblePosition - stateGameObject.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(playerPosition - stateGameObject.transform.position, Vector3.up);
        targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);

        stateGameObject.transform.rotation = Quaternion.Slerp(stateGameObject.transform.rotation, targetRotation, 0.5f);

        enemy.SetDestination(playerPosition);
    }

    public override void OnExitState()
    {
        enemy.speed = 0;
        playerSeen = false;
        stateGameObject.GetComponent<Animator>().SetBool("walking", false);
        return;
    }
    #endregion
}
