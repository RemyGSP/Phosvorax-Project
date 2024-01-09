using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerBasicAttackState")]
public class BasicAttackState : States
{
    [SerializeField] private States idleState;
    [SerializeField] private States moveState;
    [SerializeField] private States rollState;
    [SerializeField] LayerMask enemyLayerMask;
    private Rigidbody rigidBody;
    [SerializeField] float animOffsetRotation;

    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tamaño del área de detección
    [SerializeField] private float attackDamage;
    Animator anim;
    [SerializeField] float attackTime;
    float currentAttackTime;
    bool canAttack;
    private float currentAttackDelay;
    private Transform playerTransform;

    [SerializeField] private AttackAreaVisualizer attackAreaVisualizer;

    

    public BasicAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newGameState = null;
         if (currentAttackTime > attackTime){
            if (PlayerInputController.GetPlayerInputDirection() != Vector3.zero){
                newGameState = Instantiate(moveState);
            }
            if (PlayerInputController.IsRolling() && Timers.timer.rollTimer > Timers.timer.rollCD){
                newGameState = Instantiate(rollState);
            }
            if (PlayerInputController.GetPlayerInputDirection() == Vector3.zero){
                newGameState = Instantiate(idleState);
            }     
         }
        if (newGameState != null)
        {
            newGameState.InitializeState(stateGameObject);
            newGameState.Start();
            rigidBody.velocity = Vector3.zero;
            stateGameObject.transform.rotation = Quaternion.Euler(stateGameObject.transform.rotation.x , stateGameObject.transform.rotation.y - animOffsetRotation, stateGameObject.transform.rotation.z );
            //animator.SetBool("running",false);
        }
        return newGameState;
        
    }

    public override void Start()
    {
        currentAttackTime = 0;
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        anim = stateGameObject.GetComponent<Animator>();
        playerTransform = stateGameObject.GetComponent<Transform>();
        currentAttackDelay = attackDelay;
        
       // Obtener la posición del ratón en la pantalla
        Vector3 mousePos = Input.mousePosition;

        // Calcular la dirección del ratón en el mundo
        Ray castPoint = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Vector3 targetDir = Vector3.zero;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            targetDir = hit.point - stateGameObject.transform.position;
            targetDir.y = 0f; // Mantener en el plano XY
        }
        else
        {
            targetDir = castPoint.direction;
            targetDir.y = 0f; // Mantener en el plano XY
        }

        // Rotar el jugador hacia la dirección del ratón
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        stateGameObject.transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);

        attackAreaVisualizer = stateGameObject.GetComponent<AttackAreaVisualizer>();
        if (attackAreaVisualizer == null)
        {
            Debug.LogError("No se encontró el componente AttackAreaVisualizer en el jugador.");
            return;
        }

        attackAreaVisualizer.attackOffset = attackOffset;
        attackAreaVisualizer.sphereSize = sphereSize;
        ExecuteAnim();
    }
    private void ExecuteAnim()
    {
        anim.SetTrigger("attack");
        stateGameObject.transform.rotation = Quaternion.Euler(stateGameObject.transform.rotation.x , stateGameObject.transform.rotation.y + animOffsetRotation, stateGameObject.transform.rotation.z );
        currentAttackDelay = attackDelay;
        canAttack = true;
    }
    void ExecuteAttack()
    {
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
            Timers.timer.playerBasicAttackTimer = 0;
        }
        
    }

    public override void Update()
    {
        return;
    }
}
