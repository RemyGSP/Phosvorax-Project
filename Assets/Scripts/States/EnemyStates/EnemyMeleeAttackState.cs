using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EnemyMeleeAttackState")]

public class EnemyMeleeAttackState : States
{

    #region Constructor
    public EnemyMeleeAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [SerializeField] LayerMask playerLayerMask;

    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float attackDamage;

    //controlar que el daño vaya con la animacion
    float animationLength;
    float currentAttackTime;
    bool canAttack;
    private float currentAttackDelay;

    [SerializeField] private float maxAttackDistance = 5f;

    [Header("Melee Enemy Timers")]
    public float enemyMeleeAttackTimer;
    public float enemyMeleeAttackCD;

    [SerializeField] private AttackAreaVisualizer attackAreaVisualizer;

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
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        enemy.speed = 0;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        currentAttackDelay = attackDelay;

        



        ExecuteAnim();
    }

    private void ExecuteAnim()
    {
        //anim.SetTrigger("attack");
        currentAttackTime = 0;
        //animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position);
        currentAttackDelay = attackDelay;
        canAttack = true;

    }

    void ExecuteAttack()
    {


        Vector3 attackPosition = stateGameObject.transform.position + stateGameObject.transform.forward * attackOffset;
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, sphereSize / 2, playerLayerMask, QueryTriggerInteraction.UseGlobal);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                healthBehaviour.Damage(attackDamage);
            }
        }
        canAttack = false;
    }


    public override void FixedUpdate()
    {
        currentAttackDelay -= Time.deltaTime;
        currentAttackTime += Time.deltaTime;

        if (currentAttackDelay <= 0)
        {
            if (canAttack)
            {
                ExecuteAttack();
                PlayerTimers.Instance.playerBasicAttackTimer = 0;
            }
            currentAttackDelay = attackDelay;
            canAttack = true;
        }
    }
    public override void Update()
    {
        stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position, 0.5f);
        enemyMeleeAttackTimer += Time.deltaTime;
    }

    public override void OnExitState()
    {
        return;
    }

    #endregion
}




