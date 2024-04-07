using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerBasicAttackState")]
public class BasicAttackState : States
{
    [SerializeField] LayerMask enemyLayerMask;
    private Rigidbody rigidBody;
    [SerializeField] private float attackDelay; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float heightOffset;
    [SerializeField] private float attackDamage;
    [SerializeField] private float impulse;
    private RotateCharacter rotateCharacter;
    private Animator anim;
    private float animationLength;
    private float currentAttackTime;
    private bool canAttack;
    private float currentAttackDelay;
    private Transform playerTransform;

    

    public BasicAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public new States CheckTransitions()
    {
        Debug.Log("Basic Attack CurrentAttackTime: " + currentAttackTime + "AnimationLenght: " + animationLength);
        if (currentAttackTime > animationLength)
        {
            return base.CheckTransitions();
        }
        else
        {
            return null;
        }
        
    }

    public override void Start()
{
    InitializeComponents();
    RotatePlayerTowardsMouseTarget();
    GenerateAttackSlash();
    ExecuteAnimation();
}

private void InitializeComponents()
{
    PlayerTimers.Instance.playerBasicAttackTimer = 0;
    anim = PlayerReferences.instance.GetPlayerAnimator();
    rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
    rigidBody = stateGameObject.GetComponent<Rigidbody>();
    playerTransform = stateGameObject.GetComponent<Transform>();
    //rigidBody.AddForce(stateGameObject.transform.rotation * Vector3.forward * impulse, ForceMode.Impulse);
    currentAttackDelay = attackDelay;
}

private void RotatePlayerTowardsMouseTarget()
{

    if (!PlayerInputController.Instance.isGamepad){
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;
        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
    }
    
}

private void GenerateAttackSlash()
{
    stateGameObject.GetComponent<SlashGenerator>().GenerateSlash();
}

private void ExecuteAnimation()
{
    AudioManager.Instance.CallOneShot("event:/SlashSound");
    anim.SetTrigger("attack");
    currentAttackTime = 0;
    animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
    currentAttackDelay = attackDelay;
    canAttack = true;
    canAttack = false;//-----
    ExecuteAttack();
}

    void ExecuteAttack()
    {
        Debug.Log("Attack");
        //stateGameObject.GetComponent<>().OnShoot();
        Vector3 playerForward = playerTransform.forward;
        Vector3 attackPosition = playerTransform.position + playerForward * attackOffset + Vector3.up * heightOffset;
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, sphereSize / 2, enemyLayerMask, QueryTriggerInteraction.UseGlobal);
        foreach (Collider hitCollider in hitColliders)
        {
            rigidBody.velocity = Vector3.zero;
            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                healthBehaviour.Damage(attackDamage);
            }
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
        canAttack = false;
        rigidBody.velocity = Vector3.zero;
    }

    public override void FixedUpdate()
    {
        currentAttackDelay -= Time.deltaTime;
        currentAttackTime += Time.deltaTime;
        if (currentAttackDelay <= 0 && canAttack)
        {
            ExecuteAttack();
            PlayerTimers.Instance.playerBasicAttackTimer = 0;
        }

    }

    public override void Update()
    {
        rigidBody.velocity = Vector3.zero;
    }

    public new void OnEnterState()
    {
        base.OnEnterState();

    }

    public override void OnExitState()
    {
        rigidBody.velocity = Vector3.zero;
        //PlayerReferences.instance.GetPlayerAnimator().SetInteger("meleeAttack", 0);
    }
}
