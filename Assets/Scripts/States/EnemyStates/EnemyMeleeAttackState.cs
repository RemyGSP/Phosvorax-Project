using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyMeleeAttackState")]

public class EnemyMeleeAttackState : States
{
    [Header("States")]
    [SerializeField] private States EnemyChaseState;
    [SerializeField] private States EnemyDieState;

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
    #endregion

    #region AbstractMethods
    public override States CheckTransitions()
    {
        float distance = Vector3.Distance(PlayerReferences.instance.GetPlayerCoordinates(), stateGameObject.transform.position);

        States newGameState = null;

        if (distance > maxAttackDistance) //cambiar a perseeguir si el jugador esta mas lejos del rango maximo del ataque
        {
            newGameState = Instantiate(EnemyChaseState);
        }
        if (stateGameObject.GetComponent<HealthBehaviour>().CheckIfDeath()) //Si el enemigo muere
        {
            newGameState = Instantiate(EnemyDieState);
        }

        if (newGameState != null)
        {
            newGameState.InitializeState(stateGameObject);
            newGameState.Start();
            rigidBody.velocity = Vector3.zero;
            //animator.SetBool("running",false);
        }



        return newGameState;

    }

    public override void InitializeState(GameObject gameObject)
    {
        stateGameObject = gameObject;
    }
    #endregion

    #region Methods

    void Start()
    {
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        anim = stateGameObject.GetComponent<Animator>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        currentAttackDelay = attackDelay;

        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position);
        if (stateGameObject.TryGetComponent<AttackAreaVisualizer>(out AttackAreaVisualizer attAreaVisual))
        {
            attAreaVisual.DrawAttackArea(400f, 400f);
        }



        ExecuteAnim();
    }

    private void ExecuteAnim()
    {
        anim.SetTrigger("attack");
        currentAttackTime = 0;
        animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
        //stateGameObject.transform.rotation = Quaternion.Euler(stateGameObject.transform.rotation.x , stateGameObject.transform.rotation.y + animOffsetRotation, stateGameObject.transform.rotation.z );
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
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
        canAttack = false;
    }


    public override void FixedUpdate()
    {
        currentAttackDelay -= Time.deltaTime;
        currentAttackTime += Time.deltaTime;
        if (currentAttackDelay <= 0 && canAttack)
        {
            ExecuteAttack();
            PlayerTimers.timer.playerBasicAttackTimer = 0;
        }

    }

    public override void Update()
    {
        enemyMeleeAttackTimer += Time.deltaTime;
    }

    #endregion
}




