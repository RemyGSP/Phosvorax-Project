using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "States/PlayerMoveStateCharacter")]
public class MoveStateCharacter : States
{


    private RotateCharacter rotateCharacter;

    #region Variables
    private CharacterController characterController;

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
    public MoveStateCharacter(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region M�todos concretos
    public override void Start()
    {
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        characterController = stateGameObject.GetComponent<CharacterController>();
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
        // Implementar l�gica de actualizaci�n si es necesario
    }

    public override void FixedUpdate()
    {
        Vector3 PlayerDirection = PlayerInputController.Instance.GetPlayerInputDirection();
        PlayerDirection.y = Physics.gravity.y;
        characterController.Move(PlayerDirection);
        stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerDirection);
    }

    public override void OnExitState()
    {
        PlayerReferences.instance.GetPlayerAnimator().SetBool("move", false);
    }
    #endregion
}

