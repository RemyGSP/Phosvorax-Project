using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerBasicAttackState")]
public class BasicAttackState : States
{

    
    [SerializeField] private float attackOffset; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float heightOffset;
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float sphereHeight; // Tamaño del área de detección
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

    const string attackAnimationClipName = "ActionPunchFixed";
    private float inAttackStateTimer;
    private float inAttackStateExitTime;
    private bool damageChek;
    private bool meshChek;
    private bool attakChek;
    private int consecutiveSlashesCounter;
    private int attackNumberCounter;



    public BasicAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newGameState = null;
        if (inAttackStateTimer > inAttackStateExitTime)
        {
            newGameState = base.CheckTransitions();
        }
        
        
        return newGameState;
    }
    public override void Start(){
        InitializeComponents();
        PerforingAttack();
        PlayerInputController.Instance.Attacked();
    }

    private void InitializeComponents()
    {
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        anim = PlayerReferences.instance.GetPlayerAnimator();
        punchClip = CommonUtilities.FindAnimation(anim, attackAnimationClipName);
        consecutiveSlashesCounter = 1;
        attackNumberCounter= 0; 
        inAttackStateExitTime = punchClip.length;
        attakChek = false;
    }

    private void PerforingAttack()
    {
        inAttackStateTimer = 0;
        damageChek = false;
        meshChek = false;
        attakChek = true;
        attackNumberCounter++;
        RotatePlayerTowardsMouseTarget();
        AddImpulseForce();
        ExecuteAnimation();
        
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
        float capsuleRadius = sphereSize / 2;

        // Using Physics.OverlapCapsule with your provided sphereHeight
        Collider[] hitColliders = Physics.OverlapCapsule(attackPosition - Vector3.up * (sphereHeight / 2),
                                                         attackPosition + Vector3.up * (sphereHeight / 2),
                                                         capsuleRadius, hitLayerMask, QueryTriggerInteraction.UseGlobal);

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
        if (inAttackStateTimer >= punchClip.length)
        {
            attakChek = false;
        }

        if (inAttackStateTimer >= punchClip.length && consecutiveSlashesCounter > 1 && attackNumberCounter < consecutiveSlashesCounter)
        {
            PerforingAttack();
            inAttackStateExitTime = punchClip.length;
        }

        if (PlayerInputController.Instance.IsAttacking() && consecutiveSlashesCounter < consecutiveSlashes)
        {

            PlayerInputController.Instance.Attacked();
            consecutiveSlashesCounter++;
            inAttackStateExitTime += inAttackStateExitTime;
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
        base.OnExitState();
        PlayerInputController.Instance.Attacked();
    }
}
