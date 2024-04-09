using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerBasicAttackState")]
public class BasicAttackState : States
{

    
    [SerializeField] private float attackOffset; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float heightOffset;
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float attackDamage;
    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private float impulseForce;
    [SerializeField] private float damageDelay;
    [SerializeField] private float slashMeshDelay;
    [SerializeField] private float consecutiveSlashes;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    private AnimationClip punchClip;

    const string attackAnimationClipName = "ActionPunch";
    private float inAttackStateTimer;
    private bool damageChek;
    private bool meshChek;



    public BasicAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newGameState = null;
        if (inAttackStateTimer > punchClip.length){
            newGameState = base.CheckTransitions();
        }
        
        
        return newGameState;
    }
    public override void Start(){
        InitializeComponents();
        PerforingAttack();
    }

    private void PerforingAttack()
    {
        inAttackStateTimer = 0;
        damageChek = false;
        meshChek = false;
        RotatePlayerTowardsMouseTarget();
        AddImpulseForce();
        ExecuteAnimation();
        
    }

    private void InitializeComponents()
    {   
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        anim = PlayerReferences.instance.GetPlayerAnimator();
        punchClip = CommonUtilities.FindAnimation(anim, attackAnimationClipName);
    }


    private void RotatePlayerTowardsMouseTarget()
    {
        if (!PlayerInputController.Instance.isGamepad){
            Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;
            stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        }
    
    }

    private void AddImpulseForce(){
        rigidBody.AddForce(stateGameObject.transform.forward * impulseForce, ForceMode.VelocityChange);
    }

    private void ExecuteAttack(){
        Vector3 playerForward = stateGameObject.transform.forward;
        Vector3 attackPosition = stateGameObject.transform.position + playerForward * attackOffset + Vector3.up * heightOffset;
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, sphereSize / 2, hitLayerMask, QueryTriggerInteraction.UseGlobal);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                healthBehaviour.Damage(attackDamage);
            }
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
    }

    public override void FixedUpdate()
    {
        inAttackStateTimer += Time.deltaTime; 
        if (inAttackStateTimer > slashMeshDelay && !meshChek)
        {
            meshChek = true;
            GenerateAttackSlash();
        }
        if (inAttackStateTimer > damageDelay && !damageChek)
        {
            damageChek = true;
            ExecuteAttack();
        }
    }
    public override void Update()
    {
        return;
    }

    public override void OnExitState()
    {
        PlayerTimers.Instance.playerBasicAttackTimer = 0;
        rigidBody.velocity = Vector3.zero;
        Debug.Log(punchClip.length);

    }
}
