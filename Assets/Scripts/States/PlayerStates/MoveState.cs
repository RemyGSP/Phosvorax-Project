using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "States/PlayerMoveState")]
public class MoveState : States
{
    [Header("States")]
    [SerializeField] private States idleState;
    [SerializeField] private States rollState;
    [SerializeField] private States basicAttackState;

    #region Variables
    private Rigidbody rigidBody;

    [Header("MoveValues")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] float timeToMaxVelocity;
    [SerializeField] AnimationCurve curveToMaxAcceleration;
    [SerializeField] private float rotationSpeed;
    private Animator animator;
    float currentMovementStatusTimer;
    #endregion

    #region Constructor
    public MoveState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Métodos abstractos
    public override States CheckTransitions()
    {
        States newGameState = null;
        
        if (PlayerInputController.IsRolling() && Timers.timer.rollTimer > Timers.timer.rollCD){
            newGameState = Instantiate(rollState);
        }
        if (PlayerInputController.GetPlayerInputDirection() == Vector3.zero){
            newGameState = Instantiate(idleState);
        }
        if (PlayerInputController.IsAttacking() && Timers.timer.playerBasicAttackTimer > Timers.timer.playerBasicAttackCD)
        {
            newGameState = Instantiate(basicAttackState);
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
    #endregion

    #region Métodos concretos
    public override void Start()
    {
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        currentMovementStatusTimer = 0;
        currentSpeed = 0;
        if (stateGameObject.TryGetComponent<Animator>(out Animator playerAnimator))
        {
            this.animator = playerAnimator;
            animator.SetBool("running", true);
        }
    }

    void Rotate(Vector3 direction)
    {
        Vector3 relative = (stateGameObject.transform.position + direction) - stateGameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);
        stateGameObject.transform.rotation = rotation;
    }

    private Vector3 Move(Vector3 playerDirection)
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed = maxSpeed * curveToMaxAcceleration.Evaluate(currentMovementStatusTimer / timeToMaxVelocity);
        }
        currentMovementStatusTimer += Time.deltaTime;

        return playerDirection * currentSpeed;
    }

    public override void Update()
    {
        // Implementar lógica de actualización si es necesario
    }

    public override void FixedUpdate()
    {
        Vector3 PlayerDirection = PlayerInputController.GetPlayerInputDirection();
        rigidBody.velocity = Move(PlayerDirection);
        Rotate(PlayerDirection);
    }
    #endregion
}

