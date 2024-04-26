using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "BossStates/BossLaserRotatingAttackState")]

public class BossLaserRotationAttackState : States
{

    #region Constructor
    public BossLaserRotationAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    [SerializeField] LayerMask playerLayerMask;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;

    private bool hasFinishedAttack;
    [Header("Rotation")]
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationAxis = Vector3.left;

    [Header("Move Values")]
    [SerializeField] float chaseSpeed;

    [Header("Time")]
    [SerializeField] float attackTime;
    private float currentTime;
    private NavMeshAgent enemy;

    private BossLasersSpawner lasersSpawner;
    #endregion

    #region Methods

    public override States CheckTransitions()
    {
        return base.CheckTransitions();
    }

    public override void Start()
    {
        lasersSpawner = stateGameObject.GetComponent<BossLasersSpawner>();
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        stateGameObject.GetComponent<BossTimers>().abilityTimers[1] = 0;
        stateGameObject.GetComponent<GetBestAbilityToUse>().ResetArrays();
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        hasFinishedAttack = false;
        enemy.speed = 1;
        base.Start();
        currentTime = 0;

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();

        if (currentTime <= attackTime)
        {
         
            if(!lasersSpawner.isLaser1Activated())
            {
                if (!lasersSpawner.isLaser2Activated())
                {
                    lasersSpawner.ActivateLasers();
                }
            }
            rigidBody.mass = Mathf.Infinity;
            // Movimiento de rotación mientras el ataque está en progreso
            stateGameObject.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);

            // Calcular la dirección hacia el jugador
            Vector3 directionToPlayer = (playerPos - stateGameObject.transform.position).normalized;

            // Mover el enemigo en la dirección del jugador
            stateGameObject.transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
        }
        else
        {
            stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
            hasFinishedAttack = true;
           
        }
    }

    public override void OnExitState()
    {
        lasersSpawner.DeactivateLasers();
        rigidBody.mass = 2000f;
        stateGameObject.GetComponent<BossReferences>().ResetCurrentTime();
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
        stateGameObject.GetComponent<BossReferences>().SetCanUseAbility(false);
        base.OnExitState();
    }
    #endregion
}


