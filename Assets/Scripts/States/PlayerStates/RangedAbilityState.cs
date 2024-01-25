using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName ="States/FirstAbilityState")]
public class RangedAbilityState : Ability
{
    [SerializeField] LayerMask enemyLayerMask;
    private Rigidbody rigidBody;
    [SerializeField] float animOffsetRotation;
    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float attackDamage;
    RotateCharacter rotateCharacter;
    Animator anim;
    float animationLength;
    float currentAttackTime;
    Vector3 attackPosition;
    bool canAttack;
    private float currentAttackDelay;
    private Transform playerTransform;
    [SerializeField] GameObject abilityFeedback;
    GameObject currentFeedback;

    [SerializeField] private AttackAreaVisualizer attackAreaVisualizer;
    public RangedAbilityState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newGameState = null;

        if (currentAttackTime > animationLength)
        {
            bool notChanged = true;
            int counter = 0;

            while (notChanged)
            {
                newGameState = stateTransitions[counter].GetExitState(stateGameObject.GetComponent<StateMachine>());
                if (newGameState != null)
                {
                    notChanged = false;
                    newGameState.InitializeState(stateGameObject);
                    newGameState.Start();
                    rigidBody.velocity = Vector3.zero;
                    Destroy(currentFeedback);
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
        }
        return newGameState;
        
    }

    public override void Start()
    {
        AbilityManager.instance.CastedAbility();
        anim = stateGameObject.GetComponent<Animator>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();

        //Lo que tardara en ejecutarse el ataque comparado con la animacion
        currentAttackDelay = attackDelay;
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir();

        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        if (stateGameObject.TryGetComponent<AttackAreaVisualizer>(out AttackAreaVisualizer attAreaVisual))
        {
            attackPosition =attAreaVisual.GetCursorPositionInsideBounds(PlayerReferences.instance.GetMouseTargetDir() - PlayerReferences.instance.GetPlayerCoordinates());
        }

        ExecuteAnim();
    }

    private void ExecuteAnim()
    {
        anim.SetTrigger("attack");
        currentFeedback = Instantiate(abilityFeedback, attackPosition,Quaternion.identity);
        
        currentAttackTime = 0;
        animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
        //stateGameObject.transform.rotation = Quaternion.Euler(stateGameObject.transform.rotation.x , stateGameObject.transform.rotation.y + animOffsetRotation, stateGameObject.transform.rotation.z );
        currentAttackDelay = attackDelay;
        canAttack = true;
    }

    void ExecuteAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, sphereSize / 2, enemyLayerMask, QueryTriggerInteraction.UseGlobal);

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
        return;
    }

}
