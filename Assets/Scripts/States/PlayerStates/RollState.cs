using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerRollState")]
public class RollState : States
{
    [Header("States")]
    [SerializeField] private States idleState;
    [SerializeField] private States moveState;
    #region Variables
    private Rigidbody rigidBody;
    [Header("Dash Variables")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    private Vector3 dashFinalPos;
    private Animator animator;
    private Vector3 playerDirection;
    private float currentDashTime = 0f;
    #endregion

    #region Methods
    public RollState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override void Start()
    {
        currentDashTime = 0f;
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        if (stateGameObject.TryGetComponent<Animator>(out Animator objectAnimator))
        {
            animator = objectAnimator;
            animator.SetBool("dashing", true);
        }
        playerDirection = PlayerInputController.GetPlayerInputDirection();
    }

    public override States CheckTransitions()
    {
        States newPlayerState = null;

        if (Timers.timer.rollTimer < Timers.timer.rollCD)
        {
            
            if (PlayerInputController.GetPlayerInputDirection() != Vector3.zero){
                newPlayerState = moveState;
            }     
            else{
                newPlayerState = idleState;
            }
                
                
        }
        if (newPlayerState != null)
        {
            newPlayerState.InitializeState(stateGameObject);
            newPlayerState.Start();
            rigidBody.velocity = Vector3.zero;
            //animator.SetBool("dashing", false); Comentado porque de momento no tenemos animacion
        }
        return newPlayerState;
    }

    public override void FixedUpdate()
    {
        if (dashTime >= currentDashTime)
        {
            Dash();
            currentDashTime += Time.deltaTime;
        }
        else
        {
            Timers.timer.rollTimer = 0;
        }
    }

    public void Dash()
    {
        if (playerDirection != Vector3.zero)
        {
            rigidBody.AddForce(playerDirection * dashForce * Time.deltaTime, ForceMode.Impulse);
        }
        //Esto es por si el usuario no tienes ninguna direccion pulsada, asi que pillara la rotacion actual del personaje
        else
            rigidBody.AddForce(stateGameObject.transform.rotation * Vector3.forward * dashForce * Time.deltaTime, ForceMode.Impulse);
    }


    public override void Update()
    {
        return;
    }
    #endregion
}