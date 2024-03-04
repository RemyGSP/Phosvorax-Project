using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "EnemyStates/EnemyRollingState")]
public class EnemyRollingState : States
{

    #region Constructor
    public EnemyRollingState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables

    [Header("MoveValues")]
    [SerializeField] private float currentRollingSpeed;
    [SerializeField] private float maxRollingSpeed;
    [SerializeField] AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeToSpendRolling;

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

        StartRolling();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public void StartRolling()
    {
        //AÑADIR ANIMATOR PARA QUE CAMBIE A HACERSE LA BOLA

        Vector3 direction = PlayerReferences.instance.GetPlayerCoordinates();
        rigidBody.velocity = 
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }


    #endregion



}


