using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EnemyRangedAttackState")]

public class EnemyRangedAttackState : States
{

    #region Constructor
    public EnemyRangedAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
 
    [SerializeField] LayerMask playerLayerMask;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    //controlar que el daño vaya con la animacion
    float animationLength;
    float currentAttackTime;
    bool canAttack;
    private float currentAttackDelay;

    [SerializeField] private float maxAttackDistance = 5f;

    [Header("Ranged Enemy Timers")]
    [SerializeField] private float enemyRangedAttackTimer;
    [SerializeField] private float enemyRangedAttackCD;
    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;

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

        if (stateGameObject.TryGetComponent<AttackAreaVisualizer>(out AttackAreaVisualizer attAreaVisual))
        {
            attAreaVisual.DrawAttackArea(400f, 400f);
        }



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
 
        Vector3 directionToPlayer = (PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position).normalized;

        // Instancia el objeto de bala en la posición del enemigo
        GameObject bulletInstance = Instantiate(bullet, stateGameObject.transform.position, Quaternion.identity);

        // Obtén el componente Rigidbody de la bala
        Rigidbody bulletRigidbody = bulletInstance.GetComponent<Rigidbody>();

        // Si la bala tiene un componente Rigidbody
        if (bulletRigidbody != null)
        {
            // Aplica fuerza a la bala en la dirección hacia el jugador
            bulletRigidbody.velocity = directionToPlayer * bulletSpeed; // Ajusta 'bulletSpeed' según lo que necesites
        }
        else
        {
            Debug.LogError("El objeto de bala no tiene un componente Rigidbody.");
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
        enemyRangedAttackTimer += Time.deltaTime;
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}





