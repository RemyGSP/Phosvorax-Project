using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "States/PlayerMoveState")]
public class MoveState : States
{


    private RotateCharacter rotateCharacter;

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
        bool notChanged = true;
        int counter = 0;
        States newPlayerState = null;

        while (notChanged)
        {
            newPlayerState = stateTransitions[counter].GetExitState();
            if (newPlayerState != null)
            {
                notChanged = false;
                newPlayerState.InitializeState(stateGameObject);
                newPlayerState.Start();
            }
            if (counter < stateTransitions.Length -1 )
            { 
                counter++;
            }
            else
            {
                notChanged = false;
            }
        }

            return newPlayerState;
    }
    #endregion

    #region Métodos concretos
    public override void Start()
    {
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        currentMovementStatusTimer = 0;
        currentSpeed = 0;
        if (stateGameObject.TryGetComponent<Animator>(out Animator playerAnimator))
        {
            this.animator = playerAnimator;
            animator.SetBool("running", true);
        }
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
        Vector3 PlayerDirection = PlayerInputController.Instance. GetPlayerInputDirection();
        rigidBody.velocity = Move(PlayerDirection);
        stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerDirection);
    }
    #endregion
}

