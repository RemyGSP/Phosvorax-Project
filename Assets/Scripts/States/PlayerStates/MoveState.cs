using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

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
    private Coroutine isMoving;
    private FMOD.Studio.EventInstance foosteps;
    float currentMovementStatusTimer;
    #endregion

    #region Constructor
    public MoveState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Métodos concretos
    public override void Start()
    {
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        currentMovementStatusTimer = 0;
        currentSpeed = 0;
        animator = PlayerReferences.instance.GetPlayerAnimator();
        animator.SetBool("running", true);
    }



    private Vector3 Move(Vector3 playerDirection)
    {
        if (currentSpeed < maxSpeed +  ShopManager.instance.GetSpeedLevel() * maxSpeed * 0.2f)
        {
            currentSpeed = maxSpeed * curveToMaxAcceleration.Evaluate(currentMovementStatusTimer / timeToMaxVelocity);
        }
        currentMovementStatusTimer += Time.deltaTime;
        if (isMoving == null)
        {
            isMoving = MonoInstance.instance.StartCoroutine(_PlayFootstep());

        }

        return playerDirection * currentSpeed;
    }

    public override void Update()
    {
        // Implementar lógica de actualización si es necesario
    }

    public override void FixedUpdate()
    {
        Vector3 PlayerDirection = Move(PlayerInputController.Instance.GetPlayerInputDirection());
        //PlayerDirection.y = Physics.gravity.y * rigidBody.mass;
        PlayerDirection.y = rigidBody.velocity.y;
        rigidBody.velocity = PlayerDirection;
        stateGameObject.transform.rotation = rotateCharacter.Rotate(stateGameObject.transform.rotation, PlayerDirection);
    }

    public new void OnEnterState()
    {
        base.OnEnterState();
    }
    public override void OnExitState()
    {
        rigidBody.velocity = Vector3.zero;
        if (animator != null)
        {
            animator.SetBool("running", false);
        }
    }

    private IEnumerator _PlayFootstep()
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        foosteps.start();
        foosteps.release();
        yield return new WaitForSeconds(0.4f);
        isMoving = null;
    }
    #endregion
}

