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

    private Rigidbody rigidBody;

    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private  float sphereSize; // Tamaño del área de detección

    private float currentAttackDelay;
    private Transform playerTransform;

    [SerializeField] private AttackAreaVisualizer attackAreaVisualizer;

    

    public BasicAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override States CheckTransitions()
    {
        States newGameState = null;
         if (PlayerTimers.timer.playerBasicAttackTimer < PlayerTimers.timer.playerBasicAttackCD){
            if (PlayerInputController.GetPlayerInputDirection() != Vector3.zero){
            newGameState = Instantiate(moveState);
            }
            if (PlayerInputController.IsRolling() && PlayerTimers.timer.rollTimer > PlayerTimers.timer.rollCD){
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
            //animator.SetBool("running",false);
        }
        return newGameState;
        
    }

    public override void InitializeState(GameObject gameObject)
    {
        // Implementación específica de InitializeState para BasicAttackState
        stateGameObject = gameObject;
    }

    public override void Start()
    {
        rigidBody = stateGameObject.GetComponent<Rigidbody>();

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

        attackAreaVisualizer.atacOffset = attackOffset;
        attackAreaVisualizer.sphereSize = sphereSize;
    
    }

    void ExecuteAtaque()
    {
        Vector3 atacPosition = playerTransform.position + playerTransform.forward * attackOffset;
        int layerMask = 1 << 6; // Selecciona solo la capa 6
        Collider[] hitColliders = Physics.OverlapSphere(atacPosition, sphereSize / 2, layerMask, QueryTriggerInteraction.UseGlobal);

        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
    }

    public override void FixedUpdate()
    {
        currentAttackDelay -= Time.deltaTime;

        if (currentAttackDelay <= 0)
        {
            ExecuteAtaque();
            currentAttackDelay = attackDelay;
            PlayerTimers.timer.playerBasicAttackTimer = 0;
        }
        
    }

    public override void Update()
    {
        // Implementación específica de Update para BasicAttackState
    }
}
