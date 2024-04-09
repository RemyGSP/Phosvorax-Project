using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [Header("Layers")]
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] LayerMask groundLayerMask;

    LayerMask combinedLayers;

    private RotateCharacter rotateCharacter;
    private LaserGenerator laserGenerator;
    private Rigidbody rigidBody;
    private Animator anim;

    //LaserValues
    private float laserOffValue = 2.5f;
    private float laserOnMaxValue = 0f;
    private float laserAuxValue;




    [Header("Values")]
    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackDamage;
    [SerializeField] private float timeToSpendAttacking;
    [SerializeField] private float lerpSpeed = 0.5f;
    [SerializeField] private float lineDrawUnitsPerMeter = 0.375f;

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
        combinedLayers = playerLayerMask | groundLayerMask;
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
        Vector3 finalPoint = laserGenerator.getFinalPointCoordinates();
        Vector3 initialPoint = laserGenerator.getInitialPointCoordinates();

        float currentLaserLength = (finalPoint - initialPoint).magnitude;

        if (!laserGenerator.GetLaserState())
        {
            laserGenerator.ActivateLaser();
        }

        RaycastHit hit;
        Vector3 fullRayVector = (finalPoint - initialPoint);
        float laserDistanceValue = -1; // Valor predeterminado en caso de no colisión

        if (Physics.Raycast(initialPoint, fullRayVector.normalized, out hit, currentLaserLength, combinedLayers, QueryTriggerInteraction.Ignore))
        {
            Vector3 hitPosition = hit.point;

            // Si el rayo golpea un objeto en la capa del jugador, aplica daño
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<HealthBehaviour>().Damage(attackDamage);
            }
            else
            {
                float distanceFromFinalPointToCollision = (initialPoint - hitPosition).magnitude;
                float newcurrentLaserLength = Mathf.Abs(distanceFromFinalPointToCollision);
                laserDistanceValue = newcurrentLaserLength * lineDrawUnitsPerMeter;
            }

            Debug.Log("Impacto con: " + hit.collider.gameObject.name);
            Debug.DrawLine(initialPoint, hitPosition, Color.red);
        }
        else
        {
            // Si no hay colisión, actualiza el valor predeterminado del láser
            laserDistanceValue = currentLaserLength * lineDrawUnitsPerMeter;
        }

        // Actualiza la distancia del láser
        laserGenerator.SetLineRenderFinalDistance(laserDistanceValue);
    }


    public override void Update()
    {
        Quaternion desriedRotation = (rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position, 0.5f));
        stateGameObject.transform.rotation = Quaternion.Lerp(stateGameObject.transform.rotation, desriedRotation, Time.deltaTime * lerpSpeed);


        elapsedTime += Time.deltaTime;

        if (canAttack)
        {
            if (elapsedTime < timeToSpendAttacking)
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

