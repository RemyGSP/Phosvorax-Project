using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/BossJumpfallState")]
public class BossJumpfallState : States
{
    #region Constructor
    public BossJumpfallState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [SerializeField] LayerMask playerLayerMask;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;


    [SerializeField] private float attackDelay = 0.5f; // Tiempo de retraso antes de ejecutar el ataque
    [SerializeField] private float attackOffset = 1.0f; // Distancia desde el jugador para el inicio del ataque
    [SerializeField] private float sphereSize; // Tama�o del �rea de detecci�n
    [SerializeField] private float attackDamage;

    private Vector3 finalPosition;

    private bool hasExecutedAttack;

    [Header("Jump values ")]
    [SerializeField] float jumpForce;

    [Header("Fall values")]
    [SerializeField] float timeFalling;

    private UnityEngine.AI.NavMeshAgent enemy;
    #endregion

    #region Methods

    public override States CheckTransitions()
    {
        return base.CheckTransitions();
    }
    public override void OnExitState()
    {
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
        stateGameObject.GetComponent<BossReferences>().SetCanUseAbility(false);
        base.OnExitState();
    }

    public override void Start()
    {
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        finalPosition = stateGameObject.transform.position;
        hasExecutedAttack = false;
        stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerReferences.instance.GetPlayerCoordinates());
        enemy.speed = 0;
        MakeJump();
    }

    public override void Update()
    {
        
    }

    private void MakeJump()
    {
        Vector3 jumpDirection = stateGameObject.transform.up;

        // Aplica una fuerza al Rigidbody para que el jefe salte
        rigidBody.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
       

        Fall(finalPosition);
    }

    private void Fall(Vector3 finalPosition)
    {
        while (stateGameObject.transform.position.y > finalPosition.y)
        {
            // Calcula el desplazamiento hacia abajo
            float step = timeFalling * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(stateGameObject.transform.position, finalPosition, step);

            // Actualiza la posición del jefe
            stateGameObject.transform.position = newPosition;
        }

        // El jefe ha terminado de caer
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
        hasExecutedAttack = true;
    }
    #endregion
}
