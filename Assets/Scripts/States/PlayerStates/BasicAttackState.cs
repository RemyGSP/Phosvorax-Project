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
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    private float animationLength;
    private float inAttackStateTimer;
    
    public BasicAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newGameState = null;
        if (inAttackStateTimer > animationLength){
            newGameState = base.CheckTransitions();
        }
        
        
        return newGameState;
    }
    public override void Start(){
        InitializeComponents();
        RotatePlayerTowardsMouseTarget();
        AddImpulseForce();
        GenerateAttackSlash();
        ExecuteAnimation();
        ExecuteAttack();
    }

    private void InitializeComponents()
    {   
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        anim = PlayerReferences.instance.GetPlayerAnimator();
    }

    
    private void RotatePlayerTowardsMouseTarget()
    {

        if (!PlayerInputController.Instance.isGamepad){
            Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;
            stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        }
    
    }

    private void AddImpulseForce(){
        rigidBody.AddForce(stateGameObject.transform.forward * impulseForce, ForceMode.Impulse);
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
        animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
    }

    public override void FixedUpdate()
    {
        inAttackStateTimer += Time.deltaTime;   
    }
    public override void Update()
    {
        return;
    }

    public override void OnExitState()
    {
        PlayerTimers.Instance.playerBasicAttackTimer = 0;
        
    }
}
