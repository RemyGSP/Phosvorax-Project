using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "EnemyStates/RangedEnemyAttackState")]
public class RangedEnemyAttackState : States
{

    #region Constructor
    public RangedEnemyAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [SerializeField] LayerMask playerLayerMask;

    private RotateCharacter rotateCharacter;
    private LaserGenerator laserGenerator;
    private Rigidbody rigidBody;
    private Animator anim;

    [Header("Values")]
    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackDamage;
    [SerializeField] private float timeToSpendAttacking;

    //controlar que el daño vaya con la animacion
    float animationLength;
    bool canAttack;
    private float elapsedTime;
    [SerializeField] private float maxAttackDistance = 5f;

    private NavMeshAgent enemy;

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
        laserGenerator = stateGameObject.GetComponent<LaserGenerator>();
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = 0;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        canAttack = true;
        ExecuteAnim();
    }

    private void ExecuteAnim()
    {
        //anim.SetTrigger("attack");
        //animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
        canAttack = true;

    }

    void ExecuteAttack()
    {


        Vector3 attackDirection = stateGameObject.transform.forward;

        if (!laserGenerator.GetLaserState())
        {
            laserGenerator.ActivateLaser();
        }

        RaycastHit hit;
        if (Physics.Raycast(stateGameObject.transform.position, attackDirection, out hit, maxAttackDistance, playerLayerMask))
        {
            // Si el rayo golpea un objeto en la capa del jugador, aplica daño
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<HealthBehaviour>().Damage(attackDamage);
            }

            Debug.Log("Impacto con: " + hit.collider.gameObject.name);
        }
    }


    public override void Update()
    {
        stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position, 0.5f);

        elapsedTime += Time.deltaTime;

        if (canAttack)
        {
            if(elapsedTime < timeToSpendAttacking)
            {
                ExecuteAttack();
            }
            else
            {
                canAttack = false;
                stateGameObject.GetComponent<RangedEnemyReferences>().SetCanAttack(canAttack);
                stateGameObject.GetComponent<RangedEnemyReferences>().SetCanMoveAway(true);
                stateGameObject.GetComponent<RangedEnemyReferences>().SetCanChase(true);
            }
        }
    }

    public override void OnExitState()
    {
        canAttack = true;
        stateGameObject.GetComponent<LaserGenerator>().DeactivateLaser();
        stateGameObject.GetComponent<RangedEnemyReferences>().SetCanChase(false);
    }

    #endregion
}

