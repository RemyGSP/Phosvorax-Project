using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyAttackState")]

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

    private Rigidbody rigidBody;
    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tamaño del área de detección

    private float currentAttackDelay;

    [Header("Melee Enemy Timers")]
    public float enemyMeleeAttackTimer;
    public float enemyMeleeAttackCD;

    [SerializeField] private AttackAreaVisualizer attackAreaVisualizer;

    [SerializeField] private GameObject playerRefrences;
    #endregion

    public override States CheckTransitions()
    {
        States newGameState = null;
        if (enemyMeleeAttackTimer < enemyMeleeAttackCD)
        {
            if (PlayerInputController.GetPlayerInputDirection() != Vector3.zero) //cambiar a si el jugador esta mas lejos del rango maximo del ataque
            {
                newGameState = Instantiate(EnemyChaseState);
            }
            if (PlayerInputController.IsRolling() && PlayerTimers.timer.rollTimer > PlayerTimers.timer.rollCD) //cambiar a que si la vida llega a 0
            {
                //newGameState = Instantiate(EnemyDieState);
            }
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

    #region Methods

    public override void Start()
    {
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        currentAttackDelay = attackDelay;

        attackAreaVisualizer = stateGameObject.GetComponent<AttackAreaVisualizer>();
        if (attackAreaVisualizer == null)
        {
            Debug.LogError("No se encontró el componente AttackAreaVisualizer en el jugador.");
            return;
        }

        //attackAreaVisualizer.attackOffset = attackOffset;
        //attackAreaVisualizer.sphereSize = sphereSize;
    }

    private void ExecuteAttack()
    {
        Vector3 playerPosition = playerRefrences.GetComponent<PlayerReferences>().GetPlayerCoordinates();

    }


    public override void FixedUpdate()
    {
        currentAttackDelay -= Time.deltaTime;

        if (currentAttackDelay <= 0)
        {
            ExecuteAttack();
            currentAttackDelay = attackDelay;
            enemyMeleeAttackTimer = 0;
        }

    }

    public override void Update()
    {
        enemyMeleeAttackTimer += Time.deltaTime;
    }

    #endregion
}




