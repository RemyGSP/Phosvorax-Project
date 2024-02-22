using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerBasicAttackState")]
public class BasicAttackState : States
{
    [SerializeField] LayerMask enemyLayerMask;
    private Rigidbody rigidBody;
    [SerializeField] private float animOffsetRotation;

    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float attackDamage;
    private RotateCharacter rotateCharacter;
    private Animator anim;
    private float animationLength;
    private float currentAttackTime;
    private bool canAttack;
    private float currentAttackDelay;
    private Transform playerTransform;

    [SerializeField] private AttackAreaVisualizer attackAreaVisualizer;

    

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
        anim = PlayerReferences.instance.GetPlayerAnimator() ;
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();

        //Lo que tardara en ejecutarse el ataque comparado con la animacion
        currentAttackDelay = attackDelay;
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;

        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        stateGameObject.GetComponent<SlashGenerator>().GenerateSlash();

        //PlayerReferences.instance.GetPlayerAnimator().SetBool("meleeAttack", true);
        ExecuteAnim();
    }

    private void ExecuteAnim()
    {
        anim.SetTrigger("attack");
        currentAttackTime = 0;
        animationLength = anim.GetCurrentAnimatorClipInfo(0).Length ;
        //stateGameObject.transform.rotation = Quaternion.Euler(stateGameObject.transform.rotation.x , stateGameObject.transform.rotation.y + animOffsetRotation, stateGameObject.transform.rotation.z );
        currentAttackDelay = attackDelay;
        canAttack = true;
    }

    void ExecuteAttack()
    {

        //stateGameObject.GetComponent<GroundSlashShooter>().OnShoot();
        Vector3 attackPosition = playerTransform.position + playerTransform.forward * attackOffset;
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
            PlayerTimers.Instance.playerBasicAttackTimer = 0;
        }

    }

    public override void Update()
    {
        return;
    }

    public new void OnEnterState()
    {
        base.OnEnterState();

    }

    public override void OnExitState()
    {

        //PlayerReferences.instance.GetPlayerAnimator().SetInteger("meleeAttack", 0);
    }
}
