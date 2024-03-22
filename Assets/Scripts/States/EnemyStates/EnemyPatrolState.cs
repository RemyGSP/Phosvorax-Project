using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EnemyPatrolState")]
public class EnemyPatrolState : States
{

    #region Constructor
    public EnemyPatrolState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables

    [Header("Patrol Values")]
    [SerializeField] private float patrolAreaRange; //radius of sphere

    private Transform patrolCenterPoint; //centre of the area the agent wants to move around in



    private bool playerSeen = false;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    private UnityEngine.AI.NavMeshAgent enemy;
    #endregion


    #region Methods

    public override void Start()
    {
        Debug.Log(stateGameObject);
        patrolCenterPoint = stateGameObject.transform;
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        Debug.Log(enemy);
    }

    public override void Update()
    {
        Debug.Log("IsOnNavMesh " + enemy.isOnNavMesh);
        if (enemy.remainingDistance <= enemy.stoppingDistance) //ha acabado de hacer el camino
        {
            Vector3 point;
            if (RandomPoint(patrolCenterPoint.position, patrolAreaRange, out point))
            {
                //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //para ver el punto 
                enemy.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //punto random
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public override void OnExitState()
    {
        return;
    }
    #endregion
}

