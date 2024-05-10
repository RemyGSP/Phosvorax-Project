using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BossStates/BossChaseState")]
public class BossChaseState : States
{

    #region Constructor
    public BossChaseState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [Header("MoveValues")]
    [SerializeField] private float enemyNavMeshSpeed;
    private Rigidbody rigidBody;
    private Animator anim;
    private NavMeshAgent enemy;
    #endregion


    #region Methods



    public override void Start()
    {
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = enemyNavMeshSpeed;
        anim = stateGameObject.GetComponent<Animator>();
        anim.SetBool("Walk", true);
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
    }

    public override void Update()
    {
        Vector3 playerPosition = PlayerReferences.instance.GetPlayerCoordinates();

        Quaternion targetRotation = Quaternion.LookRotation(playerPosition - stateGameObject.transform.position, Vector3.up);
        targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);

        stateGameObject.transform.rotation = Quaternion.Slerp(stateGameObject.transform.rotation, targetRotation, 0.5f);

        enemy.SetDestination(playerPosition);
    }

    public override void OnExitState()
    {
        anim.SetBool("Walk", false);
        rigidBody.velocity = Vector3.zero;
        enemy.speed = 0;
    }
    #endregion
}
